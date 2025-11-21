using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeMappings
{
    public class MapClass : AMapping
    {
        public override object Map(object data, Type sourcePropType, Type destPropType)
        {
            Mapper mapper = new Mapper();

            var mapMethod = typeof(Mapper).GetMethod("Map");
            var mapGenericMethod = mapMethod.MakeGenericMethod(new Type[] { destPropType, sourcePropType });
            object mappingObject = mapGenericMethod.Invoke(mapper, new object[] { data, null });
            // object mappingObject = mapper.Map<(Class)destPropType>(data);
            return mappingObject;
        }
    }
}
