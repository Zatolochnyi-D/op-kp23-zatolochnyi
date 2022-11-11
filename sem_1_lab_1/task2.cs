using System;

namespace Assignment1
{
    class Task2
    {
        static void Main(string[] args)
        {
            //test input:
            //case 1: n = 7
            //case 2: n = 13
            //case 3: n = 26
            //case 4: n = 59
            //case 5: n = 153
            //case 6: n = 187
            //case 7: n = 19467

            int n; //input

            //define n
            do
            {
                Console.WriteLine("Enter number:");
                n = Convert.ToInt32(Console.ReadLine());
            } while (n < 1);

            // 1 is the prime number
            if (n == 1)
            {
                Console.WriteLine("Number is prime: True");
            }
            else
            {
                Console.WriteLine("Number is prime: " + isPrimeNumber(n));
            }

            //test output:
            //case 1: True
            //case 2: True
            //case 3: False
            //case 4: True
            //case 5: False
            //case 6: False
            //case 7: False

            //got:
            //case 1: True +
            //case 2: True +
            //case 3: False +
            //case 4: True +
            //case 5: False + 
            //case 6: False + 
            //case 7: False +
        }

        static bool isPrimeNumber(int n)
        {
            for (int i = 2; i < n; i++)
            {
                //if number is divisible by i, it can't be prime.
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}