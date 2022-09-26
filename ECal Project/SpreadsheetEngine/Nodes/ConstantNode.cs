// <copyright file="ConstantNode.cs" company="PlaceholderCompany">
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
    /// Class that represents a tree node which holds a constant numerical value.
    /// </summary>
    public class ConstantNode : ExpressionTreeNode
    {
        private readonly double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value">Numerical value.</param>
        public ConstantNode(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Method which evaluates the value of the node.
        /// </summary>
        /// <returns>Node value.</returns>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
