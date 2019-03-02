namespace DDDToolkit.EventSourcing.Events
{
    public class DomainEvent : IDomainEvent
    {
        public int Version { get; set; }
    }
}
