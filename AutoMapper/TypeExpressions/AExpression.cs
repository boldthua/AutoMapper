using AutoMapper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            Type targetType = expression.GetType();
            var baseTypes = GetAllBaseTypes(targetType);
            Type expressionType = Assembly.GetExecutingAssembly().DefinedTypes.FirstOrDefault(x => baseTypes.Contains(x.Name));
            AExpression rExpression = (AExpression)Activator.CreateInstance(expressionType);

            return rExpression.GetValue(expression, source);
        }

        public static KeyValuePair<ExpressionType, Expression> CheckExpressionType(Expression expression)
        {

            Type targetType = expression.GetType();
            var baseTypes = GetAllBaseTypes(targetType);
            var expType = Assembly.GetExecutingAssembly().DefinedTypes.FirstOrDefault(x => baseTypes.Contains(x.Name));
            var rExpression = (ExpressionType)Enum.Parse(typeof(ExpressionType), expType.Name);
            KeyValuePair<ExpressionType, Expression> result = new KeyValuePair<ExpressionType, Expression>(rExpression, expression);
            return result;
        }

        public static IEnumerable<string> GetAllBaseTypes(Type type)
        {
            yield return type.Name;
            var current = type.BaseType;
            while (current != null)
            {
                yield return current.Name;
                current = current.BaseType;
            }
        }

    }



}
