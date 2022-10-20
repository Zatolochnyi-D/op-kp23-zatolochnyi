using System;

namespace Practice2
{
    class Calc
    {
        static void Main(string[] args)
        {
            double a, b, res;
            string operation;

            while (true)
            {
                Console.WriteLine("Enter a");
                a = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter b");
                b = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Choose operation (+, -, *, /) or 'exit' to leave");
                operation = Convert.ToString(Console.ReadLine());

                if (operation == "exit")
                {
                    break;
                }
                else
                {
                    switch (operation)
                    {
                        case "+":
                            res = a + b;
                            Console.WriteLine("Result is " + res);
                            break;
                        case "-":
                            res = a - b;
                            Console.WriteLine("Result is " + res);
                            break;
                        case "*":
                            res = a * b;
                            Console.WriteLine("Result is " + res);
                            break;
                        case "/":
                            res = a / b;
                            Console.WriteLine("Result is " + res);
                            break;
                        case "^":
                            res = Math.Pow(a, b);
                            Console.WriteLine("Result is " + res);
                            break;
                        default:
                            Console.WriteLine("Incorrect operation");
                            break;
                    }
                }
            }
        }
    }
}