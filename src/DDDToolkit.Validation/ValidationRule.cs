using DDDToolkit.Querying;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Validation
{
    internal class ValidationRule<T>
    {
        public IQuery<T> Test { get; }
        public string Message { get; }

        public ValidationRule(IQuery<T> test, string message)
        {
            Test = test;
            Message = message;
        }
    }
}
