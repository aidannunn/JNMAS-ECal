using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECalProject
{
    public class MathCalculation
    {
        //Add tests for error cases. Discuss with the team which kinds of testing we should use. Each team member should work on testing methods.
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
        public double factorial(double n)
        {
            if (n < 0 || n - Math.Floor(n) != 0)
                throw new Exception("ERROR: Invalid input");/*Wrong value*/
            if (n == 0)
                return (1);  /*Terminating condition*/
            else
            {
                return (n * factorial(n - 1));
            }
        }

        //Percent
        public double percent(double num)
        {
            return num / 100;
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

        public double piFunction()
        {
            return Math.PI;
        }
    }
}
