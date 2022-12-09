using ECalProject;
using System;

namespace MathCalculationTests1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TestRadianMinValue()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.radian(double.MinValue), Is.EqualTo((Math.PI / 180) * double.MinValue));
        }

        [Test]
        public void TestRadianMinValuePlus()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.radian(double.MinValue+1), Is.EqualTo((Math.PI / 180) * (double.MinValue+1)));
        }

        [Test]
        public void TestRadianZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.radian(0), Is.EqualTo(0));
        }

        [Test]
        public void TestRadianMaxValueMinus()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.radian(double.MaxValue - 1), Is.EqualTo((Math.PI / 180) * (double.MaxValue - 1)));
        }

        [Test]
        public void TestRadianMaxValue()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.radian(double.MaxValue), Is.EqualTo((Math.PI / 180) * double.MaxValue));
        }

        [Test]
        public void TestFactorialMin()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.Throws<System.Exception>(
                            delegate { object result = mathCalculation.factorial(double.MinValue); });
        }

        [Test]
        public void TestFactorialMinPlus()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.Throws<System.Exception>(
                            delegate { object result = mathCalculation.factorial(double.MinValue+1); });
        }

        [Test]
        public void TestFactorialZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.factorial(0), Is.EqualTo(1));
        }

        [Test]
        public void TestFactorialNormal()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.factorial(3), Is.EqualTo(6));
        }

        [Test]
        public void TestFactorialMaxMinus() // Using a large integer instead of double.MaxValue as it would be impractical for a computer to calculate the factorial of double.MaxValue.
        {
            double fact = 1;
            for (int i = 1; i <= 9999; i++)
            {
                fact = fact * i;
            }
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.factorial(9999), Is.EqualTo(fact));
        }

        [Test]
        public void TestFactorialMax() // Using a large integer instead of double.MaxValue as it would be impractical for a computer to calculate the factorial of double.MaxValue.
        {
            double fact = 1;
            for (int i = 1; i <= 10000; i++)
            {
                fact = fact * i;
            }
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.factorial(10000), Is.EqualTo(fact));
        }

        [Test]
        public void TestPercentMin()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.percent(double.MinValue), Is.EqualTo(double.MinValue/100));
        }

        [Test]
        public void TestPercentMinPlus()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.percent(double.MinValue+1), Is.EqualTo((double.MinValue + 1) / 100));
        }

        [Test]
        public void TestPercentZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.percent(0.0), Is.EqualTo(0.0));
        }

        [Test]
        public void TestPercentMaxMinus()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.percent(double.MaxValue - 1), Is.EqualTo((double.MaxValue - 1)/100));
        }

        [Test]
        public void TestPercentMax()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.percent(double.MaxValue), Is.EqualTo((double.MaxValue) / 100));
        }

        [Test]
        public void TestEConstantMin()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.eConstant(double.MinValue), Is.EqualTo(Math.Pow(Math.E, double.MinValue)));
        }

        [Test]
        public void TestEConstantMinPlus()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.eConstant(double.MinValue+1), Is.EqualTo(Math.Pow(Math.E, double.MinValue + 1)));
        }

        [Test]
        public void TestEConstantZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.eConstant(0.0), Is.EqualTo(1));
        }

        [Test]
        public void TestEConstantMaxMinus()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.eConstant(double.MaxValue - 1), Is.EqualTo(Math.Pow(Math.E, double.MaxValue - 1)));
        }

        [Test]
        public void TestEConstantMax()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.eConstant(double.MaxValue), Is.EqualTo(Math.Pow(Math.E, double.MaxValue)));
        }

        [Test]
        public void TestInverseFunctionMin()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.inverseFunction(double.MinValue), Is.EqualTo(1 / double.MinValue));
        }

        [Test]
        public void TestInverseFunctionMinPlus()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.inverseFunction(double.MinValue + 1), Is.EqualTo(1 / (double.MinValue + 1)));
        }

        [Test]
        public void TestInverseFunctionZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.inverseFunction(0.0), Is.EqualTo(double.PositiveInfinity));
        }

        [Test]
        public void TestInverseFunctionMaxMinus()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.inverseFunction(double.MaxValue - 1), Is.EqualTo(1 / (double.MaxValue - 1)));
        }

        [Test]
        public void TestInverseFunctionMax()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.inverseFunction(double.MaxValue), Is.EqualTo(1 / (double.MaxValue)));
        }

        [Test]
        public void TestPiFunction()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.piFunction(), Is.EqualTo(Math.PI));
        }
    }
}