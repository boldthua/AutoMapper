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
                Type[] types = { typeof(int), typeof(double), typeof(decimal), typeof(bool), typeof(float), typeof(long), typeof(short) };
                if (destProperty == null)
                    continue;
                object data = property.GetValue(source);
                // 判斷property是否為容器 1.getInterface 找出這個propertyType是實作了哪些interface
                //                       2.又用IsGenericType判斷這些interface中哪些是泛型型別
                //                       3.最後用GetGenericTypeDefinition取出泛型定義符合typeof(IEnumerable<>)

                PropTypes sourceType = sourcePropType.CheckType();
                PropTypes destType = destPropType.CheckType();
                Type type = Type.GetType(sourceType.ToString());
                AMapping aMapping = (AMapping)Activator.CreateInstance(type);


                switch (sourceType)
                {
                    case PropTypes.Array:
                        Array array = (Array)data;
                        int length = array.Length;
                        Type destElementType = destPropType.GetElementType();
                        Type sourceElementType = sourcePropType.GetElementType();

                        var parseMethod = destElementType.GetMethod("Parse", new Type[] { sourceElementType });

                        Array destArray = Array.CreateInstance(destElementType, length);

                        for (int i = 0; i < length; i++)
                        {
                            var sourceValue = array.GetValue(i);
                            var result = parseMethod.Invoke(null, new object[] { sourceValue });
                            destArray.SetValue(result, i);
                        }
                        data = destArray;
                        break;

                    case PropTypes.Enumerable:
                        //拆開他，變成 List<T>
                        Type unknownType = property.PropertyType;
                        Type sourceTypeDefinition = unknownType.GetGenericTypeDefinition(); // List<?>

                        //取得泛型類型參數 <?>
                        Type sourceGenericArgument = unknownType.GetGenericArguments()[0]; // string

                        //拆開destType,變成 List<T>
                        Type destTypeDefinition = destPropType.GetGenericTypeDefinition();
                        Type destGenericArgument = destPropType.GetGenericArguments()[0]; // int


                        //創建destList物件
                        Type destinationType = destTypeDefinition.MakeGenericType(destGenericArgument);
                        object destObj = Activator.CreateInstance(destinationType);
                        var addMethod = destObj.GetType().GetMethod("Add"); // 因為是IEnumerable必定有Add


                        var EnumerParseMethod = destGenericArgument.GetMethod("Parse", new Type[] { sourceGenericArgument });
                        // 大部分基本型別都有Parse可用

                        foreach (var piece in (IEnumerable)data)
                        {
                            object destValue = EnumerParseMethod.Invoke(null, new object[] { piece });
                            addMethod.Invoke(destObj, new object[] { destValue });
                        }
                        data = destObj;
                        break;

                    case PropTypes.Class:
                        //data = Map<destPropType>(property.GetValue(source));



                        break;
                    case PropTypes.Enum:
                        if (destType != PropTypes.Enum)
                        {
                            //Enum轉 string/int
                            if (destPropType == typeof(int))
                                data = (int)(Enum.Parse(sourcePropType, data.ToString()));
                            else
                                data = data.ToString();
                        }
                        break;
                    case PropTypes.Basic:
                        if (sourcePropType != destPropType)
                        {
                            // string/int 轉 enum
                            if (destPropType.IsEnum)
                            {
                                data = Enum.Parse(destPropType, data.ToString());
                            }
                            else if (types.Contains(destPropType)) // string 轉 int
                            {
                                var destParseMethod = destPropType.GetMethod("Parse", new Type[] { typeof(string) });
                                data = destParseMethod.Invoke(null, new object[] { data.ToString() });
                            }
                            else // int 轉 string
                                data = data.ToString();
                        }
                        break;
                }
                destProperty.SetValue(dest, data);
            }
            return dest;

        }

        private object BasicMap(Type destPropType, Type sourcePropType, object data)
        {
            Type[] types = { typeof(int), typeof(double), typeof(decimal), typeof(bool), typeof(float), typeof(long), typeof(short) };
            if (sourcePropType != destPropType)
            {
                //Enum轉 Enum / string/int 轉 enum
                if (destPropType.IsEnum)
                {
                    data = Enum.Parse(destPropType, data.ToString());
                }
                else if (sourcePropType.IsEnum)
                {
                    if (destPropType == typeof(int))
                        data = (int)(Enum.Parse(sourcePropType, data.ToString()));
                    else
                        data = data.ToString();
                }
                else if (types.Contains(destPropType))
                {
                    var parseMethod = destPropType.GetMethod("Parse", new Type[] { typeof(string) });
                    data = parseMethod.Invoke(null, new object[] { data.ToString() });
                }
                else
                    data = data.ToString();
            }
            return data;
        }
    }



    // observableCollection
}

