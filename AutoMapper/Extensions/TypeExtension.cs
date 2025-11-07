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
            if (propertyType.IsArray)
                return PropTypes.Array;
            else if (propertyType != typeof(string) && propertyType.GetInterfaces()
                        .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                return PropTypes.Enumerable;
            }
            else if (propertyType.IsClass)
                return PropTypes.Class;
            else if (propertyType.IsEnum)
                return PropTypes.Enum;
            else
                return PropTypes.Basic;
        }
    }
}
