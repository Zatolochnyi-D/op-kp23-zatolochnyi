using System;
using System.IO;

namespace Assignment1
{
    public class Task1
    {
        static string pathToFile = "../../../";

        static void Main()
        {
            Task1Test();
        }
        
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
        
        static void Read(string path)
        {
            using (StreamReader sr = new(path))
            {
                while (!sr.EndOfStream)
                {
                    Console.WriteLine(sr.ReadLine());
                }
            }
        }

        static void Task1Test()
        {
            Write(pathToFile + "Task1.txt", "Hello", "world");
            Read(pathToFile + "Task1.txt");
        }
    }
}

//input:
//Write(pathToFile + "Task1.txt", "Hello", "world")
//Read(pathToFile + "Task1.txt")

//expected output:
//Hello
//world