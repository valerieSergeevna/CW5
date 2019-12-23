using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part5
{
    public class Monomial : BaseExpression
    {
        private Number number;
        private Variable variable;

        public Monomial() : base()
        {
        }

        public Monomial(BaseExpression head) : base(head)
        {
        }

        public override double Value
        {
            get { return Math.Round(number.Value * variable.Value, Precision); }
            set { throw new NotSupportedException(); }
        }

        public override double Сoefficient(Variable v)
        {
            if (variable.Equals(v))
            {
                return number.Value;
            }
            return double.NaN;
        }

        public override string Parse(string expr)
        {
            try
            {
                number = new Number(baseExpr);
                expr = number.Parse(expr);
                if (number.Value == 0)
                {
                    throw new Exception("неправильное число " + 0);
                }
                variable = new Variable(baseExpr);
                return variable.Parse(expr);
            }
            catch (Exception ex)
            {
                throw new Exception("неправильно записано " + expr);
            }
       
        }
    }
}
