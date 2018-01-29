using DDDToolkit.Querying;
using System;
using System.Linq.Expressions;

namespace DDDToolkit.Validation
{
    public class RuleBuilder<T, TProp>
    {
        private Expression<Func<T, TProp>> _propertyAccessor;
        private Action<Expression<Func<T, TProp>>, IQuery<TProp>, string> _ruleFor;

        public RuleBuilder(Expression<Func<T, TProp>> propertyAccessor, Action<Expression<Func<T, TProp>>, IQuery<TProp>, string> ruleFor)
        {
            _propertyAccessor = propertyAccessor;
            _ruleFor = ruleFor;
        }

        public RuleBuilder<T, TProp> HasRule<TQuery>(string message = null) where TQuery : IQuery<TProp>, new()
        {
            var query = Activator.CreateInstance<TQuery>();
            return HasRule(query, message);
        }

        public RuleBuilder<T, TProp> HasRule(IQuery<TProp> query, string message = null)
        {
            var newMessage = message == null ? $"{GetMemberInfo(_propertyAccessor).Member.Name} is invalid. Broken Rule was {query.GetType().Name}": message;
            _ruleFor(_propertyAccessor, query, newMessage);
            return new RuleBuilder<T, TProp>(_propertyAccessor, _ruleFor);
        }

        private static MemberExpression GetMemberInfo(Expression method)
        {
            var lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;
            if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }
    }
}