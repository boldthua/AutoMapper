using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeExpressions
{
    internal class BinaryExpression : AExpression
    {
        public override object GetValue(Expression expression, object source)
        {
            var BExp = (System.Linq.Expressions.BinaryExpression)expression;
            var left = GetExpressionValue(BExp.Left, source);
            var right = GetExpressionValue(BExp.Right, source);
            var nodeType = BExp.NodeType;

            switch (nodeType)
            {
                case ExpressionType.Add:
                    if (left is string || right is string)
                    {
                        return $"{left}{right}";
                    }
                    return Convert.ToDecimal(left) + Convert.ToDecimal(right);
                case ExpressionType.Subtract:
                    return (decimal)left - (decimal)right;
                case ExpressionType.Multiply:
                    return (decimal)left * (decimal)right;
                case ExpressionType.Divide:
                    return (decimal)left / (decimal)right;
                case ExpressionType.Modulo:
                    return (decimal)left % (decimal)right;
                case ExpressionType.AndAlso:
                    if ((bool)left && (bool)right)
                        return true;
                    else
                        return false;
                case ExpressionType.OrElse:
                    if ((bool)left || (bool)right)
                        return true;
                    else
                        return false;
                case ExpressionType.Or:
                    if ((bool)left | (bool)right)
                        return true;
                    else
                        return false;
                case ExpressionType.ExclusiveOr:
                    if ((bool)left | (bool)right)
                        return true;
                    else
                        return false;
                case ExpressionType.Equal:
                    bool result = left == right ? true : false;
                    return result;
                case ExpressionType.NotEqual:
                    bool result1 = left != right ? true : false;
                    return result1;
                case ExpressionType.GreaterThan:
                    return (decimal)left > (decimal)right;
                case ExpressionType.LessThan:
                    return (decimal)left < (decimal)right;
                case ExpressionType.GreaterThanOrEqual:
                    return (decimal)left >= (decimal)right;
                case ExpressionType.LessThanOrEqual:
                    return (decimal)left <= (decimal)right;
            }
            throw new Exception("BinaryExpression上述條件都不相符");
        }
    }
}
