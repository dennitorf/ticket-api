using Super.Ticket.Application.Features.Todos.Queries.GetAllTodos;
using MediatR;

namespace Super.Ticket.Application.Features.Todos.Commands.UpdateTodo
{
    public class UpdateTodoCommand : IRequest<TodoDto>
    {
        public int Id { get; set; } 
        public string Name { set; get; }
    }
}
