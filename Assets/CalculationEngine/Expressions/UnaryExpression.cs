using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.CalcEngine
{
    /// <summary>
    /// Unary expression, e.g. +123
    /// </summary>
	class UnaryExpression : Expression
	{
        // ** fields
		Expression	_expr;

        // ** ctor
		public UnaryExpression(Token tk, Expression expr) : base(tk)
		{
			_expr = expr;
		}

        // ** object model
		override public object Evaluate()
		{
            switch (_token.ID)
			{
				case TokenId.ADD:
                    return +(double)_expr;
				case TokenId.SUB:
                    return -(double)_expr;
			}
			throw new ArgumentException("Bad expression.");
		}
        public override Expression Optimize()
        {
            _expr = _expr.Optimize();
            return _expr._token.Type == TokenType.LITERAL
                ? new Expression(this.Evaluate())
                : this;
        }
	}
}
