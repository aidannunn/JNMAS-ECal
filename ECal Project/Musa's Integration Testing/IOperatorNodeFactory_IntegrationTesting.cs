using SpreadsheetEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musa_s_Integration_Testing
{
    public interface IOperatorNodeFactory_IntegrationTesting
    {
        public delegate void OnOperator(char op, Type type);
        List<char> GetOperators();

        bool IsOperator(char c);

        OperatorNode CreateOperatorNode(char op);

        ushort GetPrecedence(char op);

        OperatorNode.Associative GetAssociativity(char op);

        void TraverseAvailableOperators(OnOperator onOperator);

    }
}
