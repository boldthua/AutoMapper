using AutoMapper.TypeExpressions;
using AutoMapper.TypeMappings;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ExpressionType = AutoMapper.Enums.ExpressionType;
namespace AutoMapper
{
    internal class MappingExpression<TDestination, TSource> where TDestination : class, new() where TSource : class, new()
    {
        public Dictionary<PropertyInfo, ExpressionModel> mapping = new Dictionary<PropertyInfo, ExpressionModel>();
        // propertyinfo是dest , object是source

        public MappingExpression<TDestination, TSource> ForMember<TDestProp, TSourProp>(Expression<Func<TDestination, TDestProp>> expDest, Expression<Func<TSource, TSourProp>> expSource)
        {

            //source可能不是簡單的Member而已，可能是其它的Expression，要分辦出來。
            // 寫出各式expression

            //string sourcePropName = ((MemberExpression)expSource.Body).Member.Name;
            string destPropName = ((System.Linq.Expressions.MemberExpression)expDest.Body).Member.Name;

            //Member => 直接傳入類別屬性
            //Binary => 兩個運算元
            //Conditional => 條件式 (三元運算式)
            //Constant => 常數使用
            //MethodCall => 函數呼叫完後的結果
            //Unary => !x.Enabled  => 只有一個運算元 !x, -x, (double)x;
            //Parameter
            //Lambda
            //New



            var expType = AExpression.CheckExpressionType(expSource.Body);

            PropertyInfo destProperty = typeof(TDestination).GetProperty(destPropName);
            //PropertyInfo sourceProperty = typeof(TSource).GetProperty(sourcePropName);

            mapping.Add(destProperty, new ExpressionModel(expType.Item1, expType.Item2));

            return this;
        }




    }
}
