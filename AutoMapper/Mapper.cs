using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal class Mapper
    {
        public TDestination Map<TDestination>(object source) where TDestination : class, new()
        {

            var TDestinationProperties = typeof(TDestination).GetProperties();
            TDestination destination = new TDestination();
            foreach (var property in TDestinationProperties)
            {
                PropertyInfo sourceProperty = source.GetType().GetProperty(property.Name);

                var sourcePropertyClass = sourceProperty.PropertyType;
                var propertyClass = property.PropertyType;
                var value = sourceProperty.GetValue(source);


                if (property.PropertyType == typeof(int))
                {
                    var type = typeof(int);
                    var parseMethod = type.GetMethod("Parse", new Type[] { typeof(string) });
                    var result = parseMethod.Invoke(null, new object[] { value.ToString() });
                    property.SetValue(destination, result);
                }
                else if (sourceProperty.PropertyType.IsEnum)
                {
                    var type = typeof(Enum);
                    var toStringMethod = type.GetMethod("ToString", new Type[] { });
                    var result = toStringMethod.Invoke(value, new object[] { });
                    property.SetValue(destination, result);
                }
                else
                    property.SetValue(destination, value);
            }
            return destination;
        }
    }
}
