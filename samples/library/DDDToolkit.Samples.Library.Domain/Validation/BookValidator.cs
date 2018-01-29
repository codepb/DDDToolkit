using DDDToolkit.Validation;
using DDDToolkit.Querying;
using DDDToolkit.Samples.Library.Domain.Validation;
using System;

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
                .HasRule(Query<string>.Is.Satisfying(s => !string.IsNullOrEmpty(s)), "Title is required");
        }
    }
}
