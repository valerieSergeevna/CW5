using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

namespace Part4
{
    class SystemImitation
    {

        private const int QLength = 10;
        private List<Request> queue = new List<Request>();
        private Random R;
        private double costs; // издержки
        private double profit; // прибыль

        private double timeStep;
        private double lambda = 4.8;

        private int reqEntCount = 0;
        private int reqComplCount = 0;
        private int reqDelinedcCount = 0;
        private int reqAcceptedCount = 0;

        public double CurCost
        {
            get;
            private set;
        }

        public int Declined{
            get { return reqDelinedcCount; }
            private set { }
            }

        public int Accepted
        {
            get { return reqAcceptedCount; }
            private set { }
        }

        public int Compl
        {
            get { return reqComplCount; }
            private set { }
        }


        // команды
        Request Team1;
        Request Team2;

        public int queueLength;
        private int ID = 0;


        public double Costs
        {
            get { return costs; }
            set { }
        }

        public int Id
        {
            get { return ID; }
            set { }
        }

        public double Profit
        {
            get { return profit; }
            set { }
        }


        // расчет времени на обработку заявки
        double GetServiceTime(SolutionType type)
        {
            R = new Random();
            double r = R.NextDouble();
            if (type == (SolutionType)0)
                return (-1 / (Math.Log(1 - r, Math.E)));
            else
                return (-1 / ((0.5 + (int)(SolutionType)type / 10) * Math.Log(1 - r, Math.E)));


        }


        // метод для решения получили ли мы заявку или нет в момент времени и какого типа
        bool isRequested(out int type)
        {
            R = new Random();
            type = R.Next(0, 7);
            double r = R.NextDouble();
            if (r < (timeStep * lambda / 5))
            { return true; }
            return false;

        }

        public void Imitate(double timeStep, out int isR, ref string ID1, ref
        string TType, ref string ReqType)
        {
            this.timeStep = timeStep;

            int isReq = 0;
            // если первая кманда свободна
            if (Team1 != null && Team1.curTime <= 0)
            {
                Request temp1 = Team1;
                // извлечение соответствующей заявки из очереди
                Team1 = queue.FirstOrDefault(req => req.Type == (SolutionType)0);
                queue.Remove(Team1);
                reqComplCount++;
                ID1 += temp1.ID + " ";
                TType += temp1.Type + " ";
                temp1.Cost = 1 + 0.9 * 40000 + 30 * temp1.allTime;
                CurCost = temp1.Cost;
                costs += 13000 * 0.3 * temp1.allTime  + 13000;
                profit += 1 + 0.9 * 40000 + 30 * temp1.allTime - 13000 * 0.3 * temp1.allTime  + 13000;
            }
            // вторая команда свободна
            if (Team2 != null && Team2.curTime <= 0)
            {
                Request temp2 = Team2;
                Team2 = queue.FirstOrDefault(req => req.Type <= (SolutionType)7 && req.Type >= (SolutionType)1);
                queue.Remove(Team2);
                reqComplCount++;
                ID1 += temp2.ID + " ";
                TType += temp2.Type + " ";
                temp2.Cost = (1 + 0.1 * ((int)temp2.Type)) * 30000 + 20 * temp2.allTime;
                CurCost = temp2.Cost;
                costs += 0.1 * 10000 * temp2.allTime  + 10000;
                profit += (1 + 0.1 * ((int)temp2.Type)) * 30000 + 20 * temp2.allTime - 0.1 * 10000 * temp2.allTime  + 10000;

            }
            // получили ли заявку
            if (isRequested(out int type) && ((SolutionType)type >= (SolutionType)0) && ((SolutionType)type <= (SolutionType)7))
            {
                reqEntCount++;
                ID++;
                // если в очереди меньше максимальной длинны очереди элементов, то принимаем ее
                if (queue.Count <= QLength)
                {
                    reqAcceptedCount++;
                    isReq = 1;

                    Request newRequest = new Request((SolutionType)type, ID, GetServiceTime((SolutionType)type));
                    ReqType = newRequest.Type.ToString();
                    queue.Add(newRequest);
                    // если команда свободна и на очереди нет заявок данного типа, то мы сразу помещаем заявку на обработку
                    if (Team1 == null && type == 0)

                    {
                        Team1 = queue.FirstOrDefault(req => req.Type == (SolutionType)0);
                        queue.Remove(newRequest);
                    }

                    if (Team2 == null && type <= 7 && type >= 1)

                    {
                        Team2 = queue.FirstOrDefault(req => req.Type <= (SolutionType)7 && req.Type >= (SolutionType)1);
                        queue.Remove(newRequest);
                    }


                }
                else
                {
                    reqDelinedcCount++;
                    isReq = 2;
                }
            }
            // уменьшаем время работы команд над заявкой
            if (Team1 != null)
                Team1.curTime -= timeStep;
            if (Team2 != null)
                Team2.curTime -= timeStep;

            isR = isReq;

        }
    }
}

