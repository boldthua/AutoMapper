using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeExpressions
{
    internal class ConstantExpression : AExpression
    {
        public override object GetValue(Expression expression, object source)
        {
            var constant = (System.Linq.Expressions.ConstantExpression)expression;
            return constant.Value;
        }
    }
}
