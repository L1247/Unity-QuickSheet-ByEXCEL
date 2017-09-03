using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.CalcEngine
{
    /// <summary>
    /// Represents a node in the expression tree.
    /// </summary>
    internal class Token
    {
        // ** fields
        public TokenId ID;
        public TokenType Type;
        public object Value;

        // ** ctor
        public Token(object value, TokenId id, TokenType type)
        {
            Value = value;
            ID = id;
            Type = type;
        }
    }
}
