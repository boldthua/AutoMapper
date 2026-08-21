using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeExpressions
{
    public class MemberExpression : AExpression
    {
        public override object GetValue(Expression expression, object source)
        {
            var member = (System.Linq.Expressions.MemberExpression)expression;
            if (member.Expression != null) //  在一般 MemberExpression 的情況下 member.Expression 會是 Parameter Expression 也就是 y 本身
            {
                var closure = GetExpressionValue(member.Expression, source);

                if (member.Member is FieldInfo fi)
                    return fi.GetValue(closure);         // names（閉包欄位）
                if (member.Member is PropertyInfo pi)
                    return pi.GetValue(closure, null);   // y.Names（屬性）
            }
            var propertyName = member.Member.Name;
            var value = source.GetType().GetProperty(propertyName).GetValue(source);
            return value;
        }
    }
}
