namespace DDDToolkit.Core.Events
{
    public interface IDomainEvent
    {
        int Version { get; set; }
    }
}
