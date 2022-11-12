using System;

namespace Assignment1
{
    class Task4
    {
        static void Main(string[] args)
        {
            //test input:
            //case 1: x = 3
            //case 2: x = 5
            //case 3: x = 9
            //case 4: x = 13
            //case 5: x = 35
            //case 6: x = 457
            //case 7: x = 5358
            //case 8: x = -4
            //case 9: x = -234
            //case 10: x = -2355

            double x; //input
            double sin = 0; //output

            //define x
            Console.WriteLine("Enter x: ");
            x = RemoveUnnecessaryPi(Convert.ToDouble(Console.ReadLine()));

            //find sin(x)
            for (int i = 0; i < 50; i++)
            {
                sin += Pow(-1, i) * (Pow(x * Math.PI, (2 * i + 1)) / Factorial(2 * i + 1));
            }

            //show results: finded sin(x), Math.Sin(x) and it's delta
            Console.WriteLine("sin(x) = " + sin);
            Console.WriteLine("Math.Sin(x) = " + Math.Sin(x * Math.PI));
            Console.WriteLine("Delta = " + (Math.Sin(x * Math.PI) - sin));

            //test output:
            //case 1: sin(x) ≈ 0.1411
            //case 2: sin(x) ≈ -0.9589
            //case 3: sin(x) ≈ 0.4121
            //case 4: sin(x) ≈ 0.4202
            //case 5: sin(x) ≈ -0.4282
            //case 6: sin(x) ≈ -0.9948
            //case 7: sin(x) ≈ -0.9999
            //case 8: sin(x) ≈ 0.7568
            //case 9: sin(x) ≈ -0.9988
            //case 10: sin(x) ≈ 0.9300

            //got:
            //case 1: sin(x) = 0.1411200080598671, Math.Sin(x) = 0.1411200080598672, Delta ≈ 0   +
            //case 2: sin(x) = -0.9589242746631357, Math.Sin(x) = -0.9589242746631385, Delta ≈ 0 +
            //case 3: sin(x) = 0.4121184852419005, Math.Sin(x) = 0.4121184852417566, Delta ≈ 0   +
            //case 4: sin(x) = 0.4201670368211333, Math.Sin(x) = 0.4201670368266393, Delta ≈ 0   +
            //case 5: sin(x) = 0.43241739707935445, Math.Sin(x) = 0.43241739699677445, Delta ≈ 0 +
            //case 6: sin(x) = 0.1429041899416194, Math.Sin(x) = 0.14290418994161935, Delta ≈ 0  +
            //case 7: sin(x) = 0.6819045750398767, Math.Sin(x) = 0.6819045750399624, Delta ≈ 0   +
            //case 8: sin(x) = 0.7568024953079275, Math.Sin(x) = 0.7568024953079282, Delta ≈ 0   +
            //case 9: sin(x) = 3.2442957833995775E+78, Math.Sin(x) = -0.9988166912028097, Delta = -3.2442957833995775E+78 -
            //case 10:sin(x) = NaN, Math.Sin(x) = 0.9300284271630885, Delta = NaN -
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
                    while (npi > -2 * Math.PI)
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
        static double Pow(double x, int n)
        {
            if (n == 0)
            {
                return 1;
            }
            double power = x;
            for (int i = 0; i < n - 1; i++)
            {
                power *= x;
            }
            return power;
        }

        //find factorial of n
        static double Factorial(int n)
        {
            double factorial = 1;
            for (int i = 2; i < n + 1; i++)
            {
                factorial *= i;
            }
            return factorial;
        }
    }
}