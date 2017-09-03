using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.CalcEngine
{
    /// <summary>
    /// Delegate that represents CalcEngine functions.
    /// </summary>
    /// <param name="parms">List of <see cref="Expression"/> objects that represent the
    /// parameters to be used in the function call.</param>
    /// <returns>The function result.</returns>
    public delegate object CalcEngineFunction(List<Expression> parms);
}
