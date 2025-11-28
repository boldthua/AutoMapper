using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExpressionType = AutoMapper.Enums.ExpressionType;


namespace AutoMapper.TypeExpressions
{
    internal abstract class AExpression
    {
        public abstract Object GetValue(Expression expression, Object source);

        public static object GetExpressionValue(Expression expression, Object source)
        {
            Expression sourceProp = null;
            ExpressionType expType = default;
            if (expression is System.Linq.Expressions.MemberExpression memberExpression)
            {
                return new MemberExpression().GetValue(expression, source);
            }
            else if (expression is System.Linq.Expressions.ConstantExpression constantExpression)
            {
                sourceProp = constantExpression;
                expType = ExpressionType.ConstantExpression;
            }
            else if (expression is System.Linq.Expressions.MethodCallExpression methodCallExpression)
            {
                sourceProp = methodCallExpression;
                expType = ExpressionType.MethodCallExpression;
            }
            else if (expression is System.Linq.Expressions.UnaryExpression unaryExpression)
            {
                sourceProp = unaryExpression;
                expType = ExpressionType.UnaryExpression;
            }
            else if (expression is System.Linq.Expressions.BinaryExpression binaryExpression)
            {
                sourceProp = binaryExpression;
                expType = ExpressionType.BinaryExpression;
            }
            else if (expression is System.Linq.Expressions.ConditionalExpression conditionalExpression)
            {
                sourceProp = conditionalExpression;
                expType = ExpressionType.ConditionalExpression;
            }
            else if (expression is System.Linq.Expressions.NewExpression newExpression)
            {
                sourceProp = newExpression;
                expType = ExpressionType.NewExpression;
            }

            return (expType, sourceProp);
        }
    }



}
