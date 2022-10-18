using System.Collections.Generic;

namespace SpreadsheetEngine
{
    public interface IExpressionTree_IntegrationTesting
    {
        ExpressionTreeNode BuildTree(string expression);
        double Evaluate();
        bool IsHigherPrecedence(char c, char v);
        bool IsLeftAssociative(char c);
        bool IsLeftParenthesis(char c);
        bool IsLowerPrecedence(char c, char v);
        bool IsOperatorOrParenthesis(char v);
        bool IsRightAssociative(char c);
        bool IsRightParenthesis(char c);
        bool IsSamePrecedence(char c, char v);
        List<string> ShuntingYardAlgorithm(string expression);
    }
}