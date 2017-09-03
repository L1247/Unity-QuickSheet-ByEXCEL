using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Zirpl.CalcEngine
{
    /// <summary>
    /// Base class that represents parsed expressions.
    /// </summary>
    /// <remarks>
    /// For example:
    /// <code>
    /// Expression expr = scriptEngine.Parse(strExpression);
    /// object val = expr.Evaluate();
    /// </code>
    /// </remarks>
    public class Expression : IComparable<Expression>
    {
        //---------------------------------------------------------------------------
        #region ** fields

        internal Token _token;
        static CultureInfo _ci = CultureInfo.InvariantCulture;

        #endregion

        //---------------------------------------------------------------------------
        #region ** ctors

        internal Expression()
        {
            _token = new Token(null, TokenId.ATOM, TokenType.IDENTIFIER);
        }
        internal Expression(object value)
        {
            _token = new Token(value, TokenId.ATOM, TokenType.LITERAL);
        }
        internal Expression(Token tk)
        {
            _token = tk;
        }

        #endregion

        //---------------------------------------------------------------------------
        #region ** object model

        public virtual object Evaluate()
        {
            if (_token.Type != TokenType.LITERAL)
            {
                throw new ArgumentException("Bad expression.");
            }
            return _token.Value;
        }
        public virtual Expression Optimize()
        {
            return this;
        }

        #endregion

        //---------------------------------------------------------------------------
        #region ** implicit converters

        public static implicit operator string(Expression x)
        {
            var v = x.Evaluate();
            return v == null ? string.Empty : v.ToString();
        }
        public static implicit operator double(Expression x)
        {
            // evaluate
            var v = x.Evaluate();

            // handle doubles
            if (v is double)
            {
                return (double)v;
            }

            // handle booleans
            if (v is bool)
            {
                return (bool)v ? 1 : 0;
            }

            //// handle dates
            //if (v is DateTime)
            //{
            //    return ((DateTime)v).ToOADate();
            //}

            // handle nulls
            if (v == null)
            {
                return 0;
            }

            // handle everything else
            return (double)Convert.ChangeType(v, typeof(double), _ci);
        }
        public static implicit operator bool(Expression x)
        {
            // evaluate
            var v = x.Evaluate();

            // handle booleans
            if (v is bool)
            {
                return (bool)v;
            }

            // handle nulls
            if (v == null)
            {
                return false;
            }

            // handle doubles
            if (v is double)
            {
                return (double)v == 0 ? false : true;
            }

            // handle everything else
            return (double)x == 0 ? false : true;
        }
        public static implicit operator DateTime(Expression x)
        {
            // evaluate
            var v = x.Evaluate();

            // handle dates
            if (v is DateTime)
            {
                return (DateTime)v;
            }

            //// handle doubles
            //if (v is double)
            //{
            //    return DateTime.FromOADate((double)x);
            //}

            // handle everything else
            return (DateTime)Convert.ChangeType(v, typeof(DateTime), _ci);
        }

        #endregion

        //---------------------------------------------------------------------------
        #region ** IComparable<Expression>

        public int CompareTo(Expression other)
        {
            // get both values
            var c1 = this.Evaluate() as IComparable;
            var c2 = other.Evaluate() as IComparable;

            // handle nulls
            if (c1 == null && c2 == null)
            {
                return 0;
            }
            if (c2 == null)
            {
                return -1;
            }
            if (c1 == null)
            {
                return +1;
            }

            // make sure types are the same
            if (c1.GetType() != c2.GetType())
            {
                c1 = Convert.ChangeType(c1, typeof(string),null) as IComparable;
                c2 = Convert.ChangeType(c2, typeof(string),null) as IComparable;
            }

            // compare
            return c1.CompareTo(c2);
        }

        #endregion
    }
}
