using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeExpressions
{
    internal class NewExpression : AExpression
    {
        public override object GetValue(Expression expression, object source)
        {
            var newExp = (System.Linq.Expressions.NewExpression)expression;
            var constructor = newExp.Constructor;
            var arguments = newExp.Arguments;
            var args = arguments.ToArray();
            var result = constructor.Invoke(args);
            return result;
        }
    }
}
