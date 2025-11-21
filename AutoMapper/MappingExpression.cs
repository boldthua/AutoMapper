using AutoMapper.TypeMappings;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal class MappingExpression<TDestination, TSource> where TDestination : class, new() where TSource : class, new()
    {
        public Dictionary<string, string> mapping = new Dictionary<string, string>();
        // propertyinfo是dest , object是source

        public MappingExpression<TDestination, TSource> ForMember<TDestProp, TSourProp>(Expression<Func<TDestination, TDestProp>> expDest, Expression<Func<TSource, TSourProp>> expSource)
        {

            //source可能不是簡單的Member而已，可能是其它的Expression，要分辦出來。
            // 寫出各式expression

            string sourcePropName = ((MemberExpression)expSource.Body).Member.Name;
            string destPropName = ((MemberExpression)expDest.Body).Member.Name;

            //PropertyInfo destProperty = typeof(TDestination).GetProperty(destPropName);
            //PropertyInfo sourceProperty = typeof(TSource).GetProperty(sourcePropName);

            mapping.Add(destPropName, sourcePropName);

            return this;
        }


    }
}
