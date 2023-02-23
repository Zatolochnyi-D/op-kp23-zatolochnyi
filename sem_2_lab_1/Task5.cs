using System;
using System.IO;

namespace Assignment1
{
    public class Task5
    {
        static string pathToFile = "../../../";

        static string[] text = new string[]
        {
            "Unit tests are not required, but the test cases should be identified.",
            "The git history should contain at least 3 commits for each task",
            "(prototype of the system, several test cases and implementation of the system).",
            "Usually it contains more that just 3 commits (several commits for bug-fixing)",
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
        static void CountWords(string path)
        {

        }

        static void Contains(string word, char letter)
        {

        }
    }
}

//input:
//text

//expected output:
//unit: 1
//tests: 1
//are: 1
//not: 1
//required: 1
//but: 1
//the: 4
//test: 2
//cases: 2
//should: 2
//be: 1
//indentified: 1
//git: 1
//history: 1
//contain: 1
//at: 1
//least: 1
//commits: 3
//for: 2
//each: 1
//task: 1
//prototype: 1
//of: 2
//system: 2
//several: 2
//and: 1
//implementation: 1
//usually: 1
//it: 1
//contains: 1
//more: 1
//that: 1
//just: 1
//bug-fixing: 1