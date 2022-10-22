using ECalProject;
using System;

namespace MathCalculationTests
{
    [TestClass]
    public class MathCalculationTests
    {
        [TestMethod]
        public void TestRadianPositive()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(Math.PI, mathCalculation.radian(180));
        }

        [TestMethod]
        public void TestRadianNegative()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(-Math.PI, mathCalculation.radian(-180));
        }

        [TestMethod]
        public void TestRadianZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(0, mathCalculation.radian(0));
        }

        [TestMethod]
        public void TestFactorialNormal()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(6, mathCalculation.factorial(3));
        }

        [TestMethod]
        public void TestFactorialOne()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(1, mathCalculation.factorial(1));
        }

        [TestMethod]
        public void TestFactorialZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(0, mathCalculation.factorial(0));
        }

        [TestMethod]
        public void TestPercentPositive()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(110.01, mathCalculation.percent(11001));
        }

        [TestMethod]
        public void TestPercentNegative()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(-110.01, mathCalculation.percent(-11001));
        }

        [TestMethod]
        public void TestPercentZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(0, mathCalculation.percent(0));
        }

        [TestMethod]
        public void TestEConstantOne()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(Math.E, mathCalculation.eConstant(1));
        }

        [TestMethod]
        public void TestEConstantZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(1, mathCalculation.eConstant(0));
        }

        [TestMethod]
        public void TestEConstantNegative()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(Math.Pow(Math.E, -1), mathCalculation.eConstant(-1));
        }

        [TestMethod]
        public void TestInverseFunctionPositive()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(1/2.5, mathCalculation.inverseFunction(2.5));
        }

        [TestMethod]
        public void TestInverseFunctionNegative()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(-1/2.0, mathCalculation.inverseFunction(-2.0));
        }

        [TestMethod]
        public void TestInverseFunctionZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(0, mathCalculation.inverseFunction(0));
        }

        [TestMethod]
        public void TestPiFunction()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.AreEqual(Math.PI, mathCalculation.piFunction());
        }
    }
}