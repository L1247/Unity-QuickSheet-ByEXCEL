using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Zirpl.CalcEngine
{
    /// <summary>
    /// Expression based on an object's properties.
    /// </summary>
    class BindingExpression : Expression
    {
        CalculationEngine _ce;
        List<BindingInfo> _bindingPath;
        CultureInfo _ci;

        // ** ctor
        internal BindingExpression(CalculationEngine engine, List<BindingInfo> bindingPath, CultureInfo ci)
        {
            _ce = engine;
            _bindingPath = bindingPath;
            _ci = ci;
        }

        // ** object model
        override public object Evaluate()
        {
            return GetValue(_ce.DataContext);
        }

        // ** implementation
        object GetValue(object obj)
        {
            const BindingFlags bf =
                BindingFlags.IgnoreCase |
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.Static;

            foreach (var bi in _bindingPath)
            {
                // get property
                if (bi.PropertyInfo == null)
                {
                    bi.PropertyInfo = obj.GetType().GetProperty(bi.Name, bf);
                }

                // get object
                obj = bi.PropertyInfo.GetValue(obj, null);

                // handle indexers (lists and dictionaries)
                if (bi.Parms != null && bi.Parms.Count > 0)
                {
                    // get indexer property (always called "Item")
                    if (bi.PropertyInfoItem == null)
                    {
                        bi.PropertyInfoItem = obj.GetType().GetProperty("Item", bf);
                    }

                    // get indexer parameters
                    var pip = bi.PropertyInfoItem.GetIndexParameters();
                    var list = new List<object>();
                    for (int i = 0; i < pip.Length; i++)
                    {
                        var pv = bi.Parms[i].Evaluate();
                        pv = Convert.ChangeType(pv, pip[i].ParameterType, _ci);
                        list.Add(pv);
                    }

                    // get value
                    obj = bi.PropertyInfoItem.GetValue(obj, list.ToArray());
                }
            }

            // all done
            return obj;
        }
    }
}
