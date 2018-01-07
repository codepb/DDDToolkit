namespace DDDToolkit.Core
{
    public interface IDomainEvent
    {
        int Version { get; set; }
    }
}
