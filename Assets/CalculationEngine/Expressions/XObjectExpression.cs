using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.CalcEngine
{    /// <summary>
    /// Expression that represents an external object.
    /// </summary>
    class XObjectExpression :
        Expression,
        IEnumerable
    {
        object _value;

        // ** ctor
        internal XObjectExpression(object value)
        {
            _value = value;
        }

        // ** object model
        public override object Evaluate()
        {
            // use IValueObject if available
            var iv = _value as IValueObject;
            if (iv != null)
            {
                return iv.GetValue();
            }

            // return raw object
            return _value;
        }
        public IEnumerator GetEnumerator()
        {
            var ie = _value as IEnumerable;
            return ie != null ? ie.GetEnumerator() : null;
        }
    }
}
