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
            MethodInfo methodInfo = typeof(ExpressionTree).GetMethod("IsOperatorOrParenthesis", BindingFlags.Public | BindingFlags.Instance);//get the method info from the class
            
            object[] parameter1 = { '+' };//parameter to be passed into private method. passed in as array
            object result1 = methodInfo.Invoke(testObject, parameter1);//result stores the result of calling the method
            Assert.That(result1, Is.EqualTo(true));

            object[] parameter2 = { '(' };//parameter to be passed into private method. passed in as array
            object result2 = methodInfo.Invoke(testObject, parameter2);//result stores the result of calling the method
            Assert.That(result2, Is.EqualTo(true));

            object[] parameter3 = { ')' };//parameter to be passed into private method. passed in as array
            object result3 = methodInfo.Invoke(testObject, parameter3);//result stores the result of calling the method
            Assert.That(result3, Is.EqualTo(true));

            object[] parameter4 = { '8' };//parameter to be passed into private method. passed in as array
            object result4 = methodInfo.Invoke(testObject, parameter4);//result stores the result of calling the method
            Assert.That(result4, Is.EqualTo(false));

            object[] parameter5 = { '-' };//parameter to be passed into private method. passed in as array
            object result5 = methodInfo.Invoke(testObject, parameter5);//result stores the result of calling the method
            Assert.That(result5, Is.EqualTo(true));

            object[] parameter6 = { '*' };//parameter to be passed into private method. passed in as array
            object result6 = methodInfo.Invoke(testObject, parameter6);//result stores the result of calling the method
            Assert.That(result6, Is.EqualTo(true));

            object[] parameter7 = { '/' };//parameter to be passed into private method. passed in as array
            object result7 = methodInfo.Invoke(testObject, parameter7);//result stores the result of calling the method
            Assert.That(result7, Is.EqualTo(true));

            object[] parameter8 = { '^' };//parameter to be passed into private method. passed in as array
            object result8 = methodInfo.Invoke(testObject, parameter8);//result stores the result of calling the method
            Assert.That(result8, Is.EqualTo(true));
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

            object[] parameter3 = { '5' };//parameter to be passed into private method. passed in as array
            object result3 = methodInfo.Invoke(testObject, parameter3);//result stores the result of calling the method
            Assert.That(result3, Is.EqualTo(false));
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

            object[] parameter3 = { '5' };//parameter to be passed into private method. passed in as array
            object result3 = methodInfo.Invoke(testObject, parameter3);//result stores the result of calling the method
            Assert.That(result3, Is.EqualTo(false));
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