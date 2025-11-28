using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeExpressions
{
    internal class UnaryExpression : AExpression
    {
        public override object GetValue(Expression expression, object source)
        {
            var UExp = (System.Linq.Expressions.UnaryExpression)expression;
            var oper = UExp.Operand;

            object destValue = GetExpressionValue(oper, source);

            var nodeType = UExp.NodeType;
            var resultType = UExp.Type;
            switch (nodeType)
            {
                case ExpressionType.Convert:
                    return Convert.ChangeType(destValue, resultType);
                case ExpressionType.Negate:
                    return -(int)destValue;
                case ExpressionType.Not:
                    return !(bool)destValue;
                case ExpressionType.Increment:
                    return (int)destValue + 1;
                case ExpressionType.Decrement:
                    return (int)destValue - 1;
            }
            return null;

        }
    }
}
