using NUnit.Framework;
using System.Reflection;
using SpreadsheetEngine;
using Moq;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace IntegrationTestingExpressionTreeBlackbox
{
    //Equivalence classses and corresponding formula inputs:
    //  - Addition ->                                                                                   1+5 = 6
    //  - Subtraction ->                                                                                7-4 = 3
    //  - Multiplication ->                                                                             4*3 = 12
    //  - Division ->                                                                                   6/3 = 2
    //  - Power ->                                                                                      3^2 = 9
    //  - Addition or subtraction next to multiplication or division without parenthesis right side ->  4/2+2 = 4
    //  - Addition or subtraction next to multiplication or division without parenthesis left side ->   2+4/2 = 4
    //  - Addition or subtraction next to multiplication or division with parenthesis right side ->     (8/2)-3 = 1
    //  - Addition or subtraction next to multiplication or division with parenthesis left side ->      3-(6/3) = 1

    [TestFixture]
    public class Tests_Blackbox_Addition : Setup_and_Teardown
    {
        List<string> str;
        OperatorNode node1;
        ConstantNode node2;
        ConstantNode node3;
        

        [SetUp]
        public void NewSetup()
        {
            formula = "1+5";
            mock = new Mock<ExpressionTree_Testing>(formula);// create mock
            solution = 6;
            mock.CallBase = true;// allow partial mocks

            str = new List<string> { "1", "5", "+" };

            //manually build the tree that is created by BuildTree() since the stubbed method won't create the tree, it only returns the root
            node1 = factory.CreateOperatorNode('+');
            node2 = new ConstantNode(1);
            node3 = new ConstantNode(5);
            node1.Left = node2;
            node1.Right = node3;
        }

        /// <summary>
        /// Test the ExpressionTree with all methods stubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test1_AllMethodsStubbed()
        {
            //stub Evaluate
            mock.Setup(l => l.Evaluate()).Returns(solution);

            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)
                

                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }


        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test2_Evaluate()
        {
            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsHigherPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test11_IsHigherPrecedence()
        {
            //stub IsSamePrecedence
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsSamePrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test12_IsSamePrecedence()
        {
            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsLowerPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test13_IsLowerPrecedence()
        {
            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// </summary>
        [Test]
        public void Test14_Evaluate()
        {
            ExpressionTree expressionTree = new ExpressionTree(formula);

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(solution));

            expressionTree = new ExpressionTree("");

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class Tests_Blackbox_Subtraction : Setup_and_Teardown
    {
        List<string> str;
        OperatorNode node1;
        ConstantNode node2;
        ConstantNode node3;

        [SetUp]
        public void NewSetup()
        {
            formula = "7-4";
            mock = new Mock<ExpressionTree_Testing>(formula);// create mock
            solution = 3;
            mock.CallBase = true;// allow partial mocks

            str = new List<string> { "7", "4", "-" };

            //manually build the tree that is created by BuildTree() since the stubbed method won't create the tree, it only returns the root
            node1 = factory.CreateOperatorNode('-');
            node2 = new ConstantNode(7);
            node3 = new ConstantNode(4);
            node1.Left = node2;
            node1.Right = node3;
        }

        /// <summary>
        /// Test the ExpressionTree with all methods stubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test1_AllMethodsStubbed()
        {
            //stub Evaluate
            mock.Setup(l => l.Evaluate()).Returns(solution);

            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }


        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test2_Evaluate()
        {
            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsHigherPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test11_IsHigherPrecedence()
        {
            //stub IsSamePrecedence
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsSamePrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test12_IsSamePrecedence()
        {
            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsLowerPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test13_IsLowerPrecedence()
        {
            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// </summary>
        [Test]
        public void Test14_Evaluate()
        {
            ExpressionTree expressionTree = new ExpressionTree(formula);

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(solution));

            expressionTree = new ExpressionTree("");

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class Tests_Blackbox_Multiplication : Setup_and_Teardown
    {
        List<string> str;
        OperatorNode node1;
        ConstantNode node2;
        ConstantNode node3;

        [SetUp]
        public void NewSetup()
        {
            formula = "4*3";
            mock = new Mock<ExpressionTree_Testing>(formula);// create mock
            solution = 12;
            mock.CallBase = true;// allow partial mocks

            str = new List<string> { "4", "3", "*" };

            //manually build the tree that is created by BuildTree() since the stubbed method won't create the tree, it only returns the root
            node1 = factory.CreateOperatorNode('*');
            node2 = new ConstantNode(4);
            node3 = new ConstantNode(3);
            node1.Left = node2;
            node1.Right = node3;
        }

        /// <summary>
        /// Test the ExpressionTree with all methods stubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test1_AllMethodsStubbed()
        {
            //stub Evaluate
            mock.Setup(l => l.Evaluate()).Returns(solution);

            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }


        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test2_Evaluate()
        {
            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsHigherPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test11_IsHigherPrecedence()
        {
            //stub IsSamePrecedence
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsSamePrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test12_IsSamePrecedence()
        {
            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsLowerPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test13_IsLowerPrecedence()
        {
            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// </summary>
        [Test]
        public void Test14_Evaluate()
        {
            ExpressionTree expressionTree = new ExpressionTree(formula);

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(solution));

            expressionTree = new ExpressionTree("");

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class Tests_Blackbox_Division : Setup_and_Teardown
    {
        List<string> str;
        OperatorNode node1;
        ConstantNode node2;
        ConstantNode node3;

        [SetUp]
        public void NewSetup()
        {
            formula = "6/3";
            mock = new Mock<ExpressionTree_Testing>(formula);// create mock
            solution = 2;
            mock.CallBase = true;// allow partial mocks

            str = new List<string> { "6", "3", "/" };

            //manually build the tree that is created by BuildTree() since the stubbed method won't create the tree, it only returns the root
            node1 = factory.CreateOperatorNode('/');
            node2 = new ConstantNode(6);
            node3 = new ConstantNode(3);
            node1.Left = node2;
            node1.Right = node3;
        }

        /// <summary>
        /// Test the ExpressionTree with all methods stubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test1_AllMethodsStubbed()
        {
            //stub Evaluate
            mock.Setup(l => l.Evaluate()).Returns(solution);

            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }


        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test2_Evaluate()
        {
            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsHigherPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test11_IsHigherPrecedence()
        {
            //stub IsSamePrecedence
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsSamePrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test12_IsSamePrecedence()
        {
            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsLowerPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test13_IsLowerPrecedence()
        {
            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// </summary>
        [Test]
        public void Test14_Evaluate()
        {
            ExpressionTree expressionTree = new ExpressionTree(formula);

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(solution));

            expressionTree = new ExpressionTree("");

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class Tests_Blackbox_Power : Setup_and_Teardown
    {
        List<string> str;
        OperatorNode node1;
        ConstantNode node2;
        ConstantNode node3;

        [SetUp]
        public void NewSetup()
        {
            formula = "3^2";
            mock = new Mock<ExpressionTree_Testing>(formula);// create mock
            solution = 9;
            mock.CallBase = true;// allow partial mocks

            str = new List<string> { "3", "2", "^" };

            //manually build the tree that is created by BuildTree() since the stubbed method won't create the tree, it only returns the root
            node1 = factory.CreateOperatorNode('^');
            node2 = new ConstantNode(3);
            node3 = new ConstantNode(2);
            node1.Left = node2;
            node1.Right = node3;
        }

        /// <summary>
        /// Test the ExpressionTree with all methods stubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test1_AllMethodsStubbed()
        {
            //stub Evaluate
            mock.Setup(l => l.Evaluate()).Returns(solution);

            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }


        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test2_Evaluate()
        {
            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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


                .Returns(false)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsHigherPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test11_IsHigherPrecedence()
        {
            //stub IsSamePrecedence
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsSamePrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test12_IsSamePrecedence()
        {
            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsLowerPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test13_IsLowerPrecedence()
        {
            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// </summary>
        [Test]
        public void Test14_Evaluate()
        {
            ExpressionTree expressionTree = new ExpressionTree(formula);

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(solution));

            expressionTree = new ExpressionTree("");

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class Tests_Blackbox_Addition_with_division_without_parenthesis_right_side : Setup_and_Teardown
    {
        List<string> str;
        OperatorNode node1;
        OperatorNode node2;
        ConstantNode node3;
        ConstantNode node4;
        ConstantNode node5;


        [SetUp]
        public void NewSetup()
        {
            formula = "4/2+2";
            mock = new Mock<ExpressionTree_Testing>(formula);// create mock
            solution = 4;
            mock.CallBase = true;// allow partial mocks

            str = new List<string> { "4", "2", "/", "2", "+" };

            //manually build the tree that is created by BuildTree() since the stubbed method won't create the tree, it only returns the root
            node1 = factory.CreateOperatorNode('+');
            node2 = factory.CreateOperatorNode('/');
            node3 = new ConstantNode(4);
            node4 = new ConstantNode(2);
            node5 = new ConstantNode(2);
            node1.Left = node4;
            node1.Right = node2;
            node2.Right = node5;
            node2.Left = node3;
        }

        /// <summary>
        /// Test the ExpressionTree with all methods stubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test1_AllMethodsStubbed()
        {
            //stub Evaluate
            mock.Setup(l => l.Evaluate()).Returns(solution);

            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }


        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test2_Evaluate()
        {
            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
                .Returns(false)


                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsHigherPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test11_IsHigherPrecedence()
        {
            //stub IsSamePrecedence
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsSamePrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test12_IsSamePrecedence()
        {
            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsLowerPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test13_IsLowerPrecedence()
        {
            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// </summary>
        [Test]
        public void Test14_Evaluate()
        {
            ExpressionTree expressionTree = new ExpressionTree(formula);

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(solution));

            expressionTree = new ExpressionTree("");

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class Tests_Blackbox_Addition_with_division_without_parenthesis_left_side : Setup_and_Teardown
    {   
        List<string> str;
        OperatorNode node1;
        OperatorNode node2;
        ConstantNode node3;
        ConstantNode node4;
        ConstantNode node5;


        [SetUp]
        public void NewSetup()
        {
            formula = "2+4/2";
            mock = new Mock<ExpressionTree_Testing>(formula);// create mock
            solution = 4;
            mock.CallBase = true;// allow partial mocks

            str = new List<string> { "2", "4", "2", "/", "+" };

            //manually build the tree that is created by BuildTree() since the stubbed method won't create the tree, it only returns the root
            node1 = factory.CreateOperatorNode('+');
            node2 = factory.CreateOperatorNode('/');
            node3 = new ConstantNode(4);
            node4 = new ConstantNode(2);
            node5 = new ConstantNode(2);
            node1.Left = node2;
            node1.Right = node4;
            node2.Right = node5;
            node2.Left = node3;
        }

        /// <summary>
        /// Test the ExpressionTree with all methods stubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test1_AllMethodsStubbed()
        {
            //stub Evaluate
            mock.Setup(l => l.Evaluate()).Returns(solution);

            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(true)
                .Returns(true)
                .Returns(true)
                .Returns(false)
                .Returns(false);

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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }


        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// Since the constructor isn't called, the test method builds the expression tree.
        /// The final Assert statement also calls root.Evaluate instead of the <class object>.Evaluate because the mock interface can't contain static members
        /// </summary>
        [Test]
        public void Test2_Evaluate()
        {
            //stub BuildTree to return the root node
            mock.Setup(l => l.BuildTree(formula)).Returns(node1);

            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false)


                .Returns(true)
                .Returns(true)
                .Returns(true)
                .Returns(false)
                .Returns(false);

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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            //stub the shunting yard algorithm
            mock.Setup(l => l.ShuntingYardAlgorithm(formula)).Returns(str);

            //stub IsOperatorOrParenthesis. first set of calls is in Shunting Yard algorithm on the expression, second set of calls is in build tree on postfix
            mock.SetupSequence(l => l.IsOperatorOrParenthesis(It.IsAny<char>()))
                .Returns(false)
                .Returns(false)
                .Returns(true)
                .Returns(false)
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
                .Returns(false)


                .Returns(true)
                .Returns(true)
                .Returns(true)
                .Returns(false)
                .Returns(false);

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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
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
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsHigherPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test11_IsHigherPrecedence()
        {
            //stub IsSamePrecedence
            mock.Setup(l => l.IsSamePrecedence('+', '+')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('+', '-')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('-', '+')).Returns(true);

            mock.Setup(l => l.IsSamePrecedence('*', '*')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('*', '/')).Returns(true);
            mock.Setup(l => l.IsSamePrecedence('/', '*')).Returns(true);

            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsSamePrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test12_IsSamePrecedence()
        {
            //stub IsLowerPrecedence
            mock.Setup(l => l.IsLowerPrecedence('+', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '*')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('+', '/')).Returns(true);
            mock.Setup(l => l.IsLowerPrecedence('-', '/')).Returns(true);

            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with IsLowerPrecedence unstubbed.
        /// </summary>
        [Test]
        public void Test13_IsLowerPrecedence()
        {
            ExpressionTree_Testing mockTree = mock.Object;

            Assert.That(mockTree.Evaluate(), Is.EqualTo(solution));
        }

        /// <summary>
        /// Test ExpressionTree with Evaluate unstubbed.
        /// </summary>
        [Test]
        public void Test14_Evaluate()
        {
            ExpressionTree expressionTree = new ExpressionTree(formula);

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(solution));

            expressionTree = new ExpressionTree("");

            Assert.That(expressionTree.Evaluate(), Is.EqualTo(0));
        }
    }
}