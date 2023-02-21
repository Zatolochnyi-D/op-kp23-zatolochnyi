using System;
using System.IO;

namespace Assignment1
{
    public class Task2
    {
        static string pathToFile = "../../../";

        static void Main()
        {
            
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

        static double[] PseudoRandomNumbers(int n)
        {
            return new double[0];
        }

        static double MaxInFile(string path)
        {
            return 0;
        }
    }
}