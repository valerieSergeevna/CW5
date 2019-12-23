using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part5
{
    public class Expression : BaseExpression
    {
        protected List<Monomial> monomials = new List<Monomial>();//одночлены

        protected internal List<Constraint> constraints = new List<Constraint>();//ограничения

        public Expression() : base()
        { }

        private void VariablesContains(Constraint excon)
        {
            foreach (Variable v in excon.Variables)
            {
                if (Variables.IndexOf(v) < 0)
                {
                    throw new Exception("Неизвестная переменная " + v);
                }
            }
        }

        public Expression(string[] input)
        {
            try
            {
                string CurExpr = Parse(input[0]);
                //проверка на пустую строку
                if (!string.IsNullOrEmpty(CurExpr))
                {
                    throw new Exception("Неправильно введенная строка (" + input[0] + ")");
                }
                constraints.Clear();
                //если есть ограничения на больше или равно или на меньше или равно, то исключние
                var maxCount = input
                                .Skip(1)
                                .Where(x => x.Contains("<="));
                var minCount = input
                                .Skip(1)
                                .Where(x => x.Contains(">="));
                if (maxCount.Count() != 0 && minCount.Count() != 0)
                {
                    throw maxCount.Count() > minCount.Count() ? new Exception("Неправильное выражение для нахождения максимума (" + minCount.First() + ")") : new Exception("Неправильное выражение для нахождения минимума (" + maxCount.First() + ")");
                }

                foreach (var line in input.Skip(1))
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    Constraint сonstraint = new Constraint();
                    CurExpr = сonstraint.Parse(line);//парсим ограничение
                    if (!string.IsNullOrEmpty(CurExpr.Trim()))
                    {
                        throw new Exception("Неправильное ограничение(" + CurExpr.Trim() + ")");
                    }
                    VariablesContains(сonstraint);
                    constraints.Add(сonstraint);
                }
                if (constraints.Count == 0)
                {
                    throw new Exception("Должно быть определено хотя бы одно ограничение");
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override double Value
        {
            get
            {
                double val = monomials.Sum(m => m.Value);
                return Math.Round(val, Precision);
            }
            set { throw new NotSupportedException(); }
        }

        public override string Parse(string expr)
        {
            monomials.Clear();
            string strExpr = base.Parse(expr);
            if (string.IsNullOrEmpty(strExpr)) throw new Exception("неправильно записано " + strExpr);
            Monomial monomial = new Monomial(this);
            //парсинг для одночленов и добавлем первый
            strExpr = monomial.Parse(strExpr);
            monomials.Add(monomial);
            //убираем ненужные знаки
            strExpr = base.Parse(strExpr);
            //парсим все выражение до ограничения
            while (!string.IsNullOrEmpty(strExpr) && !strExpr.StartsWith("<=") && !strExpr.StartsWith(">="))
            {
                if ((strExpr[0] != '+') && (strExpr[0] != '-'))
                {
                    throw new Exception("неправильно записано " + strExpr);
                }

                if (strExpr.Length <= 1)
                {
                    throw new Exception("неправильно записано " + strExpr);
                }

                if (strExpr[0] == '+')
                {
                    strExpr = base.Parse(strExpr.Substring(1));
                }
                //парсим одночлены в выражении
                Monomial mon = new Monomial(this);
                strExpr = base.Parse(mon.Parse(strExpr));
                monomials.Add(mon);
            }
            return strExpr;
        }

        public override double Сoefficient(Variable v)
        {
            return monomials.Select(m => m.Сoefficient(v))
                         .FirstOrDefault(c => !double.IsNaN(c));
        }
    }
}
