using MediatR;

namespace Super.Ticket.Application.Features.Todos.Commands.DeleteTodo
{
    public class DeleteTodoCommand : IRequest
    {
        public int Id { set; get; }
    }
}
