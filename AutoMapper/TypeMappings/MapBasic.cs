using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeMappings
{
    public class MapBasic : AMapping
    {
        public override object Map(object data, Type sourcePropType, Type destPropType)
        {
            Type[] types = { typeof(int), typeof(double), typeof(decimal), typeof(bool), typeof(float), typeof(long), typeof(short) };
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
            return data;
        }
    }
}
