using Newtonsoft.Json;
using SpreadsheetEngine;
using System.Reflection;

namespace BuildTreeNunit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestBuildTreeEmptyFormula()
        {
            ExpressionTree expressionTree = new ExpressionTree("");
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("BuildTree", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { "" };
            object result = methodInfo.Invoke(expressionTree, parameters);
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void TestBuildTreeInvalidFormula()
        {
            ExpressionTree expressionTree = new ExpressionTree("");
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("BuildTree", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { "1 +  2/ " };
            Exception ex = Assert.Throws<System.Reflection.TargetInvocationException>(
                delegate { object result = methodInfo.Invoke(expressionTree, parameters); });
        }

        [Test]
        public void TestBuildTreeOneNumberFormula()
        {
            ExpressionTree expressionTree = new ExpressionTree("");
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("BuildTree", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { "1.2" };
            object result = methodInfo.Invoke(expressionTree, parameters);
            Assert.That(JsonConvert.SerializeObject(result), Is.EqualTo(JsonConvert.SerializeObject(new ConstantNode(1.2))));
        }

        [Test]
        public void TestBuildTreeNoSpaces()
        {
            OperatorNode expectedRoot = new PlusOperatorNode();
            expectedRoot.Left = new ConstantNode(1);
            OperatorNode expected = new DivideOperatorNode();
            expected.Left = new ConstantNode(2);
            expected.Right = new ConstantNode(4);
            expectedRoot.Right = expected;

            ExpressionTree expressionTree = new ExpressionTree("");
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("BuildTree", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { "1+2/4" };
            object result = methodInfo.Invoke(expressionTree, parameters);
            Assert.That(JsonConvert.SerializeObject(result), Is.EqualTo(JsonConvert.SerializeObject(expectedRoot)));
        }

        [Test]
        public void TestBuildTreeSpaces()
        {
            OperatorNode expectedRoot = new PlusOperatorNode();
            expectedRoot.Left = new ConstantNode(1);
            OperatorNode expected = new DivideOperatorNode();
            expected.Left = new ConstantNode(2);
            expected.Right = new ConstantNode(4);
            expectedRoot.Right = expected;

            ExpressionTree expressionTree = new ExpressionTree("");
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("BuildTree", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { "  1 +  2/ 4 " };
            object result = methodInfo.Invoke(expressionTree, parameters);
            Assert.That(JsonConvert.SerializeObject(result), Is.EqualTo(JsonConvert.SerializeObject(expectedRoot)));
        }
    }
}
