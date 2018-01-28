using DDDToolkit.Querying;

namespace DDDToolkit.Samples.Library.Domain
{
    public class AuthorHasFirstName : Query<Author>
    {
        public AuthorHasFirstName() : base(Has(a => a.FirstName).Satisfying(x => !string.IsNullOrWhiteSpace(x)))
        {
        }
    }
}