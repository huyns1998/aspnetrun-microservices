using System;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public Guid ID { get; private set; }
        public DateTime CreationDate { get; private set; }
        public IntegrationBaseEvent()
        {
            ID = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(Guid id, DateTime creationDate)
        {
            ID = id;
            CreationDate = creationDate;
        }
    }
}
