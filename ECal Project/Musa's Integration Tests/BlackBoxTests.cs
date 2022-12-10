using Moq;
using NUnit.Framework;
using SpreadsheetEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musa_s_Integration_Tests
{
    public class BlackBoxTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void testBVAGetPrecedence()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            //Assert GetPresedence
            OperatorNodeFactory_IntegrationTesting mockOperator = mock.Object;


            int min = int.MinValue;
            int minUp = int.MinValue + 1;
            int nom = 1;
            int maxDown = int.MaxValue - 1;
            int max = int.MaxValue;
            Assert.AreNotEqual(mockOperator.GetPrecedence('+'), min);
            Assert.AreNotEqual(mockOperator.GetPrecedence('+'), minUp);
            Assert.AreEqual(mockOperator.GetPrecedence('+'), nom);
            Assert.AreNotEqual(mockOperator.GetPrecedence('+'), maxDown);
            Assert.AreNotEqual(mockOperator.GetPrecedence('+'), max);


            Assert.AreNotEqual(mockOperator.GetPrecedence('-'), min);
            Assert.AreNotEqual(mockOperator.GetPrecedence('-'), minUp);
            Assert.AreEqual(mockOperator.GetPrecedence('-'), nom);
            Assert.AreNotEqual(mockOperator.GetPrecedence('-'), maxDown);
            Assert.AreNotEqual(mockOperator.GetPrecedence('-'), max);


            min = int.MinValue;
            minUp = int.MinValue + 1;
            nom = 2;
            maxDown = int.MaxValue - 1;
            max = int.MaxValue;

            Assert.AreNotEqual(mockOperator.GetPrecedence('*'), min);
            Assert.AreNotEqual(mockOperator.GetPrecedence('*'), minUp);
            Assert.AreEqual(mockOperator.GetPrecedence('*'), nom);
            Assert.AreNotEqual(mockOperator.GetPrecedence('*'), maxDown);
            Assert.AreNotEqual(mockOperator.GetPrecedence('*'), max);

            Assert.AreNotEqual(mockOperator.GetPrecedence('/'), min);
            Assert.AreNotEqual(mockOperator.GetPrecedence('/'), minUp);
            Assert.AreEqual(mockOperator.GetPrecedence('/'), nom);
            Assert.AreNotEqual(mockOperator.GetPrecedence('/'), maxDown);
            Assert.AreNotEqual(mockOperator.GetPrecedence('/'), max);



            min = int.MinValue;
            minUp = int.MinValue + 1;
            nom = 3;
            maxDown = int.MaxValue - 1;
            max = int.MaxValue;

            Assert.AreNotEqual(mockOperator.GetPrecedence('^'), min);
            Assert.AreNotEqual(mockOperator.GetPrecedence('^'), minUp);
            Assert.AreEqual(mockOperator.GetPrecedence('^'), nom);
            Assert.AreNotEqual(mockOperator.GetPrecedence('^'), maxDown);
            Assert.AreNotEqual(mockOperator.GetPrecedence('^'), max);
            Assert.That(mockOperator.GetPrecedence('^'), Is.EqualTo(3));


        }

        [Test]
        public void testBVACreateOperator()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            //Assert CreateOperator
            OperatorNodeFactory_IntegrationTesting mockOperator = mock.Object;

            OperatorNodeFactory factory = new OperatorNodeFactory();
            OperatorNode nodePlus = factory.CreateOperatorNode('+');

            //Converting operators into their ascii value to text max and min

            //Addition
            int min = 0;
            int minUp = 1;
            int nom = 43;
            int maxDown = 126;
            int max = 127;

            Assert.AreNotEqual(min, '+');
            Assert.AreNotEqual(minUp, '+');
            Assert.AreEqual(nom, '+');
            Assert.AreNotEqual(maxDown, '+');
            Assert.AreNotEqual(max, '+');

            Assert.IsInstanceOf<OperatorNode>(mockOperator.CreateOperatorNode('+'));


            //Subtraction
            nom = 45;

            Assert.AreNotEqual(min, '-');
            Assert.AreNotEqual(minUp, '-');
            Assert.AreEqual(nom, '-');
            Assert.AreNotEqual(maxDown, '-');
            Assert.AreNotEqual(max, '-');

            Assert.IsInstanceOf<OperatorNode>(mockOperator.CreateOperatorNode('-'));


            //Multiplication

            nom = 42;
            Assert.AreNotEqual(min, '*');
            Assert.AreNotEqual(minUp, '*');
            Assert.AreEqual(nom, '*');
            Assert.AreNotEqual(maxDown, '*');
            Assert.AreNotEqual(max, '*');

            Assert.IsInstanceOf<OperatorNode>(mockOperator.CreateOperatorNode('*'));

            //Division 
            nom = 47;
            Assert.AreNotEqual(min, '/');
            Assert.AreNotEqual(minUp, '/');
            Assert.AreEqual(nom, '/');
            Assert.AreNotEqual(maxDown, '/');
            Assert.AreNotEqual(max, '/');

            Assert.IsInstanceOf<OperatorNode>(mockOperator.CreateOperatorNode('/'));

            //Power 
            nom = 94;
            Assert.AreNotEqual(min, '^');
            Assert.AreNotEqual(minUp, '^');
            Assert.AreEqual(nom, '^');
            Assert.AreNotEqual(maxDown, '^');
            Assert.AreNotEqual(max, '^');

            Assert.IsInstanceOf<OperatorNode>(mockOperator.CreateOperatorNode('^'));



        }

        [Test]
        public void testBVAIsOperator()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;

            OperatorNodeFactory_IntegrationTesting mockOperator = mock.Object;


            //Addition
            Assert.IsTrue(mockOperator.IsOperator('+'));
            Assert.AreNotEqual(false,(mockOperator.IsOperator('+')));
            Assert.AreNotEqual(10, mockOperator.IsOperator('+'));

            //Subraction
            Assert.IsTrue(mockOperator.IsOperator('-'));
            Assert.AreNotEqual(false, (mockOperator.IsOperator('-')));
            Assert.AreNotEqual(10, mockOperator.IsOperator('-'));

            //Multiplication
            Assert.IsTrue(mockOperator.IsOperator('*'));
            Assert.AreNotEqual(false, (mockOperator.IsOperator('*')));
            Assert.AreNotEqual(10, mockOperator.IsOperator('*'));

            //Division
            Assert.IsTrue(mockOperator.IsOperator('/'));
            Assert.AreNotEqual(false, (mockOperator.IsOperator('/')));
            Assert.AreNotEqual(10, mockOperator.IsOperator('/'));

            //Power
            Assert.IsTrue(mockOperator.IsOperator('^'));
            Assert.AreNotEqual(false, (mockOperator.IsOperator('^')));
            Assert.AreNotEqual(10, mockOperator.IsOperator('^'));


        }

        [Test]
        public void testBVAGetOperator()
        {
            var mock = new Mock<OperatorNodeFactory_IntegrationTesting>();
            mock.CallBase = true;
            OperatorNodeFactory_IntegrationTesting mockOperator = mock.Object;


            //min
            List<int> min = new List<int>();
            min.Add(0);
            min.Add(0);
            min.Add(0);
            min.Add(0);
            min.Add(0);

            //minUp
            List<int> minUp = new List<int>();
            minUp.Add(1);
            minUp.Add(1);
            minUp.Add(1);
            minUp.Add(1);
            minUp.Add(1);


            //Nom
            List<int> nom = new List<int>();
            nom.Add(47);
            nom.Add(45);
            nom.Add(42);
            nom.Add(43);
            nom.Add(94);

            //maxDown
            List<int> maxDown = new List<int>();
            maxDown.Add(126);
            maxDown.Add(126);
            maxDown.Add(126);
            maxDown.Add(126);
            maxDown.Add(126);

            //max
            List<int> max = new List<int>();
            max.Add(127);
            max.Add(127);
            max.Add(127);
            max.Add(127);
            max.Add(127);

            Assert.AreNotEqual(min, mockOperator.GetOperators());
            Assert.AreNotEqual(minUp, mockOperator.GetOperators());
            Assert.AreEqual(nom,mockOperator.GetOperators());
            Assert.AreNotEqual(maxDown, mockOperator.GetOperators());
            Assert.AreNotEqual(max, mockOperator.GetOperators());
        }

    }
}
