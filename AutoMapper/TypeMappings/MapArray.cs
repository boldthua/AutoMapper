using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeMappings
{
    internal class MapArray : AMapping
    {
        public override object Map(object data, Type sourcePropType, Type destPropType)
        {
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
            return destArray;
        }
    }
}
