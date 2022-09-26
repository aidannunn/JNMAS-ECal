// <copyright file="ExpressionTreeApp.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTreeApp
{
    using System;
    using CalculatorEngine;

    /// <summary>
    /// Simple console application for previewing the expression tree in SpreadsheetEngine.dll.
    /// </summary>
    internal class ExpressionTreeApp
    {
        private static void Main(string[] args)
        {
            string expression = "1+2+3";
            string input = string.Empty;
            string name = string.Empty;
            double value = 0.0;
            ExpressionTree tree = new ExpressionTree(expression);

            do
            {
                Console.WriteLine("Menu (current expression = " + expression + ")");
                Console.WriteLine("     1 = Enter a new expression");
                Console.WriteLine("     2  = Evaluate tree");
                Console.WriteLine("     3 = Quit");

                input = Console.ReadLine().Trim();

                switch (input)
                {
                    case "1": // Get new expression.
                        Console.WriteLine("Enter new expression: ");
                        expression = Console.ReadLine().Trim();
                        expression.Replace(Environment.NewLine, string.Empty);
                        tree = new ExpressionTree(expression);
                        break;
                    case "2":
                        Console.WriteLine(tree.Evaluate());
                        break;
                }
            }
            while (input != "3");
        }
    }
}
