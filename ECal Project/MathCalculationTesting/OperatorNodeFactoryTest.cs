using NUnit.Framework;
using SpreadsheetEngine;

namespace UnitTesting
{
    public class OperatorNodeFactoryTest
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test GetOperators (Jared)
        /// Checks to see if operator is contained within the list of returned operators
        /// </summary>
        [Test]
        [TestCase('/')]
        [TestCase('*')]
        [TestCase('-')]
        [TestCase('+')]
        [TestCase('^')]
        public void TestGetOperators(char op)
        {
            OperatorNodeFactory factory = new OperatorNodeFactory();

            List<char> test = factory.GetOperators();

            Assert.IsTrue(test.Contains(op));
        }

        /// <summary>
        /// Test IsOperator (Jared)
        /// Similar tests as above, except it tests for when there is an invalid operator
        /// </summary>
        [Test]
        [TestCase('/', ExpectedResult = true)]
        [TestCase('*', ExpectedResult = true)]
        [TestCase('-', ExpectedResult = true)]
        [TestCase('+', ExpectedResult = true)]
        [TestCase('^', ExpectedResult = true)]
        [TestCase('a', ExpectedResult = false)]
        public bool TestIsOperator(char op)
        {
            OperatorNodeFactory factory = new OperatorNodeFactory();

           return factory.IsOperator(op);
        }

        /// <summary>
        /// Test CreateOperaterNode (Jared)
        /// Checks to make sure Operator Node is being created
        /// </summary>
        [Test]
        [TestCase('/')]
        [TestCase('*')]
        [TestCase('-')]
        [TestCase('+')]
        [TestCase('^')]
        public void TestCreateOperatorNode(char op)
        {
            OperatorNodeFactory factory = new OperatorNodeFactory();

            OperatorNode test = factory.CreateOperatorNode(op);

            Assert.NotNull(test);
        }

        /// <summary>
        /// Test CreateOperatorNode (Jared)
        /// Extension of test above, throws an exception if an invalid operator is present
        /// </summary>
        [Test]
        [TestCase('a')]
        public void TestCreateOperatorNodeInvalid(char op)
        {
            OperatorNodeFactory factory = new OperatorNodeFactory();

            var ex = Assert.Throws<Exception>(()=> factory.CreateOperatorNode(op));

            Assert.IsTrue(ex.Message.Contains("Unhandled operator"));
        }

        /// <summary>
        /// Test GetPrecedence (Jared)
        /// Checks to ensure each operator has the correct precendence level
        /// </summary>
        [Test]
        [TestCase('a', ExpectedResult = (ushort)0)]
        [TestCase('/', ExpectedResult = (ushort)2)]
        [TestCase('*', ExpectedResult = (ushort)2)]
        [TestCase('-', ExpectedResult = (ushort)1)]
        [TestCase('+', ExpectedResult = (ushort)1)]
        [TestCase('^', ExpectedResult = (ushort)3)]
        public ushort TestGetPrecedence(char op)
        {
            OperatorNodeFactory factory = new OperatorNodeFactory();

            return factory.GetPrecedence(op);
        }

        /// <summary>
        /// Test GetAssociativity (Jared)
        /// Ensures the function returns left
        /// </summary>
        [Test]
        [TestCase('/', ExpectedResult = OperatorNode.Associative.Left)]
        [TestCase('*', ExpectedResult = OperatorNode.Associative.Left)]
        [TestCase('-', ExpectedResult = OperatorNode.Associative.Left)]
        [TestCase('+', ExpectedResult = OperatorNode.Associative.Left)]
        [TestCase('^', ExpectedResult = OperatorNode.Associative.Left)]
        public OperatorNode.Associative TestGetAssociativity(char op)
        {
            OperatorNodeFactory factory = new OperatorNodeFactory();

            return factory.GetAssociativity(op);
        }
    }
}
