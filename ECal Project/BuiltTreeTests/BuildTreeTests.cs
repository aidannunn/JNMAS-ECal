using SpreadsheetEngine;

namespace BuiltTreeTests
{
    [TestClass]
    public class BuildTreeTests
    {

        [TestMethod]
        public void TestBuildTreeEmptyFormula()
        {
            ExpressionTree expressionTree = new ExpressionTree("");
            Assert.AreEqual(0, expressionTree.Evaluate());
        }

        [TestMethod]
        public void TestBuildTreeOneNumberFormula()
        {
            ExpressionTree expressionTree = new ExpressionTree("1.2");
            Assert.AreEqual(1.2, expressionTree.Evaluate());
        }

        [TestMethod]
        public void TestBuildTreeNoSpaces()
        {
            ExpressionTree expressionTree = new ExpressionTree("1+2*3");
            Assert.AreEqual(7, expressionTree.Evaluate());
        }

        [TestMethod]
        public void TestBuildTreeSpaces()
        {
            ExpressionTree expressionTree = new ExpressionTree("1 +2* 3");
            Assert.AreEqual(7, expressionTree.Evaluate());
        }
    }
}