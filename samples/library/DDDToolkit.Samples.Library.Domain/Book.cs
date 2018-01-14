using DDDToolkit.Core;

namespace DDDToolkit.Samples.Library.Domain
{
    public class Book : AggregateRoot<int>
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public ISBN ISBN { get; set; }
    }
}
