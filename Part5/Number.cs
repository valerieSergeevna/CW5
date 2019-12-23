using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part5
{
    public class Number : BaseExpression
    {
        private double val = double.NaN;
        private int sign = 1;
        private int fractioanalPosition = 0;//знаки после запятой


        public Number() : base()
        {
        }

        public Number(BaseExpression head) : base(head)
        {
        }

        public override double Value
        {
            get { return Math.Round(val * sign, Precision); }
            set { throw new NotSupportedException(); }
        }

        public override string Parse(string expression)
        {
            string number = base.Parse(expression);

            if (string.IsNullOrEmpty(number))
            {
                throw new Exception("Неапрвильно записано");
            }
            if ((number[0] == '-') || (number[0] == '+'))//задаем знак для числа
            {
                if (number[0] == '-')
                {
                    sign = -1;
                }
                if (number.Length > 1)
                {
                    number = base.Parse(number.Substring(1));
                }
                else
                {
                    //если после знака нет числа
                    throw new Exception("неправильно записано " + number);
                }
            }
            if (char.IsNumber(number, 0))//если это число, то записываем его
            {
                val = Convert.ToDouble(number[0].ToString());
                //если длинна числа больше 1, то парсим дальше
                return number.Length > 1 ? WriteFullNumber(number.Substring(1)) : "";
            }

            if ((number[0] == ',') || (number[0] == '.'))
            {
                if (number.Length <= 1) throw new Exception("неправильно записано " + number);
                val = 0;
                fractioanalPosition = 1;
                return WriteFractionalPart(number.Substring(1));
            }
            val = 1;
            return number;
        }

        public override string ToString()
        {
            return val.ToString();
        }

        private string WriteFullNumber(string num)//запись всего числа
        {
            if (char.IsNumber(num, 0))
            {
                val = Math.Round((10 * val) + Convert.ToDouble(num[0].ToString()), Precision);
                return num.Length > 1 ? WriteFullNumber(num.Substring(1)) : "";
            }

            if ((num[0] != ',') && (num[0] != '.'))
                return num;
            if (num.Length <= 1)
                throw new Exception("неправильно записано " + num);
            fractioanalPosition = 1;
            //если число дробное, то записываем то, что после . или ,
            return WriteFractionalPart(num.Substring(1));
        }

        private string WriteFractionalPart(string num)
        {
            if (!char.IsNumber(num, 0))
                throw new Exception("неправильно записано " + num);
            val += Math.Round(Convert.ToDouble(num[0].ToString()) / (10 * fractioanalPosition), Precision);
            fractioanalPosition++;
            //если дробная часть состоит изи более, чем одноой цифры
            return num.Length > 1 ? WriteFullFractionalPart(num.Substring(1)) : "";
        }

        private string WriteFullFractionalPart(string expr)
        {
            if (!char.IsNumber(expr, 0)) return expr;
            val += Math.Round(Convert.ToDouble(expr[0].ToString()) / (10 * fractioanalPosition), Precision);
            fractioanalPosition++;
            return expr.Length > 1 ? WriteFullFractionalPart(expr.Substring(1)) : "";
        }
    }
}
