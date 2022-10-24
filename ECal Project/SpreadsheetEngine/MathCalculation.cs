using System;

namespace SpreadsheetEngine
{
    public class MathCalculation
    {

        //Square root
        public double squareRoot(double num)
        {
            return Math.Sqrt(num);
        }

        //power
        public double power(double num, double power)
        {
            return Math.Pow(num, power);
        }

        //Natural log
        public double ln(double num)
        {
            return Math.Log(num);
        }

        //Log
        public double log(double num)
        {
            return Math.Log10(num);
        }

        //Sine
        public double sine(double num)
        {
            return Math.Sin(num);
        }

        //Cosine
        public double cosine(double num)
        {
            return Math.Cos(num);
        }

        //Tangent
        public double tangent(double num)
        {
            return Math.Tan(num);
        }

        //Radian
        public double radian(double num)
        {
            return (Math.PI / 180) * num;
        }

        //Factorial
        public double factorial(double num)
        {
            double ans = 1;

            while(num != 0)
            {
                ans *= num;
                num -= 1;
            }

            return ans;
        }

        //Percent
        public double percent(double num1, double num2)
        {
            double prod = num1 * num2;
            return prod / 100;
        }

        //e-constant (euler's constant)
        public double eConstant(double num)
        {
            return Math.Pow(Math.E, num);
        }

        //Inverse function
        public double inverseFunction(double num)
        {
            return 1 / num;
        }
    }
}
