// <copyright file="MultiplyOperatorNode.cs" company="PlaceholderCompany">
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
    /// Class which represents a tree node that holds the multiplication operator.
    /// </summary>
    public class MultiplyOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplyOperatorNode"/> class.
        /// </summary>
        public MultiplyOperatorNode()
        {
        }

        /// <summary>
        /// Gets addition operator.
        /// </summary>
        public static char Operator => '*';

        /// <summary>
        /// Gets operator precedence.
        /// </summary>
        public static ushort Precedence => 2;

        /// <summary>
        /// Gets operator associativity.
        /// </summary>
        public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Method which mulitplies this node's children.
        /// </summary>
        /// <returns>Product of this node's left and right children.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() * this.Right.Evaluate();
        }
    }
}
