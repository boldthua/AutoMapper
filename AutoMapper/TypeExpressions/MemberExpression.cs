using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeExpressions
{
    internal class MemberExpression : AExpression
    {
        public override object GetValue(Expression expression, object source)
        {
            var member = (System.Linq.Expressions.MemberExpression)expression;
            var propertyName = member.Member.Name;
            var value = source.GetType().GetProperty(propertyName).GetValue(source);
            return value;
        }
    }
}
