using System;

namespace Assignment1
{
    class Task4
    {
        static void Main(string[] args)
        {
            double x; //input
            double sin = 0; //output

            //define x
            Console.WriteLine("Enter x: ");
            x = RemoveUnnecessaryPi(Convert.ToDouble(Console.ReadLine()));

            int k = 1;
            double factorial = 1;
            double power = 0;

            //find sin(x)
            for (int i = 0; i < 50; i++)
            {
                k = K(i);
                factorial = Factorial(factorial, 2 * i + 1);
                power = Pow(power, x * Math.PI, 2 * i + 1);
                sin += k * (power / factorial);
            }
            double mathSin = Math.Sin(x * Math.PI);
            //show results: finded sin(x), Math.Sin(x) and it's delta
            Console.WriteLine("sin(x) = " + sin);
            Console.WriteLine("Math.Sin(x) = " + mathSin);
            Console.WriteLine("Delta = " + (mathSin - sin));
        }

        //Set x within [-2π; 2π]
        static double RemoveUnnecessaryPi(double x)
        {
            double npi = x / Math.PI; //find n from x = nπ

            //if x in [-2π; 2π], return converted x
            if (-2 * Math.PI < x && x < 2 * Math.PI)
            {
                return npi;
            }
            //if not, bring x to [-2π; 2π]
            else
            {
                if (x < 0)
                {
                    while (npi < -2 * Math.PI)
                    {
                        npi += 2 * Math.PI;
                    }
                }
                else
                {
                    while (npi > 2 * Math.PI)
                    {
                        npi -= 2 * Math.PI;
                    }
                }
                return npi;
            }
        }

        //find n power of x
        static double Pow(double power, double x, int n)
        {
            if (n == 0)
            {
                return 1;
            }
            else if (n == 1)
            {
                return x;
            }
            else
            {
                power *= x * x;
                return power;
            }
        }

        //find factorial of n
        static double Factorial(double factorial, int n)
        {
            factorial *= (n - 1) * n;
            if (factorial == 0)
            {
                factorial = 1;
            }
            return factorial;
        }

        //find k
        static int K(int n)
        {
            if (n % 2 == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}