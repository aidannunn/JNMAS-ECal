using Moq;
using NUnit.Framework;
using SpreadsheetEngine;
using System;
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

    }
}
