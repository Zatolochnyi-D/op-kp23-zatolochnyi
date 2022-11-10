using System;

namespace Assignment1
{
    class Task3
    {
        static void Main(string[] args)
        {
            //test input:
            //case 1: n = 3, x = 2
            //case 2: n = 5, x = 4
            //case 3: n = 9, x = 3
            //case 4: n = 12, x = 13
            //case 5: n = 17, x = 0.5

            double x; //input
            int n; //input
            double power; //output
            int factorial; //output

            //define n, x
            do
            {
                Console.WriteLine("Enter n");
                n = Convert.ToInt32(Console.ReadLine());
            } while (n < 0);
            Console.WriteLine("Enter x");
            x = Convert.ToDouble(Console.ReadLine());

            //find n!
            factorial = 1;
            for (int i = 2; i < n + 1; i++)
            {
                factorial *= i;
            }


            //find x^n
            power = x;
            for (int i = 0; i < n - 1; i++)
            {
                power *= x;
            }

            Console.WriteLine("n! = " + factorial);
            Console.WriteLine("x^n = " + power);

            //test output:
            //case 1: factorial = 6, power = 8
            //case 2: factorial = 120, power = 1024
            //case 3: factorial = 362 880, power = 19683
            //case 4: factorial = 479 001 600, power = 23 298 085 122 481
            //case 5: factorial = 355 687 428 096 000, power ≈ 0.000007629

            //got:
            //case 1: n! = 6, x^n = 8 + 
            //case 2: n! = 120, x^n = 1024 + 
            //case 3: n! = 362 880, x^n = 19683 + 
            //case 4: n! = 479 001 600, x^n = 23 298 085 122 481 + 
            //case 5: n! = -288 522 240, x^n ≈ 0.000007629 -
        }
    }
}