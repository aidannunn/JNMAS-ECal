// <copyright file="PowerOperatorNode.cs" company="PlaceholderCompany">
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
    /// Class which represents a tree node that holds the power operator.
    /// </summary>
    public class PowerOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerOperatorNode"/> class.
        /// </summary>
        public PowerOperatorNode()
        {
        }

        /// <summary>
        /// Gets division operator.
        /// </summary>
        public static char Operator => '^';

        /// <summary>
        /// Gets operator precedence.
        /// </summary>
        public static ushort Precedence => 3;

        /// <summary>
        /// Gets operator associativity.
        /// </summary>
        public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Method which performs the power operation on this node's children.
        /// </summary>
        /// <returns>Exponentiation of this node's left and right children.</returns>
        public override double Evaluate()
        {
            return Math.Pow(this.Left.Evaluate(), this.Right.Evaluate());
        }
    }
}
