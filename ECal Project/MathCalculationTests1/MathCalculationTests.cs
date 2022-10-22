using ECalProject;

namespace MathCalculationTests1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestRadianPositive()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.radian(180), Is.EqualTo(Math.PI));
        }

        [Test]
        public void TestRadianNegative()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.radian(-180), Is.EqualTo(-Math.PI));
        }

        [Test]
        public void TestRadianZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.radian(0), Is.EqualTo(0));
        }

        [Test]
        public void TestFactorialNormal()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.factorial(3), Is.EqualTo(6));
        }

        [Test]
        public void TestFactorialOne()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.factorial(1), Is.EqualTo(1));
        }

        [Test]
        public void TestFactorialZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.factorial(0), Is.EqualTo(0));
        }

        [Test]
        public void TestPercentPositive()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.percent(11001), Is.EqualTo(110.01));
        }

        [Test]
        public void TestPercentNegative()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.percent(-11001), Is.EqualTo(-110.01));
        }

        [Test]
        public void TestPercentZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.percent(0), Is.EqualTo(0));
        }

        [Test]
        public void TestEConstantOne()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.eConstant(1), Is.EqualTo(Math.E));
        }

        [Test]
        public void TestEConstantZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.eConstant(0), Is.EqualTo(1));
        }

        [Test]
        public void TestEConstantNegative()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.eConstant(-1), Is.EqualTo(Math.Pow(Math.E, -1)));
        }

        [Test]
        public void TestInverseFunctionPositive()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.inverseFunction(2.5), Is.EqualTo(1 / 2.5));
        }

        [Test]
        public void TestInverseFunctionNegative()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.inverseFunction(-2.0), Is.EqualTo(-1 / 2.0));
        }

        [Test]
        public void TestInverseFunctionZero()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.inverseFunction(0), Is.EqualTo(0));
        }

        [Test]
        public void TestPiFunction()
        {
            MathCalculation mathCalculation = new MathCalculation();
            Assert.That(mathCalculation.piFunction(), Is.EqualTo(Math.PI));
        }
    }
}