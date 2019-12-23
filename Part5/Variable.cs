using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part5
{
    public class Variable : BaseExpression, IComparable<Variable>, IEquatable<Variable>
    {
        private double val = double.NaN;
        private string name = null;

        public Variable() : base()
        {
        }

        public Variable(BaseExpression head) : base(head)
        {
        }

        public override double Value
        {
            get { return val; }
            set { val = value; }
        }

        public override string Parse(string expession)
        {
            string strVar = base.Parse(expession);//убираем лишние знаки
            if (string.IsNullOrEmpty(strVar))
                throw new Exception("Пустая строка");
            if (!char.IsLetter(strVar[0]))
                throw new Exception("Не буква или число");
            name = strVar.Substring(0, 1).ToLower();
            strVar = strVar.Length > 1 ? SeveralVar(strVar.Substring(1)) : "";
            if (baseExpr.Variables.Contains(this))
            {
                throw new Exception("Уже есть такая переменная " + name + " ");
            }

            baseExpr.Variables.Add(this);//добавляем переменную в список переменных
            return strVar;
        }


        private string SeveralVar(string expession)//если переменная задана больше, чем 1 символом, то пытаемся ее запомнить 
        {
            if (!char.IsLetterOrDigit(expession, 0))
                return expession;
            name += expession.Substring(0, 1).ToLower();
            if (expession.Length > 1)
            {
                return SeveralVar(expession.Substring(1));
            }
            expession = "";
            return expession;
        }

        public int CompareTo(Variable input)//переопределение сравнение для класса, чтобы можно было отсортироать переменные
        {
            return string.Compare(name, input.name, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(Variable input)
        {
            return string.Compare(name, input.name, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public override string ToString()
        {
            return name;
        }

    }
}
