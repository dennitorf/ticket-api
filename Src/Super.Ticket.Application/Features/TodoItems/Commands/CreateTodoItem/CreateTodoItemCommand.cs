using Super.Ticket.Application.Features.TodoItems.Queries.GetAllTodoItems;
using MediatR;

namespace Super.Ticket.Application.Features.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : IRequest<TodoItemDto>
    {
        public string Name { set; get; }
        public int TodoId { set; get; }
    }
}
