using System;
using System.IO;

namespace Assignment1
{
    public class Task4
    {
        static string pathToFile = "../../../";

        static string[] students = new string[]
        {
            "Stu1,Dent1,56",
            "Stu2,Dent2,28",
            "Stu3,Dent3,97",
            "Stu4,Dent4,35",
            "Stu5,Dent5,79",
            "Stu6,Dent6,46",
            "Stu7,Dent7,59",
            "Stu8,Dent8,85",
            "Stu9,Dent9,60",
        };

        static void Main()
        {
            Task4Test();
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

        //read students from file, split and check for score. Write all students with score < 60
        static void Read(string path)
        {
            using (StreamReader sr = new(path))
            {
                string[] line;
                bool writeNoOne = true;
                while (!sr.EndOfStream)
                {
                    line = Split(',', sr.ReadLine());
                    if (int.Parse(line[2]) < 60)
                    {
                        Console.WriteLine(Join(' ', line));
                        writeNoOne = false;
                    }
                }

                if (writeNoOne)
                {
                    Console.WriteLine("There are no students with a score < 60");
                }
            }
        }

        //split text with separator
        static string[] Split(char separator, string line)
        {
            int n = 1;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == separator)
                {
                    n++;
                }
            }

            string[] res = new string[n];
            string word = "";
            n = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == separator)
                {
                    res[n] = word;
                    word = "";
                    n++;
                    continue;
                }

                word += line[i];
            }
            res[n] = word;

            return res;
        }

        //create string from string array
        static string Join(char separator, string[] words)
        {
            string res = "";
            res += words[0];
            for (int i = 1; i < words.Length; i++)
            {
                res += " " + words[i];
            }

            return res;
        }

        static void Task4Test()
        {
            Write(pathToFile + "Task4.csv", students);

            Read(pathToFile + "Task4.csv");
        }
    }
}

//input:
//students

//expected output:
//Stu1 Dent1 56
//Stu2 Dent2 28
//Stu4 Dent4 35
//Stu6 Dent6 46
//Stu7 Dent7 59