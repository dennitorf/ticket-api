using MediatR;

namespace Super.Ticket.Application.Features.TodoItems.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest
    {
        public int Id { set; get; }
    }
}
