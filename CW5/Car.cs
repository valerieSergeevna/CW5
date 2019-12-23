using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW5
{
    public class Car
    {
        protected int cylilndersCount;//количество цилиндров
        protected string model;//марка
        protected double power;//мощность

        public int CylilndersCount//свойство для количества цилиндров
        {
            get { return cylilndersCount; }
            private set { }
        }

        public string Model//свойство для марки
        {
            get { return model; }
            private set { }
        }

        public double Power
        {
            get { return power; }
            private set { }
        }//свойство для мощности

        //конструкторы с параметром и без
        public Car()
        {
            cylilndersCount = 0;
            model = "VAZ";
            power = 0.0;
        }

        public Car(int cylilndersCount, string model, double power)
        {
            this.power = power;
            this.cylilndersCount = cylilndersCount;
            this.model = model;
        }
    }

}
