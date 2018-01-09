using DDDToolkit.Core;
using System;
using System.Text.RegularExpressions;

namespace DDDToolkit.Samples.Library.Domain
{
    public class ISBN : ValueObject
    {
        private string _value;
        public string Value => _value;

        protected ISBN() { }

        public ISBN(string isbn)
        {
            if(!IsValid(isbn))
            {
                throw new ArgumentException($"{isbn} is not a valid ISBN.", nameof(isbn));
            }
            _value = isbn;
        }

        public static bool IsValid(string isbn)
        {
            return IsOfCorrectLength(isbn) && IsNumeric(isbn);
        }

        private static bool IsOfCorrectLength(string isbn)
        {
            return isbn.Length == 10 || isbn.Length == 13;
        }

        private static bool IsNumeric(string isbn)
        {
            return Regex.IsMatch(isbn, @"^\d+$");
        }
    }
}