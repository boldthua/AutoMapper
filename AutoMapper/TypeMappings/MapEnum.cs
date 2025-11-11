using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeMappings
{
    public class MapEnum : AMapping
    {
        public override object Map(object data, Type sourcePropType, Type destPropType)
        {
            object mappingData = null;
            if (!destPropType.IsEnum)
            {
                //Enum轉 string/int
                if (destPropType == typeof(int))
                    mappingData = (int)(Enum.Parse(sourcePropType, data.ToString()));
                else
                    mappingData = data.ToString();
            }
            return mappingData;
        }
    }
}
