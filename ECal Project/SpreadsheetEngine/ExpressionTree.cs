// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class which will handle evaluating cell expressions in the spreadsheet.
    /// </summary>
    public class ExpressionTree
    {
        private ExpressionTreeNode root;

        private OperatorNodeFactory factory = new OperatorNodeFactory();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// Construct tree from the specific expression.
        /// </summary>
        /// <param name="expression">Mathematical expression that will be parsed into an expression tree.</param>
        public ExpressionTree(string expression)
        {
            this.root = this.BuildTree(expression);
        }

        /// <summary>
        /// Evaluates the binary expression tree to a double value. No-input override.
        /// </summary>
        /// <returns>Function call to an overridden version of Evaluate() that performs the arithmetic.</returns>
        public double Evaluate()
        {
            if (this.root == null)
            {
                return 0;
            }
            else
            {
                return this.root.Evaluate();
            }
        }

        /// <summary>
        /// Sorts an expression from infix to postfix using the Shunting Yard Algorithm.
        /// </summary>
        /// <param name="expression">Mathematical expression.</param>
        /// <returns>Input expression in postfix notation.</returns>
        public virtual List<string> ShuntingYardAlgorithm(string expression)
        {
            List<string> postfix = new List<string>();
            Stack<char> operators = new Stack<char>();
            int operandStart = -1;
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (this.IsOperatorOrParenthesis(c))
                {
                    if (operandStart != -1)
                    {
                        string operand = expression.Substring(operandStart, i - operandStart);
                        postfix.Add(operand);
                        operandStart = -1;
                    }

                    if (this.IsLeftParenthesis(c))
                    {
                        operators.Push(c);
                    }
                    else if (this.IsRightParentheses(c))
                    {
                        char op = operators.Pop();
                        while (!this.IsLeftParenthesis(op))
                        {
                            postfix.Add(op.ToString());
                            op = operators.Pop();
                        }
                    }
                    else if (this.factory.IsOperator(c))
                    {
                        if (operators.Count == 0 || this.IsLeftParenthesis(operators.Peek()))
                        {
                            operators.Push(c);
                        }
                        else if (this.IsHigherPrecedence(c, operators.Peek())
                            || (this.IsSamePrecedence(c, operators.Peek()) && this.IsRightAssociative(c)))
                        {
                            operators.Push(c);
                        }
                        else if (this.IsLowerPrecedence(c, operators.Peek())
                            || (this.IsSamePrecedence(c, operators.Peek()) && this.IsLeftAssociative(c)))
                        {
                            do
                            {
                                char op = operators.Pop();
                                postfix.Add(op.ToString());
                            }
                            while (operators.Count > 0 && (this.IsLowerPrecedence(c, operators.Peek())
                                || (this.IsSamePrecedence(c, operators.Peek()) && this.IsLeftAssociative(c))));

                            operators.Push(c);
                        }
                    }
                }
                else if (operandStart == -1)
                {
                    operandStart = i;
                }
            }

            if (operandStart != -1)
            {
                postfix.Add(expression.Substring(operandStart, expression.Length - operandStart));
                operandStart = -1;
            }

            while (operators.Count > 0)
            {
                postfix.Add(operators.Pop().ToString());
            }

            return postfix;
        }

        /// <summary>
        /// Method that recursively builds a binary expression tree based on a string input.
        /// </summary>
        /// <param name="expression">Input string.</param>
        /// <returns>Binary expressiont tree output.</returns>
        public ExpressionTreeNode BuildTree(string expression)
        {
            // Check if string is null;
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }

            Stack<ExpressionTreeNode> nodes = new Stack<ExpressionTreeNode>();
            var postfix = this.ShuntingYardAlgorithm(expression);
            foreach (var item in postfix)
            {
                if (item.Length == 1 && this.IsOperatorOrParenthesis(item[0]))
                {
                    OperatorNode node = this.factory.CreateOperatorNode(item[0]);
                    node.Right = nodes.Pop();
                    node.Left = nodes.Pop();
                    nodes.Push(node);
                }
                else
                {
                    double num = 0.0;
                    if (double.TryParse(item, out num))
                    {
                        nodes.Push(new ConstantNode(num));
                    }
                    else
                    {
                        throw new Exception("Input contained non-numerical values");//nodes.Push(new VariableNode(item, ref this.variables));
                    }
                }
            }

            return nodes.Pop();
        }

        /// <summary>
        /// Checks if a character is an operator or parenthesis.
        /// </summary>
        /// <param name="v">Input character.</param>
        /// <returns>Boolean true or false.</returns>
        private bool IsOperatorOrParenthesis(char v)
        {
            if (this.factory.GetOperators().Contains(v))
            {
                return true;
            }
            else if (this.IsLeftParenthesis(v) || this.IsRightParentheses(v))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a character is a left parenthesis.
        /// </summary>
        /// <param name="c">Input character.</param>
        /// <returns>Boolean true or false.</returns>
        private bool IsLeftParenthesis(char c)
        {
            if (c == '(')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a character is a right parenthesis.
        /// </summary>
        /// <param name="c">Input character.</param>
        /// <returns>Boolean true or false.</returns>
        private bool IsRightParentheses(char c)
        {
            if (c == ')')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if an input character is left associative.
        /// </summary>
        /// <param name="c">Input character.</param>
        /// <returns>Boolean true or false.</returns>
        private bool IsLeftAssociative(char c)
        {
            if (this.factory.GetAssociativity(c) == OperatorNode.Associative.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if input character c is lower precedence that input character v.
        /// </summary>
        /// <param name="c">Left character in expression.</param>
        /// <param name="v">Right character in expression.</param>
        /// <returns>Boolean true or false.</returns>
        private bool IsLowerPrecedence(char c, char v)
        {
            if (this.factory.GetPrecedence(c) < this.factory.GetPrecedence(v))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if an input character is right associative.
        /// </summary>
        /// <param name="c">Input character.</param>
        /// <returns>Boolean true or false.</returns>
        private bool IsRightAssociative(char c)
        {
            OperatorNode node = this.factory.CreateOperatorNode(c);

            if (this.factory.GetAssociativity(c) == OperatorNode.Associative.Right)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if two characters have equal precedence.
        /// </summary>
        /// <param name="c">Left character in expression.</param>
        /// <param name="v">Right character in expression.</param>
        /// <returns>Boolean true or false.</returns>
        private bool IsSamePrecedence(char c, char v)
        {
            if (this.factory.GetPrecedence(c) == this.factory.GetPrecedence(v))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if input character c is higher precedence that input character v.
        /// </summary>
        /// <param name="c">Left character in expression.</param>
        /// <param name="v">Right character in expression.</param>
        /// <returns>Boolean true or false.</returns>
        private bool IsHigherPrecedence(char c, char v)
        {
            if (this.factory.GetPrecedence(c) > this.factory.GetPrecedence(v))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}