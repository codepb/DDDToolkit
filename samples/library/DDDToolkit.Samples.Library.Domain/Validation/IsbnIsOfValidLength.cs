using FluentQueries;

namespace DDDToolkit.Samples.Library.Domain.Validation
{
    public class IsbnIsOfValidLength : Query<ISBN>
    {
        public IsbnIsOfValidLength()                  
        {
            Define(
                Has(i => i.Value.Length)
                  .EqualToAnyOf(10, 13)
            );
        }
    }
}
