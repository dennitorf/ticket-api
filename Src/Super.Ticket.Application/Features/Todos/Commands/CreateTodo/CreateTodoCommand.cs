using Super.Ticket.Application.Features.Todos.Queries.GetAllTodos;
using MediatR;

namespace Super.Ticket.Application.Features.Todos.Commands.CreateTodo
{
    public class CreateTodoCommand : IRequest<TodoDto>
    {
        public string Name { set; get; }
    }
}
