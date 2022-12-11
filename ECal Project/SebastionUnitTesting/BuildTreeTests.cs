using Moq;
using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using SpreadsheetEngine;
using System.Linq.Expressions;
using System.Reflection;

// NOTE: The BuildTree function only works with valid equations as input. In the app, it can only be called with valid inputs. Otherwise, it throws an error.

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
            list.Add("");
            list.Add("/");
            list.Add("+");
            
            mock.Setup(l => l.ShuntingYardAlgorithm("1+2/")).Returns(list);
            mock.CallBase = true;
            ExpressionTree mockTree = mock.Object;

            Exception ex = Assert.Throws<System.Exception>(
                            delegate { object result = mockTree.BuildTree("1+2/"); });
        }

        [Test]
        public void TestBuildTreeNonNumericValue()
        {
            var mock = new Mock<ExpressionTree>("");
            List<string> list = new List<string>();
            list.Add("a");

            mock.Setup(l => l.ShuntingYardAlgorithm("a")).Returns(list);
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

            var mock = new Mock<ExpressionTree>("1/2/4");
            List<string> list = new List<string>();
            list.Add("1");
            list.Add("2");
            list.Add("+");
            list.Add("4");
            list.Add("/");

            list = new ExpressionTree("").ShuntingYardAlgorithm("(1+2)/4");

            mock.Setup(l => l.ShuntingYardAlgorithm("(1+2)/4")).Returns(list);
            mock.Setup(l => l.IsOperatorOrParenthesis('1')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('2')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('4')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('(')).Returns(true);
            mock.Setup(l => l.IsOperatorOrParenthesis(')')).Returns(true);
            mock.Setup(l => l.IsOperatorOrParenthesis('+')).Returns(true);
            mock.Setup(l => l.IsOperatorOrParenthesis('/')).Returns(true);
            mock.Setup(l => l.CreateOperatorNode('/')).Returns(new DivideOperatorNode());
            mock.CallBase = true;
            ExpressionTree mockTree = mock.Object;

            Assert.That(JsonConvert.SerializeObject(mockTree.BuildTree("(1+2)/4")), Is.EqualTo(JsonConvert.SerializeObject(expectedRoot)));
            Assert.That(mockTree.BuildTree("(1+2)/4").Evaluate(), Is.EqualTo(0.75));
        }

        [Test]
        public void TestBuildTreeShuntingYardConsecutiveOperators()
        {
            OperatorNode expectedRoot = new DivideOperatorNode();
            OperatorNode expected = new DivideOperatorNode();
            expected.Left = new ConstantNode(4);
            expected.Right = new ConstantNode(7);
            expectedRoot.Left = new ConstantNode(2);
            expectedRoot.Right = expected;

            var mock = new Mock<ExpressionTree>("1/2/4");
            List<string> list = new List<string>();
            list.Add("2");
            list.Add("4");
            list.Add("7");
            list.Add("/");
            list.Add("/");

            list = new ExpressionTree("").ShuntingYardAlgorithm("2/(4/7)");

            mock.Setup(l => l.ShuntingYardAlgorithm("2/(4/7)")).Returns(list);
            mock.Setup(l => l.IsOperatorOrParenthesis('2')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('4')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('7')).Returns(false);
            mock.Setup(l => l.IsOperatorOrParenthesis('/')).Returns(true);
            mock.CallBase = true;
            ExpressionTree mockTree = mock.Object;

            Assert.That(JsonConvert.SerializeObject(mockTree.BuildTree("2/(4/7)")), Is.EqualTo(JsonConvert.SerializeObject(expectedRoot)));
        }
    }
}
