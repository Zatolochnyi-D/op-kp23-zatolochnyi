using System;
using System.IO;

namespace Assignment1
{
    public class Task2
    {
        static string pathToFile = "../../../";

        static void Main()
        {
            Task2Test();
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

        //copy of Write, but without rewriting the existing file
        static void WriteOrAppend(string path, params string[] text)
        {
            using (StreamWriter sw = new(path, true))
            {
                foreach (string s in text)
                {
                    sw.WriteLine(s);
                }
            }
        }

        //create 1D array of n double numbers, generated with Random
        static double[] PseudoRandomNumbers(int n)
        {
            Random rnd = new();
            double[] res = new double[n];

            for (int i = 0; i < n; i++)
            {
                res[i] = rnd.NextDouble();
            }

            return res;
        }

        //find max value in file's first line
        static double MaxInFile(string path)
        {
            using (StreamReader sr = new(path))
            {
                string num = "";
                int ch = 0;
                double max = -1.0, next;

                while (ch != -1)
                {
                    while (true)
                    {
                        ch = sr.Read();
                        if (ch == ' ' || ch == -1)
                        {
                            break;
                        }
                        num += (char)ch;
                    }

                    Console.WriteLine(num);
                    next = double.Parse(num);
                    if (next > max)
                    {
                        max = next;
                    }
                    num = "";
                }

                Console.WriteLine($"\nMax value: {max}\n");
                return max;
            }
        }

        static void Task2Test()
        {
            File.Create(pathToFile + "max.txt").Close(); //create new clear max.txt

            Write(pathToFile + "Task2.txt", "1.3 4.5 9.1 1.2 4.3 8.6 3.6 0.2 4.5 7.6 1.2 4.9 6.7 0.4 3.3");
            WriteOrAppend(pathToFile + "max.txt", MaxInFile(pathToFile + "Task2.txt").ToString());

            Write(pathToFile + "Task2.txt", string.Join(" ", PseudoRandomNumbers(15)));
            WriteOrAppend(pathToFile + "max.txt", MaxInFile(pathToFile + "Task2.txt").ToString());
        }
    }
}

//input:
//1.3 4.5 9.1 1.2 4.3 8.6 3.6 0.2 4.5 7.6 1.2 4.9 6.7 0.4 3.3
//PseudoRandomNumbers(15)

//expected output:
//8.6
//*some double*