using DDDToolkit.Querying;

namespace DDDToolkit.Samples.Library.Domain
{
    internal class AuthorHasLastName : Query<Author>
    {
        public AuthorHasLastName() : base(Has(a => a.LastName).Satisfying(x => !string.IsNullOrWhiteSpace(x)))
        {
        }
    }
}