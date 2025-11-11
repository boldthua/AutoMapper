using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeMappings
{
    public class MapEnumerable : AMapping
    {
        public override object Map(object data, Type sourcePropType, Type destPropType)
        {
            //拆開他，變成 List<T>
            Type sourceTypeDefinition = sourcePropType.GetGenericTypeDefinition(); // List<?>

            //取得泛型類型參數 <?>
            Type sourceGenericArgument = sourcePropType.GetGenericArguments()[0]; // string

            //拆開destType,變成 List<T>
            Type destTypeDefinition = destPropType.GetGenericTypeDefinition();
            Type destGenericArgument = destPropType.GetGenericArguments()[0]; // int

            //創建destList物件
            Type destinationType = destTypeDefinition.MakeGenericType(destGenericArgument);
            object destObj = Activator.CreateInstance(destinationType);
            var addMethod = destObj.GetType().GetMethod("Add"); // 因為是IEnumerable必定有Add

            PropTypes sourceType = sourceGenericArgument.CheckType();
            PropTypes destType = destGenericArgument.CheckType();
            Type type = Type.GetType("AutoMapper.TypeMappings." + sourceType.ToString());
            AMapping aMapping = (AMapping)Activator.CreateInstance(type);
            object destValue = null;
            foreach (var piece in (IEnumerable)data)
            {
                destValue = aMapping.Map(piece, sourceGenericArgument, destGenericArgument);
                addMethod.Invoke(destObj, new object[] { destValue });
            }
            return destObj;
        }
    }
}
