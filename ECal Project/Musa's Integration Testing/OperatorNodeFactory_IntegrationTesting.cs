using SpreadsheetEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Musa_s_Integration_Testing
{
    public class OperatorNodeFactory_IntegrationTesting : IOperatorNodeFactory_IntegrationTesting
    {

        private Dictionary<char, Type> operators = new Dictionary<char, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory_IntegrationTesting()
        {
            this.TraverseAvailableOperators((op, type) => this.operators.Add(op, type));
        }

        public delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Gets a character list of operators from the dictionary.
        /// </summary>
        /// <returns>List of char operators.</returns>
        public List<char> GetOperators()
        {
            List<char> opList = new List<char>();
            foreach (char key in this.operators.Keys)
            {
                opList.Add(key);
            }

            return opList;
        }

        /// <summary>
        /// Checks if the input character is a key in the operators dictionary.
        /// </summary>
        /// <param name="c">Operator character.</param>
        /// <returns>Boolean true or false.</returns>
        public bool IsOperator(char c)
        {
            if (this.operators.Keys.Contains(c))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Builds a new operator node depending on the operator character passed in as a parameter.
        /// </summary>
        /// <param name="op">Operator character.</param>
        /// <returns>Operator node child.</returns>
        public OperatorNode CreateOperatorNode(char op)
        {
            if (this.operators.ContainsKey(op))
            {
                object operatorNodeObject = System.Activator.CreateInstance(this.operators[op]);
                if (operatorNodeObject is OperatorNode)
                {
                    return (OperatorNode)operatorNodeObject;
                }
            }

            throw new Exception("Unhandled operator");
        }

        /// <summary>
        /// Gets the precedence of an operator character by checking it against the dictionary of operators and types.
        /// </summary>
        /// <param name="op">Operator character.</param>
        /// <returns>Ushort value representing precedence.</returns>
        public ushort GetPrecedence(char op)
        {
            ushort output = 0;

            if (this.IsOperator(op))
            {
                Type type = this.operators[op];

                PropertyInfo pField = type.GetProperty("Precedence");

                if (pField != null)
                {
                    object value = pField.GetValue(type);

                    if (value.GetType().Name == "UInt16")
                    {
                        output = (ushort)value;
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Gets the associativity of an operator character by checking it against the dictionary of operators and types.
        /// </summary>
        /// <param name="op">Operator character.</param>
        /// <returns>Left or right associativity.</returns>
        public OperatorNode.Associative GetAssociativity(char op)
        {
            OperatorNode.Associative output = OperatorNode.Associative.Left;

            if (this.IsOperator(op))
            {
                Type type = this.operators[op];

                PropertyInfo aField = type.GetProperty("Associativity");

                if (aField != null)
                {
                    object value = aField.GetValue(type);
                    string s = value.GetType().Name;

                    if (value.GetType().Name == "Associative")
                    {
                        output = (OperatorNode.Associative)value;
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Views the available operator nodes and adds them to the dictionary via the constructor.
        /// </summary>
        /// <param name="onOperator">Delegate.</param>
        public void TraverseAvailableOperators(OnOperator onOperator)
        {
            Type operatorNodeType = typeof(OperatorNode);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                foreach (var type in operatorTypes)
                {
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        object value = operatorField.GetValue(type);

                        if (value is char)
                        {
                            char operatorSymbol = (char)value;
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
