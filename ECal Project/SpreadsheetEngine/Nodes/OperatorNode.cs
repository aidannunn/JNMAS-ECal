// <copyright file="OperatorNode.cs" company="PlaceholderCompany">
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
    /// Operator node parent class to specialized operator node classes.
    /// </summary>
    public abstract class OperatorNode : ExpressionTreeNode
    {
        /// <summary>
        /// Left or right association enumerable.
        /// </summary>
        public enum Associative
        {
            /// <summary>
            /// Right association.
            /// </summary>
            Right,

            /// <summary>
            /// Left association.
            /// </summary>
            Left,
        }

        /// <summary>
        /// Gets or sets left node pointer.
        /// </summary>
        public ExpressionTreeNode Left { get; set; }

        /// <summary>
        /// Gets or sets right node pointer.
        /// </summary>
        public ExpressionTreeNode Right { get; set; }
    }
}
