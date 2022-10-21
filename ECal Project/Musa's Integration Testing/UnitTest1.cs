namespace Musa_s_Integration_Testing
{
    using NUnit.Framework;
    using SpreadsheetEngine;
    using Moq;

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
            opList.Add('+');
            opList.Add('-');
            opList.Add('*');
            opList.Add('/');
            opList.Add('^');

            //For GetAssociativity
            OperatorNode.Associative ANode = OperatorNode.Associative.Left;


            //Stub TraverseAvaliableOperators
            mock.Verify(l => l.TraverseAvailableOperators((op, type) => operators.Add(op, type)), Times.Once());

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
            mock.Setup(l => l.GetPrecedence('*')).Returns(1);
            mock.Setup(l => l.GetPrecedence('/')).Returns(1);
            mock.Setup(l => l.GetPrecedence('^')).Returns(1);

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

            //For CreateOperatorr
            OperatorNodeFactory factory = new OperatorNodeFactory();
            OperatorNode nodePlus = factory.CreateOperatorNode('+');
            OperatorNode nodeMinus = factory.CreateOperatorNode('-');
            OperatorNode nodeMult = factory.CreateOperatorNode('*');
            OperatorNode nodeDiv = factory.CreateOperatorNode('/');
            OperatorNode nodeSqr = factory.CreateOperatorNode('^');

            //For GetOperators
            List<char> opList = new List<char>();
            opList.Add('+');
            opList.Add('-');
            opList.Add('*');
            opList.Add('/');
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
            mock.Setup(l => l.GetPrecedence('*')).Returns(1);
            mock.Setup(l => l.GetPrecedence('/')).Returns(1);
            mock.Setup(l => l.GetPrecedence('^')).Returns(1);

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

            Assert.That(mockOperator.CreateOperatorNode('+'), Is.EqualTo(nodePlus));
            Assert.That(mockOperator.CreateOperatorNode('-'), Is.EqualTo(nodeMinus));
            Assert.That(mockOperator.CreateOperatorNode('*'), Is.EqualTo(nodeMult));
            Assert.That(mockOperator.CreateOperatorNode('/'), Is.EqualTo(nodeDiv));
            Assert.That(mockOperator.CreateOperatorNode('^'), Is.EqualTo(nodeSqr));

        }

        [Test]
        public void Test3_IsOperator()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            //For GetOperators
            List<char> opList = new List<char>();
            opList.Add('+');
            opList.Add('-');
            opList.Add('*');
            opList.Add('/');
            opList.Add('^');

            //For GetAssociativity
            OperatorNode.Associative ANode = OperatorNode.Associative.Left;

            //Stub GetPrecedence
            mock.Setup(l => l.GetPrecedence('+')).Returns(1);
            mock.Setup(l => l.GetPrecedence('-')).Returns(1);
            mock.Setup(l => l.GetPrecedence('*')).Returns(1);
            mock.Setup(l => l.GetPrecedence('/')).Returns(1);
            mock.Setup(l => l.GetPrecedence('^')).Returns(1);

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
            Assert.That(mockOperator.IsOperator('+'), Is.True);
            Assert.That(mockOperator.IsOperator('-'), Is.True);
            Assert.That(mockOperator.IsOperator('*'), Is.True);
            Assert.That(mockOperator.IsOperator('/'), Is.True);
            Assert.That(mockOperator.IsOperator('^'), Is.True);

        }

        [Test]
        public void Test3_GetPrecedence()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            //For GetOperators
            List<char> opList = new List<char>();
            opList.Add('+');
            opList.Add('-');
            opList.Add('*');
            opList.Add('/');
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
            Assert.That(mockOperator.GetPrecedence('*'), Is.EqualTo(1));
            Assert.That(mockOperator.GetPrecedence('/'), Is.EqualTo(1));
            Assert.That(mockOperator.GetPrecedence('^'), Is.EqualTo(1));
        }

        [Test]
        public void Test4_GetOperators()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            //For GetOperators
            List<char> opList = new List<char>();
            opList.Add('+');
            opList.Add('-');
            opList.Add('*');
            opList.Add('/');
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
        }

        [Test]
        public void Test5_GetAssociativity()
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