using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part5
{
    public abstract class BaseExpression
    {
        protected static int precision = 10;//точность
        protected List<Variable> variables;//переменные 
        protected BaseExpression baseExpr;//указатель на себя

        public BaseExpression()
        {
            variables = new List<Variable>();
            baseExpr = this;
        }

        public BaseExpression(BaseExpression head)
        {
            baseExpr = head;
        }

        public static int Precision
        {
            get { return precision; }
            set { precision = Math.Min(20, Math.Max(1, value)); }
        }

        public virtual string Parse(string expresion)//убираем из выражений ненужные знаки табуляции, пробелов, перехода на другую строку и тд
        {
            return expresion.TrimStart(new char[] { ' ', '\n', '\r', '\t' });
        }

        public List<Variable> Variables
        {
            get
            {
                variables.Sort();
                return variables;
            }
            set { variables = value; }
        }

        public void Clear()
        {
            variables.Clear();
        }

        public abstract double Value
        {
            get; set;
        }

        public virtual double Сoefficient(Variable var)
        {
            return 0;
        }


    }
}
