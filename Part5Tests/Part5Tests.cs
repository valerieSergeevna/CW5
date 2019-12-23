using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Part5;

namespace Part5Tests
{
    [TestClass]
    public class Part5Tests
    {
     
        [TestMethod]
        public void MaxResultTest()
        {
            var functionConstraint = new[] { "2x1+4x2+x3", "x1+x2+2x3>=500", "10x1+8x2+5x3>=2000", "2x1+x2>=100" };
            SimplexMethod optimize = new SimplexMethod(functionConstraint);
            double result = optimize.SimplexCalculate();
            Assert.AreEqual(400, result);
        }

        [TestMethod]
        public void MinResultTest()
        {
      
            var functionConstraint = new[] { "4x1 + 9x2", "11x1 + 3x2 <= 45", "4x1 + 3x2 <= 24", "4x1 + 15x2 <= 48" };
            SimplexMethod optimize = new SimplexMethod(functionConstraint);
            var result = optimize.SimplexCalculate();

            Assert.AreEqual(34.353, Math.Round(result,3));
        }

        [TestMethod]
        public void ExpMinResultTest()
        {

            try
            {
                var functionConstraint = new[] { "4x1 + 9x2", "11x1 + 3x2 >= 45", "4x1 + 3x2 <= 24", "4x1 + 15x2 <= 48" };
                SimplexMethod optimize = new SimplexMethod(functionConstraint);
                var result = optimize.SimplexCalculate();
            }
            catch (Exception ex)

            {
                Assert.AreEqual("Неправильное выражение для нахождения максимума (11x1 + 3x2 >= 45)", ex.Message);
            }
        }
        
    }
}
