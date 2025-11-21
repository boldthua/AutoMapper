using AutoMapper.TypeMappings;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal class Mapper
    {
        public TDestination Map<TDestination, TSource>(object source, Action<MappingExpression<TDestination, TSource>> exp = null) where TDestination : class, new() where TSource : class, new()
        {
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();
            TDestination dest = new TDestination();

            // 去抓source所有property的name來找dest內有沒有property名稱一樣的，有就塞進去。
            foreach (var property in sourceProperties)
            {
                PropertyInfo destProperty = dest.GetType().GetProperty(property.Name);

                StuffProperty<TDestination>(destProperty, property, dest, source);
            }


            // 處理名稱不相同的情況
            // 從Action<Expression>中找到source的name和dest的name，抓出對應的property
            // 把sourceProperty的值塞進destProperty去

            MappingExpression<TDestination, TSource> mExp = new MappingExpression<TDestination, TSource>();
            if (exp != null)
            {
                exp.Invoke(mExp);
                foreach (var set in mExp.mapping)
                {
                    PropertyInfo destProperty = dest.GetType().GetProperty(set.Key);
                    PropertyInfo sourceProperty = source.GetType().GetProperty(set.Value);

                    StuffProperty<TDestination>(destProperty, sourceProperty, dest, source);
                }
            }
            return dest;
        }

        private void StuffProperty<TDestination>(PropertyInfo destProperty, PropertyInfo property, TDestination dest, object source)
        {
            if (destProperty == null)
                return;
            Type destPropType = destProperty.PropertyType;
            Type sourcePropType = property.PropertyType;

            object data = property.GetValue(source);
            if (destPropType == sourcePropType)
            {
                destProperty.SetValue(dest, property.GetValue(source));
                return;
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

    }



    // observableCollection
}

