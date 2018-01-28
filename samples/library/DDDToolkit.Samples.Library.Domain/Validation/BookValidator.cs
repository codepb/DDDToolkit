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
            Property(b => b.ISBN).HasRule<IsbnIsNumeric>().HasRule<IsbnIsOfValidLength>();
            Property(b => b.Author).HasRule<AuthorHasFirstName>().HasRule<AuthorHasLastName>();
        }
    }
}
