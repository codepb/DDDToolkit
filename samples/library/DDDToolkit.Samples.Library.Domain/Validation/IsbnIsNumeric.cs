using System.Text.RegularExpressions;
using FluentQueries;

namespace DDDToolkit.Samples.Library.Domain.Validation
{
    public class IsbnIsNumeric : Query<ISBN>
    {
        public IsbnIsNumeric()
        {
            Define(
                Has(i => i.Value)
                .Satisfying(i => Regex.IsMatch(i, @"^\d+$"))
            );
        }
    }
}
