using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.TypeMappings
{
    internal abstract class AMapping
    {
        public abstract object Map(object data, Type sourcePropType, Type destPropType);
    }
}
