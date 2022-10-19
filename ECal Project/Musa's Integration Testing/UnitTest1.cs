namespace Musa_s_Integration_Testing
{
    using NUnit.Framework;
    using SpreadsheetEngine;

     /*
     * Top-Down Integration Testing of ExpressionTree
     * Tests are organized in order of un-stubbing. The name of a test states which method is unstubbed.
     */

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
    }
}