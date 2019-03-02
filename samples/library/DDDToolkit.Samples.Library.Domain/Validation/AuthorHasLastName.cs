using FluentQueries;

namespace DDDToolkit.Samples.Library.Domain
{
    internal class AuthorHasLastName : Query<Author>
    {
        public AuthorHasLastName()
        {
            Define(
                Has(a => a.LastName)
                .Satisfying(x => !string.IsNullOrWhiteSpace(x))
            );
        }
    }
}