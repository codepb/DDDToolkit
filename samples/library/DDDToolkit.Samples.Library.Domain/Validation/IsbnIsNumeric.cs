using DDDToolkit.Querying;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace DDDToolkit.Samples.Library.Domain.Validation
{
    public class IsbnIsNumeric : Query<ISBN>
    {
        public IsbnIsNumeric() :
            base(Has(i => i.Value).Satisfying(i => Regex.IsMatch(i, @"^\d+$")))
        {
        }
    }
}
