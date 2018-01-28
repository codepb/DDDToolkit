using DDDToolkit.Querying;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DDDToolkit.Validation
{
    public abstract class Validator<T> : IValidator<T>
    {
        private IDictionary<object, ICollection<object>> _rules = new Dictionary<object, ICollection<object>>();

        protected void RuleFor<TProp>(Expression<Func<T, TProp>> propertyAccessor, IQuery<TProp> validationRule)
            => RulesFor(propertyAccessor, validationRule);

        protected void RulesFor<TProp>(Expression<Func<T, TProp>> propertyAccessor, params IQuery<TProp>[] validationRules)
        {
            var rules = validationRules.ToList().Select(r => new Query<T>(r.AsExpression().WithParameter(propertyAccessor))).ToList();
            if (!_rules.ContainsKey(propertyAccessor))
            {
                _rules.Add(propertyAccessor, new List<object>() );
            }
            rules.ForEach(r => _rules[propertyAccessor].Add(r));
        }

        protected RuleBuilder<T, TProp> Property<TProp>(Expression<Func<T, TProp>> propertyAccessor)
            => new RuleBuilder<T, TProp>(propertyAccessor, this.RuleFor<TProp>);

        private bool TestRule(object validationRule, T subject)
            => ((IQuery<T>)validationRule).IsSatisfiedBy(subject);

        public bool IsValid(T subject)
            => _rules.Values.SelectMany(r => r).All(r => TestRule(r, subject));


        public bool IsValidProperty<TProp>(T subject, Expression<Func<T, TProp>> propertyAccessor)
        {
            if(!_rules.ContainsKey(propertyAccessor))
            {
                return true;
            }
            return _rules[propertyAccessor].All(r => TestRule(r, subject));
        }
    }
}
