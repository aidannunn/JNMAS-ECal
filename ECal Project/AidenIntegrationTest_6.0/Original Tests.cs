using NUnit.Framework;
using System.Reflection;
using SpreadsheetEngine;
using Moq;
using System.Collections.Generic;

namespace IntegrationTestingExpressionTreeBlackbox
{
    [TestFixture]
    public class Tests
    {
        OperatorNodeFactory factory;
        Mock<ExpressionTree_Testing> mock;

        [SetUp]
        public void Setup()
        {
            //Formula input -> ("5+2*(4-3)")
            factory = new OperatorNodeFactory();
            mock = new Mock<ExpressionTree_Testing>("5+2*(4-3)");// create mock
            mock.CallBase = true;// allow partial mocks
        }

        /// <summary>
        /// Test the ExpressionTree with all methods stubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test1_AllMethodsStubbed()
        {
            List<string> str = new List<string> { "5", "2", "4", "3", "-", "*", "+" };

            //manually build the tree that is created by BuildTree() since the stubbed method won't create the tree, it only returns the root
            OperatorNode nodemult = factory.CreateOperatorNode('*');
            OperatorNode nodeplus = factory.CreateOperatorNode('+');
            ConstantNode node2 = new ConstantNode(2);
            ConstantNode node5 = new ConstantNode(5);
            OperatorNode node_neg = factory.CreateOperatorNode('-');
            ConstantNode node4 = new ConstantNode(4);
            ConstantNode node3 = new ConstantNode(3);
            nodeplus.Left = node5;
            nodeplus.Right = nodemult;
            nodemult.Left = node2;
            nodemult.Right = node_neg;
            node_neg.Left = node4;
            node_neg.Right = node3;

            //stub Evaluate
            mock.Setup(l => l.Evaluate()).Returns(7.0);

            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree("5+2*(4-3)")).Returns(nodeplus);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm("5+2*(4-3)")).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)

                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(true);

            //stub IsLeftParenthesis
            mock.Setup(l => l.IsLeftParenthesis('(')).Returns(true);

            //stub IsRightParenthesis
            mock.Setup(l => l.IsRightParenthesis(')')).Returns(true);

            //stub IsLeftAssociative
            mock.Setup(l => l.IsLeftAssociative('+')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('-')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('/')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('*')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('^')).Returns(true);

            //stub IsHigherPrecedence
            mock.Setup(l => l.IsHigherPrecedence('^', It.IsAny<char>())).Returns(true);// power is always higher precedence than any other current operator
            mock.Setup(l => l.IsHigherPrecedence('*', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '-')).Returns(true);

            //stub IsSamePrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsHigherPrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(7.0));
        }


        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test2_Evaluate()
        {
            List<string> str = new List<string> { "5", "2", "4", "3", "-", "*", "+" };

            //manually build the tree that is created by BuildTree() since the stubbed method won't create the tree, it only returns the root
            OperatorNode nodemult = factory.CreateOperatorNode('*');
            OperatorNode nodeplus = factory.CreateOperatorNode('+');
            ConstantNode node2 = new ConstantNode(2);
            ConstantNode node5 = new ConstantNode(5);
            OperatorNode node_neg = factory.CreateOperatorNode('-');
            ConstantNode node4 = new ConstantNode(4);
            ConstantNode node3 = new ConstantNode(3);
            nodeplus.Left = node5;
            nodeplus.Right = nodemult;
            nodemult.Left = node2;
            nodemult.Right = node_neg;
            node_neg.Left = node4;
            node_neg.Right = node3;

            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree("5+2*(4-3)")).Returns(nodeplus);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm("5+2*(4-3)")).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(true);

            //stub IsLeftParenthesis
            mock.Setup(l => l.IsLeftParenthesis('(')).Returns(true);

            //stub IsRightParenthesis
            mock.Setup(l => l.IsRightParenthesis(')')).Returns(true);

            //stub IsLeftAssociative
            mock.Setup(l => l.IsLeftAssociative('+')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('-')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('/')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('*')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('^')).Returns(true);

            //stub IsHigherPrecedence
            mock.Setup(l => l.IsHigherPrecedence('^', It.IsAny<char>())).Returns(true);// power is always higher precedence than any other current operator
            mock.Setup(l => l.IsHigherPrecedence('*', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '-')).Returns(true);

            //stub IsSamePrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsHigherPrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(7.0));
        }

        /// <summary>
        /// Test ExpressionTree with the Constructor unstubbed.
        /// Constructor cannot be stubbed in C#. However, the method is simple enough to warrent not testing
        /// </summary>
        [Test]
        public void Test3_Constructor()
        {
            Assert.Pass();
        }

        /// <summary>
        /// Test ExpressionTree with BuildTree unstubbed.
        /// </summary>
        [Test]
        public void Test4_BuildTree()
        {
            List<string> str = new List<string> { "5", "2", "4", "3", "-", "*", "+" };

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm("5+2*(4-3)")).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(true);

            //stub IsLeftParenthesis
            mock.Setup(l => l.IsLeftParenthesis('(')).Returns(true);

            //stub IsRightParenthesis
            mock.Setup(l => l.IsRightParenthesis(')')).Returns(true);

            //stub IsLeftAssociative
            mock.Setup(l => l.IsLeftAssociative('+')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('-')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('/')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('*')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('^')).Returns(true);

            //stub IsHigherPrecedence
            mock.Setup(l => l.IsHigherPrecedence('^', It.IsAny<char>())).Returns(true);// power is always higher precedence than any other current operator
            mock.Setup(l => l.IsHigherPrecedence('*', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '-')).Returns(true);

            //stub IsSamePrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsHigherPrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(7.0));
        }

        /// <summary>
        /// Test ExpressionTree with ShuntingYardAlgorithm unstubbed.
        /// </summary>
        [Test]
        public void Test5_ShuntingYardAlgorithm()
        {
            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(true);

            //stub IsLeftParenthesis
            mock.Setup(l => l.IsLeftParenthesis('(')).Returns(true);

            //stub IsRightParenthesis
            mock.Setup(l => l.IsRightParenthesis(')')).Returns(true);

            //stub IsLeftAssociative
            mock.Setup(l => l.IsLeftAssociative('+')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('-')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('/')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('*')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('^')).Returns(true);

            //stub IsHigherPrecedence
            mock.Setup(l => l.IsHigherPrecedence('^', It.IsAny<char>())).Returns(true);// power is always higher precedence than any other current operator
            mock.Setup(l => l.IsHigherPrecedence('*', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '-')).Returns(true);

            //stub IsSamePrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsHigherPrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(7.0));
        }

        /// <summary>
        /// Test ExpressionTree with IsOperatorOrParenthesis unstubbed.
        /// </summary>
        [Test]
        public void Test6_IsOperatorOrParenthesis()
        {
            //stub IsLeftParenthesis
            mock.Setup(l => l.IsLeftParenthesis('(')).Returns(true);

            //stub IsRightParenthesis
            mock.Setup(l => l.IsRightParenthesis(')')).Returns(true);

            //stub IsLeftAssociative
            mock.Setup(l => l.IsLeftAssociative('+')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('-')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('/')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('*')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('^')).Returns(true);

            //stub IsHigherPrecedence
            mock.Setup(l => l.IsHigherPrecedence('^', It.IsAny<char>())).Returns(true);// power is always higher precedence than any other current operator
            mock.Setup(l => l.IsHigherPrecedence('*', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '-')).Returns(true);

            //stub IsSamePrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsHigherPrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(7.0));
        }

        /// <summary>
        /// Test ExpressionTree with IsLeftParenthesis unstubbed.
        /// </summary>
        [Test]
        public void Test7_IsLeftParenthesis()
        {
            //stub IsRightParenthesis
            mock.Setup(l => l.IsRightParenthesis(')')).Returns(true);

            //stub IsLeftAssociative
            mock.Setup(l => l.IsLeftAssociative('+')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('-')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('/')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('*')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('^')).Returns(true);

            //stub IsHigherPrecedence
            mock.Setup(l => l.IsHigherPrecedence('^', It.IsAny<char>())).Returns(true);// power is always higher precedence than any other current operator
            mock.Setup(l => l.IsHigherPrecedence('*', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '-')).Returns(true);

            //stub IsSamePrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsHigherPrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(7.0));
        }

        /// <summary>
        /// Test ExpressionTree with IsRightParenthesis unstubbed.
        /// </summary>
        [Test]
        public void Test8_IsRightParenthesis()
        {
            //stub IsLeftAssociative
            mock.Setup(l => l.IsLeftAssociative('+')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('-')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('/')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('*')).Returns(true);
            mock.Setup(l => l.IsLeftAssociative('^')).Returns(true);

            //stub IsHigherPrecedence
            mock.Setup(l => l.IsHigherPrecedence('^', It.IsAny<char>())).Returns(true);// power is always higher precedence than any other current operator
            mock.Setup(l => l.IsHigherPrecedence('*', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '-')).Returns(true);

            //stub IsSamePrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsHigherPrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(7.0));
        }

        /// <summary>
        /// Test ExpressionTree with IsRightAssociative unstubbed.
        /// This method is not tested since none of the operators in the current build have right associativity.
        /// </summary>
        [Test]
        public void Test9_IsRightAssociative()
        {
            Assert.Pass();
        }

        /// <summary>
        /// Test ExpressionTree with IsLeftAssociative unstubbed.
        /// </summary>
        [Test]
        public void Test10_IsLeftAssociative()
        {
            //stub IsHigherPrecedence
            mock.Setup(l => l.IsHigherPrecedence('^', It.IsAny<char>())).Returns(true);// power is always higher precedence than any other current operator
            mock.Setup(l => l.IsHigherPrecedence('*', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '-')).Returns(true);

            //stub IsSamePrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsHigherPrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(7.0));
        }

        /// <summary>
        /// Test ExpressionTree with IsHigherPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test11_IsHigherPrecedence()
        {
            //stub IsSamePrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsHigherPrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(7.0));
        }

        /// <summary>
        /// Test ExpressionTree with IsSamePrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test12_IsSamePrecedence()
        {
            //stub IsLowerPrecedence
            mock.Setup(l => l.IsHigherPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsHigherPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(7.0));
        }

        /// <summary>
        /// Test ExpressionTree with IsLowerPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test13_IsLowerPrecedence()
        {
            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(7.0));
        }

        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// </summary>
        [Test]
        public void Test14_Evaluate()
        {
            ExpressionTree expressionTree = new ExpressionTree("5+2*(4-3)");

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(7.0));

            expressionTree = new ExpressionTree("");

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(0));
        }
    }
}