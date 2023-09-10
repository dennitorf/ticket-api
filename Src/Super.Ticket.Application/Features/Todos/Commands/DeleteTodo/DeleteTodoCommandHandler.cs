using AutoMapper;
using Super.Ticket.Application.Common.Exceptions;
using Super.Ticket.Domain.Entities.Sample;
using Super.Ticket.Persistence.Contexts;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Super.Ticket.Application.Features.Todos.Commands.DeleteTodo
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
    {
        private ILogger logger;
        private AppDbContext db;
        private IMapper mapper;

        public DeleteTodoCommandHandler(ILogger<DeleteTodoCommandHandler> logger, AppDbContext db, IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var ent = await db.Todos.FindAsync(request.Id);

            if (ent == null)
                throw new NotFoundException(nameof(Todo), request.Id);

            db.Todos.Remove(ent);
            await db.SaveChangesAsync(cancellationToken);

            return Unit.Task.Result;
        }
    }
}
