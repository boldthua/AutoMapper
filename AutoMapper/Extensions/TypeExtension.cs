using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal static class TypeExtension
    {
        internal static PropTypes CheckType(this Type propertyType)
        {
            if (propertyType == typeof(string))
                return PropTypes.MapBasic;
            else if (propertyType.IsArray)
                return PropTypes.MapArray;
            else if (propertyType.GetInterfaces()
                                 .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                return PropTypes.MapEnumerable;
            }
            else if (propertyType.IsClass)
                return PropTypes.MapClass;
            else if (propertyType.IsEnum)
                return PropTypes.MapEnum;
            else
                return PropTypes.MapBasic;
        }
    }
}
