using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using SpreadsheetEngine;

namespace UnitTesting
{
    public class SpreadSheetTest
    {
        private List<string> validStrings = new List<string>() { "1+1", "1*1", "2^2", "1/1", "1-1", "(1+1)+1" };

        [SetUp]
        public void Setup()
        {

        }

        /// <summary>
        /// Test Constructor (Jared)
        /// weather constructor -> constructs
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            ExpressionTree expTree = new ExpressionTree(string.Empty);
            Assert.IsNotNull(expTree);
        }

        /// <summary>
        /// Test Evaluation (Jared)
        /// Evaluate cannot currently handle negative values
        /// </summary>
        [Test]
        [TestCase("1+1", ExpectedResult = 2.0)]
        [TestCase("1*1", ExpectedResult = 1.0)]
        [TestCase("1/1", ExpectedResult = 1.0)]
        [TestCase("1-1", ExpectedResult = 0.0)]
        // [TestCase("-1+1", ExpectedResult = 0.0)]
        [TestCase("(1+1)-1", ExpectedResult = 1.0)]
        [TestCase("5*4-2", ExpectedResult = 18.0)]
        public double TestEvaluate(string expression)
        {
            ExpressionTree expTree = new ExpressionTree(expression);
            
            return expTree.Evaluate();
        }

        /// <summary>
        /// Test Shunting Yard Algorithm (Jared)
        /// </summary>
        [Test]
        [TestCase("1+1", new string[] { "1", "1", "+" })]
        [TestCase("1*1", new string[] { "1", "1", "*" })]
        [TestCase("1-1", new string[] { "1", "1", "-" })]
        [TestCase("1/1", new string[] { "1", "1", "/" })]
        [TestCase("2^2", new string[] { "2", "2", "^" })]
        [TestCase("(1+1)-1", new string[] { "1", "1", "+" , "-", "1"})]
        [TestCase("5*4-2", new string[] { "5", "4", "2", "*", "-" })]
        public void TestShuntinYardAlgorithm(string testExpression, string[] expectedResults)
        {
            ExpressionTree expTree = new ExpressionTree(string.Empty);

            List<string> output = expTree.ShuntingYardAlgorithm(testExpression);

            Assert.AreEqual(output.Count, expectedResults.Length);
            for(int i = 0; i < expectedResults.Length; i++)
            {
                Assert.That(output.Contains(expectedResults[i]));
            }
        }

        /// <summary>
        /// Test Build Tree (Sebastian)
        /// </summary>

    }
}
