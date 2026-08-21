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
    public class MappingExpression<TDestination, TSource> where TDestination : class, new() where TSource : class, new()
    {
        public Dictionary<PropertyInfo, ExpressionModel> mapping = new Dictionary<PropertyInfo, ExpressionModel>();
        // propertyinfo是dest , object是source

        public MappingExpression<TDestination, TSource> ForMember<TDestProp, TSourProp>(Expression<Func<TDestination, TDestProp>> expDest, Expression<Func<TSource, TSourProp>> expSource)
        {
            string destPropName = ((System.Linq.Expressions.MemberExpression)expDest.Body).Member.Name;

            var expType = AExpression.CheckExpressionType(expSource.Body);

            PropertyInfo destProperty = typeof(TDestination).GetProperty(destPropName);
            //PropertyInfo sourceProperty = typeof(TSource).GetProperty(sourcePropName);

            mapping.Add(destProperty, new ExpressionModel(expType.Key, expType.Value));

            return this;
        }




    }
}
