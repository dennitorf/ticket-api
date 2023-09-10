using Super.Ticket.Application.Common.Interfaces.Email;
using Super.Ticket.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Super.Ticket.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailService, EmailService>();   

            return services;
        }
    }
}
