using DDDToolkit.Querying;

namespace DDDToolkit.Samples.Library.Domain
{
    public class AuthorHasName : Query<Author>
    {
        public AuthorHasName(string firstName, string lastName)
            : base(
                  Has(a => a.FirstName)
                  .EqualTo(firstName)
                  .And()
                  .Has(a => a.LastName)
                  .EqualTo(lastName)
                  ) { }
    }
}
