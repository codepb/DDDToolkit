namespace DDDToolkit.Core.Events
{
    public class DomainEvent : IDomainEvent
    {
        public int Version { get; set; }
    }
}
