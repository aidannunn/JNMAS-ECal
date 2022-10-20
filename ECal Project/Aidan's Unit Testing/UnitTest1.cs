using NUnit.Framework;
using SpreadsheetEngine;
using System.Reflection;

namespace Aidan_s_Unit_Testing
{
    public class Tests
    {
        private ExpressionTree testObject;

        [SetUp]
        public void Setup()
        {
            testObject = new ExpressionTree("1+2*(3*3)");// create class object
        }

        [TearDown]
        public void Teardown()
        {
            testObject = null;
        }

        /// <summary>
        /// Unit test of IsOperatorOrParenthesis() using reflections to test the private method
        /// </summary>
        [Test]
        public void Test1_IsOperatorOrParenthesis()
        {
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("IsOperatorOrParenthesis", BindingFlags.NonPublic | BindingFlags.Instance);//get the method info from the class
            
            object[] parameter1 = { '+' };//parameter to be passed into private method. passed in as array
            object result1 = methodInfo.Invoke(testObject, parameter1);//result stores the result of calling the method
            Assert.That(result1, Is.EqualTo(true));

            object[] parameter2 = { '8' };//parameter to be passed into private method. passed in as array
            object result2 = methodInfo.Invoke(testObject, parameter2);//result stores the result of calling the method
            Assert.That(result2, Is.EqualTo(false));
        }

        /// <summary>
        /// Unit test of IsLeftParenthesis() using reflections to test the private method
        /// </summary>
        [Test]
        public void Test2_IsLeftParenthesis()
        {
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("IsLeftParenthesis", BindingFlags.NonPublic | BindingFlags.Instance);//get the method info from the class

            object[] parameter1 = { '(' };//parameter to be passed into private method. passed in as array
            object result1 = methodInfo.Invoke(testObject, parameter1);//result stores the result of calling the method
            Assert.That(result1, Is.EqualTo(true));

            object[] parameter2 = { '-' };//parameter to be passed into private method. passed in as array
            object result2 = methodInfo.Invoke(testObject, parameter2);//result stores the result of calling the method
            Assert.That(result2, Is.EqualTo(false));
        }

        /// <summary>
        /// Unit test of IsRightParenthesis() using reflections to test the private method
        /// </summary>
        [Test]
        public void Test3_IsRightParenthesis()
        {
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("IsRightParentheses", BindingFlags.NonPublic | BindingFlags.Instance);//get the method info from the class

            object[] parameter1 = { ')' };//parameter to be passed into private method. passed in as array
            object result1 = methodInfo.Invoke(testObject, parameter1);//result stores the result of calling the method
            Assert.That(result1, Is.EqualTo(true));

            object[] parameter2 = { '*' };//parameter to be passed into private method. passed in as array
            object result2 = methodInfo.Invoke(testObject, parameter2);//result stores the result of calling the method
            Assert.That(result2, Is.EqualTo(false));
        }

        /// <summary>
        /// Unit test of IsLeftAssociative() using reflections to test the private method
        /// </summary>
        [Test]
        public void Test4_IsLeftAssociative()
        {
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("IsLeftAssociative", BindingFlags.NonPublic | BindingFlags.Instance);//get the method info from the class

            object[] parameter1 = { '-' };//parameter to be passed into private method. passed in as array
            object result1 = methodInfo.Invoke(testObject, parameter1);//result stores the result of calling the method
            Assert.That(result1, Is.EqualTo(true));
        }

        /// <summary>
        /// Unit test of IsLowerPrecedence() using reflections to test the private method
        /// </summary>
        [Test]
        public void Test5_IsLowerPrecedence()
        {
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("IsLowerPrecedence", BindingFlags.NonPublic | BindingFlags.Instance);//get the method info from the class
            
            object[] parameter1 = { '-', '*' };//parameter to be passed into private method. passed in as array
            object result1 = methodInfo.Invoke(testObject, parameter1);//result stores the result of calling the method
            Assert.That(result1, Is.EqualTo(true));

            object[] parameter2 = { '/', '*' };//parameter to be passed into private method. passed in as array
            object result2 = methodInfo.Invoke(testObject, parameter2);//result stores the result of calling the method
            Assert.That(result2, Is.EqualTo(false));

            object[] parameter3 = { '/', '+' };//parameter to be passed into private method. passed in as array
            object result3 = methodInfo.Invoke(testObject, parameter3);//result stores the result of calling the method
            Assert.That(result3, Is.EqualTo(false));
        }

        /// <summary>
        /// Unit test of IsSamePrecedence() using reflections to test the private method
        /// </summary>
        [Test]
        public void Test5_IsSamePrecedence()
        {
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("IsSamePrecedence", BindingFlags.NonPublic | BindingFlags.Instance);//get the method info from the class

            object[] parameter1 = { '*', '*' };//parameter to be passed into private method. passed in as array
            object result1 = methodInfo.Invoke(testObject, parameter1);//result stores the result of calling the method
            Assert.That(result1, Is.EqualTo(true));

            object[] parameter2 = { '-', '*' };//parameter to be passed into private method. passed in as array
            object result2 = methodInfo.Invoke(testObject, parameter2);//result stores the result of calling the method
            Assert.That(result2, Is.EqualTo(false));

            object[] parameter3 = { '/', '+' };//parameter to be passed into private method. passed in as array
            object result3 = methodInfo.Invoke(testObject, parameter3);//result stores the result of calling the method
            Assert.That(result3, Is.EqualTo(false));
        }

        /// <summary>
        /// Unit test of IsHigherPrecedence() using reflections to test the private method
        /// </summary>
        [Test]
        public void Test5_IsHigherPrecedence()
        {
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("IsHigherPrecedence", BindingFlags.NonPublic | BindingFlags.Instance);//get the method info from the class

            object[] parameter1 = { '*', '+' };//parameter to be passed into private method. passed in as array
            object result1 = methodInfo.Invoke(testObject, parameter1);//result stores the result of calling the method
            Assert.That(result1, Is.EqualTo(true));

            object[] parameter2 = { '-', '*' };//parameter to be passed into private method. passed in as array
            object result2 = methodInfo.Invoke(testObject, parameter2);//result stores the result of calling the method
            Assert.That(result2, Is.EqualTo(false));

            object[] parameter3 = { '/', '*' };//parameter to be passed into private method. passed in as array
            object result3 = methodInfo.Invoke(testObject, parameter3);//result stores the result of calling the method
            Assert.That(result3, Is.EqualTo(false));
        }
    }
}