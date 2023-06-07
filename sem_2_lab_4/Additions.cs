using System;
using CustomCollections;

namespace Assignment
{
    public static class Additions
    {
        public static int NearestPrime(int number)
        {
            SLList<int> numbers1 = new();
            SLList<int> numbers2 = new();
            SLList<int> primes = new();

            for (int i = 2; i < number + 1; i++)
            {
                numbers1.Add(i);
            }

            while (numbers1.Count != 0)
            {
                int prime = numbers1[0];
                numbers1.RemoveAt(0);

                primes.Add(prime);

                while (numbers1.Count != 0)
                {
                    if (numbers1[0] % prime != 0)
                    {
                        numbers2.Add(numbers1[0]);
                    }
                    numbers1.RemoveAt(0);
                }

                numbers1 = numbers2;
                numbers2 = new();
            }

            return primes[primes.Count - 1];
        }

        public static int Mod(int x, int y)
        {
            int r = x % y;
            return r < 0 ? r + y : r;
        }
    }
}

