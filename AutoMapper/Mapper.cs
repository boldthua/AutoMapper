using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal class Mapper
    {
        //public TDestination Map<TDestination>(object source) where TDestination : class, new()
        //{

        //    var TDestinationProperties = typeof(TDestination).GetProperties();
        //    TDestination destination = new TDestination();
        //    foreach (var property in TDestinationProperties)
        //    {
        //        PropertyInfo sourceProperty = source.GetType().GetProperty(property.Name);

        //        var sourcePropertyClass = sourceProperty.PropertyType;
        //        var propertyClass = property.PropertyType;
        //        var value = sourceProperty.GetValue(source);


        //        if (property.PropertyType == typeof(int))
        //        {
        //            var type = typeof(int);
        //            var parseMethod = type.GetMethod("Parse", new Type[] { typeof(string) });
        //            var result = parseMethod.Invoke(null, new object[] { value.ToString() });
        //            property.SetValue(destination, result);
        //        }
        //        else if (sourceProperty.PropertyType.IsEnum)
        //        {
        //            var type = typeof(Enum);
        //            var toStringMethod = type.GetMethod("ToString", new Type[] { });
        //            var result = toStringMethod.Invoke(value, new object[] { });
        //            property.SetValue(destination, result);
        //        }
        //        else
        //            property.SetValue(destination, value);
        //    }
        //    return destination;
        //}



        public TDestionation Map<TDestionation>(object source) where TDestionation : class, new()
        {
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();
            TDestionation dest = new TDestionation();
            foreach (var property in sourceProperties)
            {
                PropertyInfo DestProperty = dest.GetType().GetProperty(property.Name);
                object data = property.GetValue(source).ToString();
                // 如果DestProperty是int
                if (DestProperty.PropertyType == typeof(int) && !property.PropertyType.IsEnum)
                {
                    var intParse = typeof(int).GetMethod("Parse", new Type[] { typeof(string) });
                    data = intParse.Invoke(null, new object[] { data });

                }
                else if (property.PropertyType.IsEnum)
                {
                    // if destProperty 是 int 或string 又或 
                    var toStringMethod = typeof(Enum).GetMethod("ToString", new Type[] { typeof(Enum) });
                    data = toStringMethod.Invoke(null, new object[] { data });
                }
                else if (DestProperty.PropertyType.IsEnum)
                {

                }
                DestProperty.SetValue(dest, data);
            }
            return dest;

        }
    }
}
