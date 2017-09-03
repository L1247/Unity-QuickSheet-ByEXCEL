using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.CalcEngine
{
    /// <summary>
    /// Binary expression, e.g. 1+2
    /// </summary>
    class BinaryExpression : Expression
    {
        // ** fields
        Expression _lft;
        Expression _rgt;

        // ** ctor
        public BinaryExpression(Token tk, Expression exprLeft, Expression exprRight)
            : base(tk)
        {
            _lft = exprLeft;
            _rgt = exprRight;
        }

        // ** object model
        override public object Evaluate()
        {
            // handle comparisons
            if (_token.Type == TokenType.COMPARE)
            {
                var cmp = _lft.CompareTo(_rgt);
                switch (_token.ID)
                {
                    case TokenId.GT: return cmp > 0;
                    case TokenId.LT: return cmp < 0;
                    case TokenId.GE: return cmp >= 0;
                    case TokenId.LE: return cmp <= 0;
                    case TokenId.EQ: return cmp == 0;
                    case TokenId.NE: return cmp != 0;
                }
            }

            // handle everything else
            switch (_token.ID)
            {
                case TokenId.AND:
                    return (bool)_lft && (bool)_rgt;
                case TokenId.OR:
                    return (bool)_lft || (bool)_rgt;
                case TokenId.ADD:
                    return (double)_lft + (double)_rgt;
                case TokenId.SUB:
                    return (double)_lft - (double)_rgt;
                case TokenId.MUL:
                    return (double)_lft * (double)_rgt;
                case TokenId.DIV:
                    return (double)_lft / (double)_rgt;
                case TokenId.DIVINT:
                    return (double)(int)((double)_lft / (double)_rgt);
                case TokenId.MOD:
                    return (double)(int)((double)_lft % (double)_rgt);
                case TokenId.POWER:
                    var a = (double)_lft;
                    var b = (double)_rgt;
                    if (b == 0.0) return 1.0;
                    if (b == 0.5) return Math.Sqrt(a);
                    if (b == 1.0) return a;
                    if (b == 2.0) return a * a;
                    if (b == 3.0) return a * a * a;
                    if (b == 4.0) return a * a * a * a;
                    return Math.Pow((double)_lft, (double)_rgt);
            }
            throw new ArgumentException("Bad expression.");
        }
        public override Expression Optimize()
        {
            _lft = _lft.Optimize();
            _rgt = _rgt.Optimize();
            return _lft._token.Type == TokenType.LITERAL && _rgt._token.Type == TokenType.LITERAL
                ? new Expression(this.Evaluate())
                : this;
        }
    }
}
