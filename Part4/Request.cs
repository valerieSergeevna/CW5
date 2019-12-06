using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part4
{
    class Request
    {
        SolutionType type = new SolutionType();
        bool acceptance;
        double cost = 0;
        public double timeInQueue;
        public double timeProcess;
        public int ID = 0;

        public SolutionType Type{
            get
            {
                return type;
            }
            set {
                type = value;
            }
        }
        public Request()
        {
            this.type = (SolutionType)1;
           // this.Type = Type;
           // ID++;
        }
    }
}
