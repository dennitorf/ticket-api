using MediatR;
using System.Collections.Generic;

namespace Super.Ticket.Application.Features.Todos.Queries.GetAllTodos
{
    public class GetAllTodosQuery : IRequest<IEnumerable<TodoDto>>
    {
        public int Id { set; get; }

        public string Name { set; get; }
    }
}
