using System;
using System.IO;

namespace Assignment1
{
    public class Task3
    {
        static string pathToFile = "../../../";

        //manually created unsorted list of words
        static string[] words = new string[]
        {
            "bite",
            "deal",
            "know",
            "dig",
            "give",
            "build",
            "awake",
            "eat",
            "catch",
            "forget",
            "have",
            "lead",
            "go",
            "find",
            "buy",
            "cost",
            "deal",
            "begin",
            "hold",
            "interweave",
            "want",
        };

        //manually sorted list of words
        static string[] sortedWords = new string[]
        {
            "awake",
            "begin",
            "bite",
            "build",
            "buy",
            "catch",
            "cost",
            "deal",
            "deal",
            "dig",
            "eat",
            "find",
            "forget",
            "give",
            "go",
            "have",
            "hold",
            "interweave",
            "know",
            "lead",
            "want",
        };

        static void Main()
        {
            Task3Test();
        }

        //write in file some text
        //from Task 1
        static void Write(string path, params string[] text)
        {
            using (StreamWriter sw = new(path, false))
            {
                foreach (string s in text)
                {
                    sw.WriteLine(s);
                }
            }
        }

        //read words, sort them and write to new file
        static string[] Sort(string pathToUnsorted, string pathToSorted)
        {
            string[] w = new string[40];

            using (StreamReader sr = new(pathToUnsorted))
            {
                int c = 0;
                for (int i = 0; i < w.Length; i++)
                {
                    if (sr.EndOfStream)
                    {
                        break;
                    }
                    c++;
                    w[i] = sr.ReadLine();
                }

                Console.WriteLine(c);
                w = Resize(w, c);

                string key;
                int j;
                for (int i = 1; i < w.Length; i++)
                {
                    key = w[i];
                    j = i - 1;
                    while (j >= 0 && Compare(w[j], key))
                    {
                        w[j + 1] = w[j];
                        j -= 1;
                    }
                    w[j + 1] = key;
                }
            }

            Write(pathToSorted, w);
            return w;
        }

        //create new array with size n and move elements from target array to it
        static string[] Resize(string[] target, int n)
        {
            string[] res = new string[n];

            for (int i = 0; i < n; i++)
            {
                res[i] = target[i];
            }

            return res;
        }

        //compare to words. Order: b a -> true, a b -> false
        static bool Compare(string a, string b)
        {
            int l = Math.Min(a.Length, b.Length);

            for (int i = 0; i < l; i++)
            {
                if (a[i] > b[i])
                {
                    return true;
                }
                else if (a[i] < b[i])
                {
                    return false;
                }
            }

            if (a.Length > l)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //compare two arrays
        static bool Equals(string[] a, string[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }

        static void Task3Test()
        {
            Write(pathToFile + "Task3Unsorted.txt", words);
            if (Equals(sortedWords, Sort(pathToFile + "Task3Unsorted.txt", pathToFile + "Task3Sorted.txt")))
            {
                Console.WriteLine("Sort result is equal to sortedWords");
            }
            else
            {
                Console.WriteLine("Sort result isn't equal to sortedWords");
            }
        }
    }
}

//input:
//words

//expected output:
//Sort result is equal to sortedWords