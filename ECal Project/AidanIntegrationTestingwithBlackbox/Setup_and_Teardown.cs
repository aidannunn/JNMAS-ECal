using Moq;
using SpreadsheetEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingExpressionTreeBlackbox
{
    public class Setup_and_Teardown
    {
        public OperatorNodeFactory factory;
        public Mock<ExpressionTree_Testing> mock = new Mock<ExpressionTree_Testing>("");// create mock;
        public double solution;
        public string formula = "";

        [SetUp]
        public void BaseSetup()
        {
            factory = new OperatorNodeFactory();
        }
    }
}
