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
       // public bool acceptance = false;
        double cost = 0;
        public double curTime;
        public double allTime;
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

        public double Cost
        {
            get; set;
        }

        public Request()
        {

        }

            public Request(SolutionType type, int ID, double allTime )
        {
            this.type = type;
            this.ID = ID;
            
            this.allTime = allTime;
            this.curTime = this.allTime;
            // acceptance = true;
            // cost = 0;
            //       timeInQueue;
            //     timeProcess;
            // ID++;
        }
    }
}
