using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AutoMapper.TypeExpressions
{
    internal class ConditionalExpression : AExpression
    {
        public override object GetValue(Expression expression, object source)
        {
            var CExp = (System.Linq.Expressions.ConditionalExpression)expression;
            bool test = (bool)GetExpressionValue(CExp.Test, source);
            var result = test == true ? GetExpressionValue(CExp.IfTrue, source) : GetExpressionValue(CExp.IfFalse, source);
            return result;

        }
    }

    //Test	  Expression 條件運算式（必須是 bool）
    //IfTrue  Expression 當 Test 為 true 時返回的運算式
    //IfFalse Expression 當 Test 為 false 時返回的運算式
    //Type    Type       整個條件運算式的結果型別（通常由 IfTrue/IfFalse 決定）
}
