using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part5
{
    public class Constraint : Expression
    {
        private Number сonstraint;

        public override double Value
        {
            get { return сonstraint.Value; }
            set { throw new NotSupportedException(); }
        }

        public override string Parse(string expression)
        {
            string strExpr = base.Parse(expression);
            if (!strExpr.StartsWith("<=") && !strExpr.StartsWith(">=") || (strExpr.Length < 3))//если нет знака больше(меньше) или равно и есть ли число
            {
                throw new Exception("неправильно записано " + strExpr);
            }
            сonstraint = new Number();
            strExpr = сonstraint.Parse(strExpr.Substring(2));//парсим число после знака
            if (сonstraint.Value == 0)
            {
                throw new Exception("неправильное число " + 0);
            }
            return strExpr;
        }
    }
}
