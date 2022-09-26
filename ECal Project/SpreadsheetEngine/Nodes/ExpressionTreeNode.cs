// <copyright file="ExpressionTreeNode.cs" company="PlaceholderCompany">
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
    /// Class which acts as a tree node for ExpressionTree.cs.
    /// </summary>
    public abstract class ExpressionTreeNode
    {
        /// <summary>
        /// Template method for ExpressionTreeNode children.
        /// </summary>
        /// <returns>Double value.</returns>
        public abstract double Evaluate();
    }
}
