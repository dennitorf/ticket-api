using Super.Ticket.Application.Features.Todos.Queries.GetAllTodos;
using MediatR;

namespace Super.Ticket.Application.Features.Todos.Queries.GetTodo
{
    public class GetTodoQuery : IRequest<TodoDto>
    {
        public int Id { set; get; }
    }
}
