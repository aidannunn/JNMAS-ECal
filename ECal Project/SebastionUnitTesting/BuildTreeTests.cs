using Moq;
using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using SpreadsheetEngine;
using System.Linq.Expressions;
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
            var mock = new Mock<ExpressionTree>("");
            mock.CallBase = true;
            ExpressionTree mockTree = mock.Object;
            Assert.That(mockTree.BuildTree(""), Is.EqualTo(null));
        }

        [Test]
        public void TestBuildTreeInvalidFormula()
        {
            var mock = new Mock<ExpressionTree>("");
            List<string> list = new List<string>();
            list.Add("1");
            list.Add("2");
            list.Add("/");
            list.Add("+");

            mock.Setup(l => l.ShuntingYardAlgorithm("1+2/")).Returns(list);
            mock.Setup(l => l.IsOperatorOrParenthesis('1')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('2')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('/')).Returns(true);
            mock.Setup(l => l.IsOperatorOrParenthesis('+')).Returns(true);
            mock.Setup(l => l.CreateOperatorNode('/')).Returns(new DivideOperatorNode());
            mock.Setup(l => l.CreateOperatorNode('+')).Returns(new PlusOperatorNode());
            mock.CallBase = true;
            ExpressionTree mockTree = mock.Object;

            Exception ex = Assert.Throws<System.InvalidOperationException>(
                            delegate { object result = mockTree.BuildTree("1+2/"); });
        }

        [Test]
        public void TestBuildTreeNonNumericValue()
        {
            var mock = new Mock<ExpressionTree>("");
            List<string> list = new List<string>();
            list.Add("a");

            mock.Setup(l => l.ShuntingYardAlgorithm("a")).Returns(list);
            mock.Setup(l => l.IsOperatorOrParenthesis('a')).Returns(false);
            mock.CallBase = true;
            ExpressionTree mockTree = mock.Object;

            Exception ex = Assert.Throws<System.Exception>(
                            delegate { object result = mockTree.BuildTree("a"); });
        }

        [Test]
        public void TestBuildTreeOneNumberFormula()
        {
            var mock = new Mock<ExpressionTree>("");
            List<string> list = new List<string>();
            list.Add("1.2");

            mock.Setup(l => l.ShuntingYardAlgorithm("1.2")).Returns(list);
            mock.CallBase = true;
            ExpressionTree mockTree = mock.Object;

            Assert.That(JsonConvert.SerializeObject(mockTree.BuildTree("1.2")), Is.EqualTo(JsonConvert.SerializeObject(new ConstantNode(1.2))));
        }

        [Test]
        public void TestBuildTreeShuntingYardAlternatingNumbersAndOperators()
        {
            OperatorNode expectedRoot = new DivideOperatorNode();
            OperatorNode expected = new PlusOperatorNode();
            expected.Left = new ConstantNode(1);
            expected.Right = new ConstantNode(2);
            expectedRoot.Right = new ConstantNode(4);
            expectedRoot.Left = expected;

            var mock = new Mock<ExpressionTree>("");
            List<string> list = new List<string>();
            list.Add("1");
            list.Add("2");
            list.Add("+");
            list.Add("4");
            list.Add("/");

            mock.Setup(l => l.ShuntingYardAlgorithm("(1+2)/4")).Returns(list);
            mock.Setup(l => l.IsOperatorOrParenthesis('1')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('2')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('4')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('+')).Returns(true);
            mock.Setup(l => l.IsOperatorOrParenthesis('/')).Returns(true);
            mock.Setup(l => l.CreateOperatorNode('/')).Returns(new DivideOperatorNode());
            mock.Setup(l => l.CreateOperatorNode('+')).Returns(new PlusOperatorNode());
            mock.CallBase = true;
            ExpressionTree mockTree = mock.Object;

            Assert.That(JsonConvert.SerializeObject(mockTree.BuildTree("(1+2)/4")), Is.EqualTo(JsonConvert.SerializeObject(expectedRoot)));
            Assert.IsTrue(mockTree.BuildTree("(1+2)/4").GetType() == expectedRoot.GetType());
            Assert.IsTrue(((OperatorNode)mockTree.BuildTree("(1+2)/4")).Right.GetType() == expectedRoot.Right.GetType());
            Assert.IsTrue(((OperatorNode)mockTree.BuildTree("(1+2)/4")).Left.GetType() == expected.GetType());
        }

        [Test]
        public void TestBuildTreeShuntingYardConsecutiveOperators()
        {
            OperatorNode expectedRoot = new DivideOperatorNode();
            OperatorNode expected = new PlusOperatorNode();
            expected.Left = new ConstantNode(4);
            expected.Right = new ConstantNode(1);
            expectedRoot.Left = new ConstantNode(2);
            expectedRoot.Right = expected;

            var mock = new Mock<ExpressionTree>("");
            List<string> list = new List<string>();
            list.Add("2");
            list.Add("4");
            list.Add("1");
            list.Add("+");
            list.Add("/");

            mock.Setup(l => l.ShuntingYardAlgorithm("2/(4+1)")).Returns(list);
            mock.Setup(l => l.IsOperatorOrParenthesis('2')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('4')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('1')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('/')).Returns(true);
            mock.Setup(l => l.IsOperatorOrParenthesis('+')).Returns(true);
            mock.Setup(l => l.CreateOperatorNode('/')).Returns(new DivideOperatorNode());
            mock.Setup(l => l.CreateOperatorNode('+')).Returns(new PlusOperatorNode());
            mock.CallBase = true;
            ExpressionTree mockTree = mock.Object;

            Assert.That(JsonConvert.SerializeObject(mockTree.BuildTree("2/(4+1)")), Is.EqualTo(JsonConvert.SerializeObject(expectedRoot)));
            Assert.IsTrue(mockTree.BuildTree("2/(4+1)").GetType() == expectedRoot.GetType());
            Assert.IsTrue(((OperatorNode)mockTree.BuildTree("2/(4+1)")).Right.GetType() == expected.GetType());
        }
    }
}
