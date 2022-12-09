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
            list.Add("");
            list.Add("/");
            list.Add("+");
            
            mock.Setup(l => l.ShuntingYardAlgorithm("1+2/")).Returns(list);
            mock.Setup(l => l.IsOperatorOrParenthesis('1')).Returns(true);
//            mock.Setup(l => l.factory.CreateOperatorNode('/')).Returns(new DivideOperatorNode());
//            mock.Setup(l => l.factory.CreateOperatorNode('+')).Returns(new PlusOperatorNode());
            mock.CallBase = true;
            ExpressionTree mockTree = mock.Object;

            List<string> list1 = mockTree.ShuntingYardAlgorithm("1+2/");
            Assert.That(mockTree.ShuntingYardAlgorithm("1+2/"), Is.EqualTo(list));

            Exception ex = Assert.Throws<System.Exception>(
                            delegate { object result = mockTree.BuildTree("1+2/"); });

            Assert.That(ex.Message, Is.EqualTo("Input contained non-numerical values"));
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
            Assert.That(mockTree.BuildTree("1.2").Evaluate(), Is.EqualTo(1.2));
        }

        [Test]
        public void TestBuildTreeNoSpaces()
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
            mock.CallBase = true;
            ExpressionTree mockTree = mock.Object;

            Assert.That(JsonConvert.SerializeObject(mockTree.BuildTree("(1+2)/4")), Is.EqualTo(JsonConvert.SerializeObject(expectedRoot)));
            Assert.That(mockTree.BuildTree("(1+2)/4").Evaluate(), Is.EqualTo(0.75));
        }
    }
}
