using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.CalcEngine
{
    /// <summary>
    /// Interface supported by external objects that have to return a value
    /// other than themselves (e.g. a cell range object should return the 
    /// cell content instead of the range itself).
    /// </summary>
    public interface IValueObject
    {
        object GetValue();
    }
}
