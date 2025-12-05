using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.Enums
{
    internal enum ExpressionType
    {
        MemberExpression = 0,
        ConstantExpression = 1,
        MethodCallExpression = 2,
        UnaryExpression = 3,
        BinaryExpression = 4,
        ConditionalExpression = 5,
        NewExpression = 6,
        Parameter = 7
    }
}
