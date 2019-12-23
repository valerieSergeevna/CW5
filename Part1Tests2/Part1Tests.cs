using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CW5;

namespace Part1Tests
{
    [TestClass]
    public class Part1Tests
    {
        [TestMethod]
        public void changeModelTest()
        {
            Lorry car = new Lorry(5,3, "mitsubishi", 9);
            car.ChangeModel("nissan");
            Assert.AreEqual("nissan", car.Model);
        }

       [TestMethod]
        public void ChangeCarryingCapacityTest()
        {
            Lorry car = new Lorry(5, 3, "mitsubishi", 9);
            car.ChangeCarryingCapacity(10);
            Assert.AreEqual(10, car.CarryingCapacity);
        }
    }
}
