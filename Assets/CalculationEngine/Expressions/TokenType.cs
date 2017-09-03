using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.CalcEngine
{
    /// <summary>
    /// Token types (used when building expressions, sequence defines operator priority)
    /// </summary>
    internal enum TokenType
    {
        LOGICAL , // &&,||
        COMPARE , // < > = <= >= ==
        ADDSUB  , // + -
        MULDIV  , // * /
        POWER   , // ^
        GROUP   , // ( )         , .
        LITERAL , // 123.32      , "Hello" , etc.
        IDENTIFIER  // functions, external objects, bindings
    }
}
