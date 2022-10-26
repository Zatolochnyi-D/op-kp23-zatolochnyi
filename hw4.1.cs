using System;

namespace Homework
{
    class Root
    {
        static void Main(string[] args)
        {
            double a = 0, root_a = 0, calc_a = 0; //number to calc
            double l = 0, r = 0; //borders of segment system
            double delta = 1; //measurement delta
            int root = 0; //degree of the root
            int dp = 0; //number of decimal places

            //define degree of the root
            do
            {
                Console.WriteLine("Enter degree of the root");
                root = Convert.ToInt32(Console.ReadLine());
            } while (root < 0);

            //define number of decimal places
            do
            {
                Console.WriteLine("Enter number of decimal places: ");
                dp = Convert.ToInt32(Console.ReadLine());
            } while (dp < 0);

            //define delta using decimal places
            for (int i = 0; i < dp; i++)
            {
                delta /= 10;
            }

            //define number a. If root is even, a must be greater than 0
            if (root % 2 == 0)
            {
                do
                {
                    Console.WriteLine("Enter number: ");
                    a = Convert.ToDouble(Console.ReadLine());
                } while (a < 0);
            }
            else
            {
                Console.WriteLine("Enter number: ");
                a = Convert.ToDouble(Console.ReadLine());
            }

            //find root of number a
            if (a > 1) //number a is in the segment from 1 to ∞
            {
                //define borders of segment
                l = 1;
                r = a;

                //find root
                do
                {
                    root_a = ((l + r) / 2); //take the center of the segment

                    //raise to the root power
                    calc_a = root_a;
                    for (int i = 1; i < root; i++)
                    {
                        calc_a *= root_a;
                    }

                    //move borders
                    if (calc_a > a)
                    {
                        r = root_a; //move righ, if calculated value is greater than given
                    }
                    else
                    {
                        l = root_a; //move left, if not
                    }
                } while (Math.Abs(a - calc_a) > delta); //stop when calculated value will be close to given
            }
            else if (a < -1) //number a is in the segment from -∞ to -1
            {
                l = a;
                r = -1;
                do
                {
                    root_a = ((l + r) / 2);
                    calc_a = root_a;
                    for (int i = 1; i < root; i++)
                    {
                        calc_a *= root_a;
                    }
                    if (calc_a < a)
                    {
                        l = root_a;
                    }
                    else
                    {
                        r = root_a;
                    }
                } while (Math.Abs(a - calc_a) > delta);
            }
            else if (0 < a && a < 1) //number a is in the segment from 0 to 1
            {
                l = a;
                r = 1;
                do
                {
                    root_a = ((l + r) / 2);
                    calc_a = root_a;
                    for (int i = 1; i < root; i++)
                    {
                        calc_a *= root_a;
                    }
                    if (calc_a > a)
                    {
                        r = root_a;
                    }
                    else
                    {
                        l = root_a;
                    }
                } while (Math.Abs(a - calc_a) > delta);
            }
            else if (-1 < a && a < 0) //number a is in the segment from -1 to 0
            {
                l = -1;
                r = a;
                do
                {
                    root_a = ((l + r) / 2);
                    calc_a = root_a;
                    for (int i = 1; i < root; i++)
                    {
                        calc_a *= root_a;
                    }
                    if (calc_a < a)
                    {
                        l = root_a;
                    }
                    else
                    {
                        r = root_a;
                    }
                } while (Math.Abs(a - calc_a) > delta);
            }
            else //number a is -1, 0 or 1
            {
                switch (a)
                {
                    case 1:
                        root_a = 1;
                        break;
                    case 0:
                        root_a = 0;
                        break;
                    case -1:
                        root_a = -1;
                        break;
                }
            }

            //show the result
            Console.WriteLine(String.Format("{0:F" + dp + "}", root_a));
        }
    }
}