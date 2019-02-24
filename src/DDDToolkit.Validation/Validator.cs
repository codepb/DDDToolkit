using FluentQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DDDToolkit.Validation
{
    public abstract class Validator<T> : IValidator<T>
    {
        private IDictionary<object, ICollection<ValidationRule<T>>> _rules = new Dictionary<object, ICollection<ValidationRule<T>>>();

        private void RuleFor<TProp>(Expression<Func<T, TProp>> propertyAccessor, IQuery<TProp> validationRule, string message)
        {
            var rule = new Query<T>(validationRule.AsExpression().WithParameter(propertyAccessor));
            var compiledPropertyAccessor = propertyAccessor.Compile();
            if (!_rules.ContainsKey(compiledPropertyAccessor))
            {
                _rules.Add(compiledPropertyAccessor, new List<ValidationRule<T>>());
            }
            _rules[compiledPropertyAccessor].Add(new ValidationRule<T>(rule, message));
        }

        protected RuleBuilder<T, TProp> Property<TProp>(Expression<Func<T, TProp>> propertyAccessor)
            => new RuleBuilder<T, TProp>(propertyAccessor, RuleFor);

        private bool TestRule(ValidationRule<T> validationRule, T subject)
            => validationRule.Test.IsSatisfiedBy(subject);

        public bool IsValid(T subject)
            => _rules.Values.SelectMany(r => r).All(r => TestRule(r, subject));


        public bool IsValidProperty<TProp>(T subject, Func<T, TProp> propertyAccessor)
        {
            if(!_rules.ContainsKey(propertyAccessor))
            {
                return true;
            }
            return _rules[propertyAccessor].All(r => TestRule(r, subject));
        }

        public IEnumerable<IBrokenRule<T, object>> Validate(T subject)
        {
            var brokenRules = new List<IBrokenRule<T, object>>();
            foreach(var key in _rules.Keys)
            {
                brokenRules.AddRange(_rules[key].Where(r => !TestRule(r, subject)).Select(r => new BrokenRule<T, object>((Func<T, object>)key, r.Message)));
            }
            return brokenRules;
        }

        public IEnumerable<IBrokenRule<T, TProp>> ValidateProperty<TProp>(T subject, Func<T, TProp> propertyAccessor)
        {
            if (!_rules.ContainsKey(propertyAccessor))
            {
                return Enumerable.Empty<IBrokenRule<T, TProp>>();
            }
            return _rules[propertyAccessor].Where(r => !TestRule(r, subject)).Select(r => new BrokenRule<T, TProp>(propertyAccessor, r.Message));
        }
    }
}
