using Super.Ticket.Domain.Common;
using System.Collections.Generic;

namespace Super.Ticket.Domain.Entities.Sample
{
    public class Todo : BaseEntity
    {
        public string Name { set; get; }        
        public virtual ICollection<TodoItem> TodoItems { get; set; }
    }
}
