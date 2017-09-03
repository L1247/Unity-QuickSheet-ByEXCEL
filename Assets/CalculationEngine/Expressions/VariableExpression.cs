using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.CalcEngine
{
    /// <summary>
    /// Simple variable reference.
    /// </summary>
    class VariableExpression : Expression
    {
        Dictionary<string, object> _dct;
        string _name;

        public VariableExpression(Dictionary<string, object> dct, string name)
        {
            _dct = dct;
            _name = name;
        }
        public override object Evaluate()
        {
            return _dct[_name];
        }
    }
}
