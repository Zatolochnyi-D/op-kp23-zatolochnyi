using System;
using System.IO;

namespace Assignment1
{
    public class Task2
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

        static void Sort()
        {

        }
    }
}