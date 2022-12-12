namespace Musa_s_Integration_Tests
{
    using NUnit.Framework;
    using System.Reflection;
    using SpreadsheetEngine;
    using Moq;
    using System.Collections.Generic;
    using System;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1_StubAllMethods()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            //For Constructor and TraverseAvaliableOperators()
            Dictionary<char, Type> operators = new Dictionary<char, Type>();

            //For CreateOperatorr
            OperatorNodeFactory factory = new OperatorNodeFactory();
            OperatorNode nodePlus = factory.CreateOperatorNode('+');
            OperatorNode nodeMinus = factory.CreateOperatorNode('-');
            OperatorNode nodeMult = factory.CreateOperatorNode('*');
            OperatorNode nodeDiv = factory.CreateOperatorNode('/');
            OperatorNode nodeSqr = factory.CreateOperatorNode('^');


            //For GetOperators
            List<char> opList = new List<char>();
            opList.Add('/');
            opList.Add('-');
            opList.Add('*');
            opList.Add('+');
            opList.Add('^');

            //For GetAssociativity
            OperatorNode.Associative ANode = OperatorNode.Associative.Left;


            //Stub TraverseAvaliableOperators
            //mock.Verify(l => l.TraverseAvailableOperators((op, type) => operators.Add(op, type)), Times.Once());

            //Stub CreateOperator
            mock.Setup(l => l.CreateOperatorNode('+')).Returns(nodePlus);
            mock.Setup(l => l.CreateOperatorNode('-')).Returns(nodeMinus);
            mock.Setup(l => l.CreateOperatorNode('*')).Returns(nodeMult);
            mock.Setup(l => l.CreateOperatorNode('/')).Returns(nodeDiv);
            mock.Setup(l => l.CreateOperatorNode('^')).Returns(nodeSqr);

            //Stub isOperator
            mock.Setup(l => l.IsOperator('+')).Returns(true);
            mock.Setup(l => l.IsOperator('-')).Returns(true);
            mock.Setup(l => l.IsOperator('*')).Returns(true);
            mock.Setup(l => l.IsOperator('/')).Returns(true);
            mock.Setup(l => l.IsOperator('^')).Returns(true);

            //Stub GetPrecedence
            mock.Setup(l => l.GetPrecedence('+')).Returns(1);
            mock.Setup(l => l.GetPrecedence('-')).Returns(1);
            mock.Setup(l => l.GetPrecedence('*')).Returns(2);
            mock.Setup(l => l.GetPrecedence('/')).Returns(2);
            mock.Setup(l => l.GetPrecedence('^')).Returns(3);

            //Stub GetOperators
            mock.Setup(l => l.GetOperators()).Returns(opList);

            //Stub GetAssociativity 
            mock.Setup(l => l.GetAssociativity('+')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('-')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('*')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('/')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('^')).Returns(ANode);

        }

        [Test]
        public void Test2_CreateOperator()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            //For GetOperators
            List<char> opList = new List<char>();
            opList.Add('/');
            opList.Add('-');
            opList.Add('*');
            opList.Add('+');
            opList.Add('^');

            //For GetAssociativity
            OperatorNode.Associative ANode = OperatorNode.Associative.Left;


            //Stub isOperator
            mock.Setup(l => l.IsOperator('+')).Returns(true);
            mock.Setup(l => l.IsOperator('-')).Returns(true);
            mock.Setup(l => l.IsOperator('*')).Returns(true);
            mock.Setup(l => l.IsOperator('/')).Returns(true);
            mock.Setup(l => l.IsOperator('^')).Returns(true);

            //Stub GetPrecedence
            mock.Setup(l => l.GetPrecedence('+')).Returns(1);
            mock.Setup(l => l.GetPrecedence('-')).Returns(1);
            mock.Setup(l => l.GetPrecedence('*')).Returns(2);
            mock.Setup(l => l.GetPrecedence('/')).Returns(2);
            mock.Setup(l => l.GetPrecedence('^')).Returns(3);

            //Stub GetOperators
            mock.Setup(l => l.GetOperators()).Returns(opList);

            //Stub GetAssociativity 
            mock.Setup(l => l.GetAssociativity('+')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('-')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('*')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('/')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('^')).Returns(ANode);

            //Assert CreateOperator 

            OperatorNodeFactory_IntegrationTesting mockOperator = mock.Object;

            Assert.IsInstanceOf<OperatorNode>(mockOperator.CreateOperatorNode('+'));
            Assert.IsInstanceOf<OperatorNode>(mockOperator.CreateOperatorNode('-'));
            Assert.IsInstanceOf<OperatorNode>(mockOperator.CreateOperatorNode('*'));
            Assert.IsInstanceOf<OperatorNode>(mockOperator.CreateOperatorNode('/'));
            Assert.IsInstanceOf<OperatorNode>(mockOperator.CreateOperatorNode('^'));

        }

        [Test]
        public void Test3_IsOperator()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            //For GetOperators
            List<char> opList = new List<char>();
            opList.Add('/');
            opList.Add('-');
            opList.Add('*');
            opList.Add('+');
            opList.Add('^');

            //For GetAssociativity
            OperatorNode.Associative ANode = OperatorNode.Associative.Left;

            //Stub GetPrecedence
            mock.Setup(l => l.GetPrecedence('+')).Returns(1);
            mock.Setup(l => l.GetPrecedence('-')).Returns(1);
            mock.Setup(l => l.GetPrecedence('*')).Returns(2);
            mock.Setup(l => l.GetPrecedence('/')).Returns(2);
            mock.Setup(l => l.GetPrecedence('^')).Returns(3);

            //Stub GetOperators
            mock.Setup(l => l.GetOperators()).Returns(opList);

            //Stub GetAssociativity 
            mock.Setup(l => l.GetAssociativity('+')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('-')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('*')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('/')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('^')).Returns(ANode);

            //Assert IsOperator
            OperatorNodeFactory_IntegrationTesting mockOperator = mock.Object;

            Assert.IsTrue(mockOperator.IsOperator('+'));
            Assert.IsTrue(mockOperator.IsOperator('-'));
            Assert.IsTrue(mockOperator.IsOperator('*'));
            Assert.IsTrue(mockOperator.IsOperator('/'));
            Assert.IsTrue(mockOperator.IsOperator('^'));

        }

        [Test]
        public void Test4_GetPrecedence()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            //For GetOperators
            List<char> opList = new List<char>();
            opList.Add('/');
            opList.Add('-');
            opList.Add('*');
            opList.Add('+');
            opList.Add('^');

            //For GetAssociativity
            OperatorNode.Associative ANode = OperatorNode.Associative.Left;

            //Stub GetOperators
            mock.Setup(l => l.GetOperators()).Returns(opList);

            //Stub GetAssociativity 
            mock.Setup(l => l.GetAssociativity('+')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('-')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('*')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('/')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('^')).Returns(ANode);

            //Assert GetPrecedence
            OperatorNodeFactory_IntegrationTesting mockOperator = mock.Object;
            Assert.That(mockOperator.GetPrecedence('+'), Is.EqualTo(1));
            Assert.That(mockOperator.GetPrecedence('-'), Is.EqualTo(1));
            Assert.That(mockOperator.GetPrecedence('*'), Is.EqualTo(2));
            Assert.That(mockOperator.GetPrecedence('/'), Is.EqualTo(2));
            Assert.That(mockOperator.GetPrecedence('^'), Is.EqualTo(3));
        }

        [Test]
        public void Test5_GetOperators()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            //For GetOperators
            List<char> opList = new List<char>();
            opList.Add('/');
            opList.Add('-');
            opList.Add('*');
            opList.Add('+');
            opList.Add('^');

            //For GetAssociativity
            OperatorNode.Associative ANode = OperatorNode.Associative.Left;

            //Stub GetAssociativity
            mock.Setup(l => l.GetAssociativity('+')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('-')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('*')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('/')).Returns(ANode);
            mock.Setup(l => l.GetAssociativity('^')).Returns(ANode);

            //Assert GetOperators
            OperatorNodeFactory_IntegrationTesting mockOperator = mock.Object;

            Assert.That(mockOperator.GetOperators(), Is.EqualTo(opList));
            Assert.AreEqual(mockOperator.GetOperators(), opList);
        }

        [Test]
        public void Test6_GetAssociativity()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            //Assert GetOperators
            OperatorNodeFactory_IntegrationTesting mockOperator = mock.Object;

            Assert.That(mockOperator.GetAssociativity('+'), Is.EqualTo(OperatorNode.Associative.Left));
            Assert.That(mockOperator.GetAssociativity('-'), Is.EqualTo(OperatorNode.Associative.Left));
            Assert.That(mockOperator.GetAssociativity('*'), Is.EqualTo(OperatorNode.Associative.Left));
            Assert.That(mockOperator.GetAssociativity('/'), Is.EqualTo(OperatorNode.Associative.Left));
            Assert.That(mockOperator.GetAssociativity('^'), Is.EqualTo(OperatorNode.Associative.Left));
        }
    }
}