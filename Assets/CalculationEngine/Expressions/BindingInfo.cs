using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Zirpl.CalcEngine
{
    /// <summary>
    /// Helper used for building BindingExpression objects.
    /// </summary>
    class BindingInfo
    {
        public BindingInfo(string member, List<Expression> parms)
        {
            Name = member;
            Parms = parms;
        }
        public string Name { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public PropertyInfo PropertyInfoItem { get; set; }
        public List<Expression> Parms { get; set; }
    }
}
