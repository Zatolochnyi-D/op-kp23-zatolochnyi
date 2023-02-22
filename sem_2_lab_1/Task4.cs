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

//input:
//students

//expected output:
//First name: Stu1, Last name: Dent1, Score: 56
//First name: Stu2, Last name: Dent2, Score: 28
//First name: Stu4, Last name: Dent4, Score: 35
//First name: Stu6, Last name: Dent6, Score: 46
//First name: Stu7, Last name: Dent7, Score: 59