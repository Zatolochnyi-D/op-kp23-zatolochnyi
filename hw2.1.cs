using System;

namespace OP
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;

            do
            {
                Console.WriteLine("Enter amount of steps in Pyramid");
                n = Convert.ToByte(Console.ReadLine());
            } while (n < 1 || n > 9);
            
            for (int i = 0; i < n; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.Write("                1\n");
                        break;
                    case 1:
                        Console.Write("              1   1\n");
                        break;
                    case 2:
                        Console.Write("            1   2   1\n");
                        break;
                    case 3:
                        Console.Write("          1   3   3   1\n");
                        break;
                    case 4:
                        Console.Write("        1   4   6   4   1\n");
                        break;
                    case 5:
                        Console.Write("      1   5  10   10  5   1\n");
                        break;
                    case 6:
                        Console.Write("    1   6  15   20  15   6  1\n");
                        break;
                    case 7:
                        Console.Write("  1   7  21   35  35  21   7  1\n");
                        break;
                    case 8:
                        Console.Write("1   8  28   56  70  56   28  8  1\n");
                        break;
                }
            }
        }
    }
} 