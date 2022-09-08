// <copyright file="PlusOperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class which represents a tree node that holds the addition operator.
    /// </summary>
    public class PlusOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlusOperatorNode"/> class.
        /// </summary>
        public PlusOperatorNode()
        {
        }

        /// <summary>
        /// Gets addition operator.
        /// </summary>
        public static char Operator => '+';

        /// <summary>
        /// Gets operator precedence.
        /// </summary>
        public static ushort Precedence => 1;

        /// <summary>
        /// Gets operator associativity.
        /// </summary>
        public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Method which sums this node's children.
        /// </summary>
        /// <returns>Sum of this node's left and right children.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() + this.Right.Evaluate();
        }
    }
}
