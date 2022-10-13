namespace Aidan_s_Integration_Testing
{
    using NUnit.Framework;
    using SpreadsheetEngine;
    /*
     * Top-Down Integration Testing of ExpressionTree
     * Tests are organized in order of un-stubbing. The name of a test states which method is unstubbed.
     */
    public class IntegrationTestExpressionTree
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test the ExpressionTree with all methods stubbed.
        /// </summary>
        [Test]
        public void Test1_AllStubs()
        {
            Assert.Pass();
        }

        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// </summary>
        [Test]
        public void Test2_Evaluate()
        {

        }

        /// <summary>
        /// Test ExpressionTree with the Constructor unstubbed.
        /// </summary>
        [Test]
        public void Test3_Constructor()
        {

        }

        /// <summary>
        /// Test ExpressionTree with BuildTree unstubbed.
        /// </summary>
        [Test]
        public void Test4_BuildTree()
        {

        }

        /// <summary>
        /// Test ExpressionTree with ShuntingYardAlgorithm unstubbed.
        /// </summary>
        [Test]
        public void Test5_ShuntingYardAlgorithm()
        {

        }

        /// <summary>
        /// Test ExpressionTree with IsOperatorOrParenthesis unstubbed.
        /// </summary>
        [Test]
        public void Test6_IsOperatorOrParenthesis()
        {

        }

        /// <summary>
        /// Test ExpressionTree with IsLeftParenthesis unstubbed.
        /// </summary>
        [Test]
        public void Test7_IsLeftParenthesis()
        {

        }

        /// <summary>
        /// Test ExpressionTree with IsRightParenthesis unstubbed.
        /// </summary>
        [Test]
        public void Test8_IsRightParenthesis()
        {

        }

        /// <summary>
        /// Test ExpressionTree with IsRightAssociative unstubbed.
        /// </summary>
        [Test]
        public void Test9_IsRightAssociative()
        {

        }

        /// <summary>
        /// Test ExpressionTree with IsLeftAssociative unstubbed.
        /// </summary>
        [Test]
        public void Test10_IsLeftAssociative()
        {

        }

        /// <summary>
        /// Test ExpressionTree with IsHigherPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test11_IsHigherPrecedence()
        {

        }

        /// <summary>
        /// Test ExpressionTree with IsSamePrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test12_IsSamePrecedence()
        {

        }

        /// <summary>
        /// Test ExpressionTree with IsLowerPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test13_IsLowerPrecedence()
        {

        }
    }
}