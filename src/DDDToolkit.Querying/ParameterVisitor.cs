using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    class ParameterVisitor : ExpressionVisitor
    {
        private readonly IReadOnlyList<ParameterExpression> _from;
        private readonly Expression[] _to;
        public ParameterVisitor(
            IReadOnlyList<ParameterExpression> from,
             IReadOnlyList<ParameterExpression> to) : this(from, (Expression[])to.ToArray())
        {
            
        }

        public ParameterVisitor(
            IReadOnlyList<ParameterExpression> from,
            params Expression[] to)
        {
            if (from == null) throw new ArgumentNullException("from");
            if (to == null) throw new ArgumentNullException("to");
            if (from.Count != to.Length) throw new InvalidOperationException(
                  "Parameter lengths must match");
            _from = from;
            _to = to;
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            for (int i = 0; i < _from.Count; i++)
            {
                if (node == _from[i]) return _to[i];
            }
            return node;
        }
    }
}