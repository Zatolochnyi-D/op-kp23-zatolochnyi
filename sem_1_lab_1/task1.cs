using System;

namespace Assignment1
{
    class Task1
    {
        static void Main(string[] args)
        {
            //test input:
            //case 1: x0 = 0; xn = 8; n = 10
            //case 2: x0 = 3; xn = -6, n = 7

            double x0, xn, n; //input
            double x, yx; //output

            Console.WriteLine("Enter x0:");
            x0 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter xn:");
            xn = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter n:");
            n = Convert.ToDouble(Console.ReadLine());

            for (int i = 1; i < n + 1; i++)
            {
                x = x0 + i * (xn - x0) / n;
                if (x <= -1 * Math.Pow(Math.E, 0.82))
                {
                    Console.WriteLine("x" + i + " = " + x + ", y(x) does not exist");
                }
                else
                {
                    yx = 0.0025 * 2.3 * Math.Pow(x, 3) + Math.Sqrt(x + Math.Pow(Math.E, 0.82));
                    Console.WriteLine("x" + i + " = " + x + ", y(x) = " + yx);
                }
            }

            //test output:
            //case 1:
            //  x1 = 0.8, y(x) ≈ 1.755
            //  x2 = 1.6, y(x) ≈ 1.991
            //  x3 = 2.4, y(x) ≈ 2.241
            //  x4 = 3.2, y(x) ≈ 2.527
            //  x5 = 4, y(x) ≈ 2.872
            //  x6 = 4.8, y(x) ≈ 3.295
            //  x7 = 5.6, y(x) ≈ 3.815
            //  x8 = 6.4, y(x) ≈ 4.452
            //  x9 = 7.2, y(x) ≈ 5.224
            //  x10 = 8, y(x) ≈ 6.149
            //case 2:
            //  x1 ≈ 1.714, y(x) ≈ 2.025
            //  x2 ≈ 0.429, y(x) ≈ 1.643
            //  x3 ≈ -0.857, y(x) ≈ 1.185
            //  x4 ≈ -2.143, y(x) ≈ 0.301
            //  x5 ≈ -3.429, y(x) does not exist
            //  x6 ≈ -4.714, y(x) does not exist
            //  x7 = -6, y(x) does not exist

            //got:
            //case 1:
            //  x1 = 0.8, y(x) = 1.7552281771620282                   +
            //  x2 = 1.6, y(x) = 1.990910594037296                    +
            //  x3 = 2.4, y(x) = 2.24062192401591                     +
            //  x4 = 3.2, y(x) = 2.527325967812444                    +
            //  x5 = 4, y(x) = 2.8720966110620423                     +
            //  x6 = 4.8, y(x) = 3.294945150026153                    +
            //  x7 = 5.6, y(x) = 3.8152331128256467                   +
            //  x8 = 6.4, y(x) = 4.451899248506717                    +
            //  x9 = 7.2, y(x) = 5.223593722301022                    +
            //  x10 = 8, y(x) = 6.148762056305024                     +
            //case 2:                                                 
            //  x1 = 1.7142857142857142, y(x) = 2.0251606942225626    +
            //  x2 = 0.4285714285714284, y(x) = 1.6433376673908302    +
            //  x3 = -0.8571428571428572, y(x) = 1.1852259208378784   +
            //  x4 = -2.1428571428571432, y(x) = 0.30069318972584125  +
            //  x5 = -3.428571428571429, y(x) does not exist          +
            //  x6 = -4.714285714285714, y(x) does not exist          +
            //  x7 = -6, y(x) does not exist                          +
        }
    }
}