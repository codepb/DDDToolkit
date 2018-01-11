using DDDToolkit.Core;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace DDDToolkit.Samples.Library.Domain
{
    public class ISBN : ValueObject
    {
        private string _value;
        public string Value => _value;

        protected ISBN() { }

        [JsonConstructor]
        public ISBN(string value)
        {
            if(!IsValid(value))
            {
                throw new ArgumentException($"{value} is not a valid ISBN.", nameof(value));
            }
            _value = value;
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