using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part4
{
    class SystemImitation
    {
        enum SystemState {S0,S1,S2,S3 }; //0-both free, 1 - first free, 2- second free, 3- both busy 
        private SystemState currentSystemState = SystemState.S0;
        //  private Queue<Request> requestQueue = new Queue<Request>();
        private const int QLength = 10;
        private Request[] queue = new Request[QLength];
        private Random R;
        private double costs; // издержки
        private double profit;
        private double um = 1.7;
        private double timeStep;
        private double lambda = 4.8;
        SolutionType CurType = new SolutionType();
        public double[] timeOfFinishProcessingReq = new double[2];
        private int reqEntCount = 0;
        private int reqDelinedcCount = 0;
        private int reqAcceptedCount = 0;
        private int CurrentReq1;
        private int CurrentReq2;
        public int queueLength = 0;
        private int ID = 0;

        public double Costs
        {
            get { return costs; }
            set { }
        }

        public double Id
        {
            get { return ID; }
            set { }
        }

        public double Profit
        {
            get { return profit; }
            set { }
        }

        public SystemImitation()
        {
            
        }

        double GetServiceTime()
        {
            double r = R.NextDouble();
            return (-1 / um * Math.Log(1 - r, Math.E));
        }

        SystemState GetCondition()

        {
            if (timeOfFinishProcessingReq[0] > 0)
                if (timeOfFinishProcessingReq[1] > 0)
                    return SystemState.S3;
                else return SystemState.S2;
            else if  (timeOfFinishProcessingReq[1] > 0)
                    return SystemState.S1;
                else return SystemState.S0;
        }


        bool isRequested(out int type)
         {
            R = new Random();
            type = R.Next(7);
            double r = R.NextDouble();
            if (r < (timeStep * lambda))
            { return true; }
            return false;

        }

        void Dequeue() 
        {
            
            int i = 0;
         
            while (queue[i + 1] != null)
            {
                queue[i] = queue[i + 1];
                i++;
            }
            queue[i] = null;     
           
        }
        public void Imitate(double timeStep, out int isR)
        {
            this.timeStep = timeStep;
     
            int isReq = 0;
            SystemState State = GetCondition();
                
                if (queueLength > 0)
                {
                    
                   // queue[queueLength - 1].timeInQueue += timeStep;
                    if (timeOfFinishProcessingReq[0] <= 0)
                    {
                        timeOfFinishProcessingReq[0] = GetServiceTime() * 2;
                        costs += 3 * timeOfFinishProcessingReq[1] + 200 + 500;
                        profit += 1.9 * 7000 + 3 * timeOfFinishProcessingReq[1] - costs;
                        Dequeue();
                        queueLength--;
                    }

                    if (timeOfFinishProcessingReq[1] <= 0)
                    {
                        timeOfFinishProcessingReq[1] = GetServiceTime();
                        costs += 2* timeOfFinishProcessingReq[1] + 200 + 500;
                        profit += (1 + 0.1 * ((int)CurType))*5000 + 2 * timeOfFinishProcessingReq[1] - costs;
                        Dequeue();
                        queueLength--;


                    } 
                   
                }

            if (isRequested(out int type))
            {
                ID++;

               
                CurType = (SolutionType)type;
                reqEntCount++;
                if (queueLength < QLength)
                {
                    reqAcceptedCount++;
                     isReq = 1;
                    if (((State < SystemState.S1) || (State == SystemState.S0)) && ((SolutionType)type == 0))
                    {

                        if (timeOfFinishProcessingReq[0] <= 0)
                        {
                            timeOfFinishProcessingReq[0] = GetServiceTime() * 2;
                            // queue[queueLength - 1].timeProcess = timeOfFinishProcessingReq[1];
                             CurrentReq1 = ID;
                        }
                    }
                    else if (((State == SystemState.S2) || (State == SystemState.S0)) && ((SolutionType)type >= (SolutionType)1) && ((SolutionType)type <= (SolutionType)7))
                    {

                        if (timeOfFinishProcessingReq[1] <= 0)
                        {
                            timeOfFinishProcessingReq[1] = GetServiceTime();
                            CurrentReq2 = ID;
                            //  CurrentReq2 = queueLength - 1;
                            //queue[queueLength - 1].timeProcess = timeOfFinishProcessingReq[1];

                        }
                    }
                    else
                    {C:\Users\Lera\source\repos\CW5\Part4\Form1.cs
                        queueLength++;
                        queue[queueLength - 1].Type = CurType;
                        queue[queueLength - 1].ID++;
                    }
                }
                else { reqDelinedcCount++;  isReq = 2; }
            }
             
                if (timeOfFinishProcessingReq[1] > 0)
                {
                    timeOfFinishProcessingReq[1] -= timeStep;

                }
                if (timeOfFinishProcessingReq[0] > 0)
                    timeOfFinishProcessingReq[0] -= timeStep;


            isR = isReq;
        }
    }
}
