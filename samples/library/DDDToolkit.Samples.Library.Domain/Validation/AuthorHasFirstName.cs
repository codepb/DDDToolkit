using FluentQueries;

namespace DDDToolkit.Samples.Library.Domain
{
    public class AuthorHasFirstName : Query<Author>
    {
        public AuthorHasFirstName()
        {
            Define(
                Has(a => a.FirstName)
                .Satisfying(x => !string.IsNullOrWhiteSpace(x))
            );
        }
    }
}