using DDDToolkit.Core;

namespace DDDToolkit.Samples.Library.Domain
{
    public class Author : ValueObject
    {
        private string _firstName;
        public string FirstName => _firstName;
        private string _lastName;
        public string LastName => _lastName;
        public string FullName => $"{FirstName} {LastName}";

        protected Author() { }

        public Author(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }
    }
}