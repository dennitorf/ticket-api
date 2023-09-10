using Super.Ticket.Persistence.Contexts;
using Super.Ticket.Application.Common.Mappings;
using AutoMapper;

namespace  Super.Ticket.Application.UnitTests.Mocks.Persistence
{
    public static class  AutoMapperMock
    {
        public static IMapper mapper;

        static AutoMapperMock()
        {
            var mapperConfiguration = new MapperConfiguration(c => {
                c.AddProfile<AutoMapperProfile>();
            });

            mapper = mapperConfiguration.CreateMapper();
        }        
    }
}