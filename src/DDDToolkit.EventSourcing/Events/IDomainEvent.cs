namespace DDDToolkit.EventSourcing.Events
{
    public interface IDomainEvent
    {
        int Version { get; set; }
    }
}
