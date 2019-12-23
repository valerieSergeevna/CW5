using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW5
{

    public class Lorry : Car
    {
        protected double carryingCapacity;//грузоподъемность
        public double CarryingCapacity
        {
            get { return carryingCapacity; }
            private set { }
        }
        //смена значения грузоподъемности
        public void ChangeCarryingCapacity(double cap) { carryingCapacity = cap; }
        //смена значения марки
        public void ChangeModel(string model) { this.model = model; }
        //конструктор
        public Lorry(double carryingCapacity, int cylilndersCount, string model, double power) : base(cylilndersCount, model, power)
        {
            this.carryingCapacity = carryingCapacity;
        }

    }
}
