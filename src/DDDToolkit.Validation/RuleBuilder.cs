using DDDToolkit.Querying;
using System;
using System.Linq.Expressions;

namespace DDDToolkit.Validation
{
    public class RuleBuilder<T, TProp>
    {
        private Expression<Func<T, TProp>> _propertyAccessor;
        private Action<Expression<Func<T, TProp>>, IQuery<TProp>> _ruleFor;

        public RuleBuilder(Expression<Func<T, TProp>> propertyAccessor, Action<Expression<Func<T, TProp>>, IQuery<TProp>> ruleFor)
        {
            _propertyAccessor = propertyAccessor;
            _ruleFor = ruleFor;
        }

        public RuleBuilder<T, TProp> HasRule<TQuery>() where TQuery : IQuery<TProp>, new()
        {
            var query = Activator.CreateInstance<TQuery>();
            _ruleFor(_propertyAccessor, query);
            return new RuleBuilder<T, TProp>(_propertyAccessor, _ruleFor);
        }

        public RuleBuilder<T, TProp> HasRule(IQuery<TProp> query)
        {
            _ruleFor(_propertyAccessor, query);
            return new RuleBuilder<T, TProp>(_propertyAccessor, _ruleFor);
        }
    }
}