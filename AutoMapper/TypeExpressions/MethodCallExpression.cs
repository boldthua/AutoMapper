using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeExpressions
{
    internal class MethodCallExpression : AExpression
    {
        public override object GetValue(Expression expression, object source)
        {
            var MExp = (System.Linq.Expressions.MethodCallExpression)expression;
            var obj = MExp.Object;
            var data = GetExpressionValue(obj, source);
            var method = MExp.Method;

            var args = MExp.Arguments.Select(x => GetExpressionValue(x, source)).ToArray();

            var res = method.Invoke(data, args);


            return res;
        }
    }
}
