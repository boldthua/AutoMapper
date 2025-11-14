using AutoMapper.TypeMappings;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal class Mapper
    {
        public TDestionation Map<TDestionation>(object source) where TDestionation : class, new()
        {
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();
            TDestionation dest = new TDestionation();

            foreach (var property in sourceProperties)
            {
                PropertyInfo destProperty = dest.GetType().GetProperty(property.Name);
                Type destPropType = destProperty.PropertyType;
                Type sourcePropType = property.PropertyType;
                if (destProperty == null)
                    continue;
                object data = property.GetValue(source);
                if (destPropType == sourcePropType)
                {
                    destProperty.SetValue(dest, property.GetValue(source));
                    continue;
                }
                // 判斷property是否為容器 1.getInterface 找出這個propertyType是實作了哪些interface
                //                       2.又用IsGenericType判斷這些interface中哪些是泛型型別
                //                       3.最後用GetGenericTypeDefinition取出泛型定義符合typeof(IEnumerable<>)

                PropTypes sourceType = sourcePropType.CheckType();
                PropTypes destType = destPropType.CheckType();
                Type type = Type.GetType("AutoMapper.TypeMappings." + sourceType.ToString());
                AMapping aMapping = (AMapping)Activator.CreateInstance(type);
                data = aMapping.Map(data, sourcePropType, destPropType);

                destProperty.SetValue(dest, data);
            }
            return dest;

        }

    }



    // observableCollection
}

