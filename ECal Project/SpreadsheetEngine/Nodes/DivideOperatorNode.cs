// <copyright file="DivideOperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CalculatorEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class which represents a tree node that holds the division operator.
    /// </summary>
    public class DivideOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivideOperatorNode"/> class.
        /// </summary>
        public DivideOperatorNode()
        {
        }

        /// <summary>
        /// Gets division operator.
        /// </summary>
        public static char Operator => '/';

        /// <summary>
        /// Gets operator precedence.
        /// </summary>
        public static ushort Precedence => 2;

        /// <summary>
        /// Gets operator associativity.
        /// </summary>
        public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Method which divides this node's children.
        /// </summary>
        /// <returns>Dividend of this node's left and right children.</returns>
        public override double Evaluate()
        {
            if (this.Right.Evaluate() == 0)
            {
                throw new Exception("Division by 0");
            }

            return this.Left.Evaluate() / this.Right.Evaluate();
        }
    }
}
