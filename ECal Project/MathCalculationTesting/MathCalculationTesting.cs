// This class will be testing MathCalculations within ECALProject

using NUnit.Framework;
using SpreadsheetEngine;

namespace MathCalculationTesting
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(4, ExpectedResult = 2)]
        [TestCase(16, ExpectedResult = 4)]
        [TestCase(64, ExpectedResult = 8)]
        [TestCase(256, ExpectedResult = 16)]

        public double TestSquareRoot(double num)
        {
            MathCalculation mathCalc = new MathCalculation();

            return mathCalc.squareRoot(num);
        }

        [Test]
        [TestCase(0,0, ExpectedResult = 1)]
        [TestCase(0,1, ExpectedResult = 0)]
        [TestCase(1,0, ExpectedResult = 1)]
        [TestCase(2,0, ExpectedResult = 1)]
        [TestCase(2,2, ExpectedResult = 4)]
        [TestCase(2, 3, ExpectedResult = 8)]

        public double TestPower(double num, double power)
        {
            MathCalculation mathCalc = new MathCalculation();

            return mathCalc.power(num, power);
        }

        [Test]
        [TestCase(1, ExpectedResult = 0)]
        [TestCase(16, ExpectedResult = 1.2041199826559248)]

        public double TestLnLog(double num)
        {
            MathCalculation mathCalc = new MathCalculation();

            return mathCalc.log(num);
        }

        [Test]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(16, ExpectedResult = 4)]
        public double TestLog(double num)
        {
            MathCalculation mathCalc = new MathCalculation();

            return mathCalc.squareRoot(num);
        }

        [Test]
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(Math.PI/2, ExpectedResult = 1)]
        [TestCase(Math.PI, ExpectedResult = 1.2246467991473532E-16)]
        [TestCase(2*Math.PI, ExpectedResult = -2.4492935982947064E-16)]

        public double TestSine(double num)
        {
            MathCalculation mathCalc = new MathCalculation();

            return mathCalc.sine(num);
        }

        [Test]
        [TestCase(0, ExpectedResult = 1)]
        [TestCase(Math.PI / 2, ExpectedResult = 6.123233995736766E-17)]
        [TestCase(Math.PI, ExpectedResult = -1.0)]
        [TestCase(2 * Math.PI, ExpectedResult = 1)]

        public double TestCosine(double num)
        {
            MathCalculation mathCalc = new MathCalculation();

            return mathCalc.cosine(num);
        }

        [Test]
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(Math.PI, ExpectedResult = -1.2246467991473532E-16)]
        [TestCase(2 * Math.PI, ExpectedResult = -2.4492935982947064E-16)]

        public double TestTangent(double num)
        {
            MathCalculation mathCalc = new MathCalculation();

            return mathCalc.tangent(num);
        }
    }
}