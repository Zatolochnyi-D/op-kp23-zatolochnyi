using System;
using System.IO;

namespace Assignment1
{
    public class Task4
    {
        static string pathToFile = "../../../";

        string[] students = new string[]
        {
            "Stu1,Dent1,56",
            "Stu2,Dent2,28",
            "Stu3,Dent3,97",
            "Stu4,Dent4,35",
            "Stu5,Dent5,79",
            "Stu6,Dent6,46",
            "Stu7,Dent7,59",
            "Stu8,Dent8,85",
        };

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

        static void Read()
        {

        }

        static string[] Split()
        {
            return new string[0];
        }
    }
}