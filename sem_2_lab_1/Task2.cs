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

        //create 1D array of n double numbers, generated with Random
        static double[] PseudoRandomNumbers(int n)
        {
            return new double[0];
        }

        static double MaxInFile(string path)
        {
            return 0;
        }

        static void Task2Test()
        {

        }
    }
}

//input:
//1.3 4.5 9.1 1.2 4.3 8.6 3.6 0.2 4.5 7.6 1.2 4.9 6.7 0.4 3.3
//PseudoRandomNumbers(15)

//expected output:
//0.2
//*some double*