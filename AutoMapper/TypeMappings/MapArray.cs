using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeMappings
{
    public class MapArray : AMapping
    {
        public override object Map(object data, Type sourcePropType, Type destPropType)
        {
            Array array = (Array)data;
            int length = array.Length;
            Type destElementType = destPropType.GetElementType();
            Type sourceElementType = sourcePropType.GetElementType();

            PropTypes sourceType = sourceElementType.CheckType();
            PropTypes destType = destElementType.CheckType();
            Type type = Type.GetType("AutoMapper.TypeMappings." + sourceType.ToString());
            AMapping aMapping = (AMapping)Activator.CreateInstance(type);
            Array destArray = Array.CreateInstance(destElementType, length);

            for (int i = 0; i < length; i++)
            {
                var sourceValue = aMapping.Map(array.GetValue(i), sourceElementType, destElementType);
                destArray.SetValue(sourceValue, i);
            }
            return destArray;
        }
    }
}
