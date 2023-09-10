using Super.Ticket.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Super.Ticket.Persistence.Seeders;
using System;

namespace  Super.Ticket.Application.UnitTests.Mocks.Persistence
{
    public static class  AppplicationDbContextMock
    {
        public static AppDbContext appDbContext;

        static AppplicationDbContextMock()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            
            appDbContext = new AppDbContext(optionsBuilder.Options);

            InitInMemoryDatabase();
        }

        static void InitInMemoryDatabase() 
        {
            var seeder = new AppDbContextSeeder();
            seeder.SeedEverything(appDbContext);
        }  
    }
}