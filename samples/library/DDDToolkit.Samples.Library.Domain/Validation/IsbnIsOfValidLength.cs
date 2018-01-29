using DDDToolkit.Querying;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DDDToolkit.Samples.Library.Domain.Validation
{
    public class IsbnIsOfValidLength : Query<ISBN>
    {
        public IsbnIsOfValidLength()
            : base(Has(i => i.Value.Length)
                  .EqualToAnyOf(10, 13))                  
        {
        }
    }
}
