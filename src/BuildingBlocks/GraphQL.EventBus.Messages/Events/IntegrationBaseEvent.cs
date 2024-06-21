using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }

        public IntegrationBaseEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreateDate = createDate;
        }

        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
    }
}
