using Super.Ticket.Application;
using Super.Ticket.Infrastructure;
using Super.Ticket.Persistence;
using Super.Ticket.WebApi.Filters.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace Super.Ticket.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistence(Configuration);
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Super.Ticket.WebApi", Version = "v1" });
            });

            services.AddMvc(options => 
            {
                options.Filters.Add(typeof(TicketCustomExceptionFilterAttribute));
            });

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(cfg => {
                cfg.Authority  = "https://accounts.google.com"; // use env/cfg var
                cfg.Audience = "951705324268-0d03as6du1nj4f30rfu6sp9pmaj5t0he.apps.googleusercontent.com";

                cfg.Events = new JwtBearerEvents() {
                    OnTokenValidated = async ctx => {
                        if (!ctx.Principal.HasClaim(c => c.Type == ClaimTypes.Role)) {
                            // once token is valided, in case roles are missing ... 
                            
                            var email = ctx.Principal.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault();

                            if (email != null) {
                                var claims = new List<Claim>();

                                // code to get the roles claims
                                // var userClaims = await ....() .... / return a preference a list of strings with role identification 
                                // claims.AddRange(userClaims.Select(c => new Claim(c)));

                                // after above code, we should have a list of claims, manually builded, ready to attach to our token

                                var identiy = new ClaimsIdentity(claims);
                                ctx.Principal.AddIdentity(identiy);
                            }
                        }
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Super.Ticket.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
