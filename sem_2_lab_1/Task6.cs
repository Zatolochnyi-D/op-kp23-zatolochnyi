using System;
using System.IO;

namespace Assignment1
{
    public class Task6
    {
        static string pathToFile = "../../../";

        static void Main()
        {
            Task6Test();
        }

        static void ToBinary(string fromPath, string toPath)
        {
            using (StreamReader sr = new(fromPath))
            using (BinaryWriter bw = new(File.Open(toPath, FileMode.Create)))
            {
                string[] line;

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split(",");

                    foreach (string word in line)
                    {
                        bw.Write(word);
                    }
                }
            }
        }

        static void RewriteBinary(string fromPath, string toPath)
        {
            using(BinaryReader br = new(File.Open(fromPath, FileMode.Open)))
            using(BinaryWriter wr = new(File.Open(toPath, FileMode.Create)))
            {
                string firstName, lastName, score;

                while (true)
                {
                    try
                    {
                        firstName = br.ReadString();
                        lastName = br.ReadString();
                        score = br.ReadString();

                        if (int.Parse(score) >= 95)
                        {
                            Console.WriteLine($"{firstName} {lastName} {score}");
                        }
                    }
                    catch (IOException)
                    {
                        break;
                    }
                }
            }
        }

        static void ReadBinary(string path)
        {
            using(BinaryReader br = new(File.Open(path, FileMode.OpenOrCreate)))
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine(br.ReadString());
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("End of File");
                        break;
                    }
                }
            }
        }

        static void Task6Test()
        {
            ToBinary(pathToFile + "Task4.csv", pathToFile + "Task6.dat");
            RewriteBinary(pathToFile + "Task6.dat", pathToFile + "Task6Best.dat");
            Console.WriteLine("\n\nThe content of Task6.dat:\n");
            ReadBinary(pathToFile + "Task6.dat");
        }
    }
}

//input:
//Task4.csv

//expected output:
//Stu3 Dent3 97