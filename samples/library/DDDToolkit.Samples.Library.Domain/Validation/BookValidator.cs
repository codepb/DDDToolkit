using DDDToolkit.Validation;
using DDDToolkit.Samples.Library.Domain.Validation;
using FluentQueries;

namespace DDDToolkit.Samples.Library.Domain
{
    public class BookValidator : Validator<Book>
    {
        public BookValidator()
        {
            Property(b => b.ISBN)
                .HasRule<IsbnIsNumeric>("ISBN is not numeric")
                .HasRule<IsbnIsOfValidLength>();
            Property(b => b.Author)
                .HasRule<AuthorHasFirstName>()
                .HasRule<AuthorHasLastName>();
            Property(b => b.Title)
                .HasRule(Query<string>.Is.NotNullOrEmpty(), "Title is required");
        }
    }
}
