using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExpressionType = AutoMapper.Enums.ExpressionType;

namespace AutoMapper
{
    internal class ExpressionModel
    {
        public ExpressionType type { get; set; }
        public Expression value { get; set; }

        public ExpressionModel(ExpressionType type, Expression value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
