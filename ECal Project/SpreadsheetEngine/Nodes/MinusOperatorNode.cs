// <copyright file="MinusOperatorNode.cs" company="PlaceholderCompany">
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
    /// Class which represents a tree node that holds the subtraction operator.
    /// </summary>
    public class MinusOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinusOperatorNode"/> class.
        /// </summary>
        public MinusOperatorNode()
        {
        }

        /// <summary>
        /// Gets subtraction operator.
        /// </summary>
        public static char Operator => '-';

        /// <summary>
        /// Gets operator precedence.
        /// </summary>
        public static ushort Precedence => 1;

        /// <summary>
        /// Gets operator associativity.
        /// </summary>
        public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Method which subtracts this node's children.
        /// </summary>
        /// <returns>Difference between this node's left and right children.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() - this.Right.Evaluate();
        }
    }
}
