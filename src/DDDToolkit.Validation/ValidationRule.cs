using FluentQueries;

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
