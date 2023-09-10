using AutoMapper;
using Super.Ticket.Application.Common.Exceptions;
using Super.Ticket.Application.Features.Todos.Queries.GetAllTodos;
using Super.Ticket.Domain.Entities.Sample;
using Super.Ticket.Persistence.Contexts;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Super.Ticket.Application.Features.Todos.Queries.GetTodo
{
    public class GetTodoQueryHandler : IRequestHandler<GetTodoQuery, TodoDto>
    {        
        private ILogger logger;
        private AppDbContext db;
        private IMapper mapper;

        public GetTodoQueryHandler(ILogger<GetTodoQueryHandler> logger, AppDbContext db, IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<TodoDto> Handle(GetTodoQuery request, CancellationToken cancellationToken)
        {
            var ent = await db.Todos.FindAsync(request.Id);

            if (ent == null)
                throw new NotFoundException(nameof(Todo), request.Id);

            return mapper.Map<TodoDto>(ent);

        }
    }
}
