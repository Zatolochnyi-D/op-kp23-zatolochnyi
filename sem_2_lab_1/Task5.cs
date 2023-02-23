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
            Task5Test();
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

        //count number of each word in text, located in file
        static void CountWords(string path)
        {
            using (StreamReader sr = new(path))
            {
                string word = "";
                string whiteList = "abcdefghijklmnopqrstuvwxyz-"; //characters which can be in word
                char next;

                //dictionary
                string[] keys = new string[10];
                int[] values = new int[10];
                int count = 0;

                //flag that word not in dictionary
                bool added;

                while (!sr.EndOfStream)
                {
                    //construct word from allowed characters
                    next = char.ToLower((char)sr.Read());
                    if (!Contains(whiteList, next))
                    {
                        continue;
                    }
                    while (Contains(whiteList, next))
                    {
                        word += next;
                        next = (char)sr.Read();
                    }

                    //increase counter if word in dictionary or add to it if not
                    added = false;
                    for (int i = 0; i < count; i++)
                    {
                        if (keys[i] == word)
                        {
                            values[i]++;
                            added = true;
                        }
                    }
                    if (!added)
                    {
                        keys[count] = word;
                        values[count] = 1;
                        count++;
                    }

                    //increase size of dictionary arrays if it is not enough
                    if (count == keys.Length)
                    {
                        Array.Resize(ref keys, keys.Length * 2);
                        Array.Resize(ref values, values.Length * 2);
                    }

                    //reset word
                    word = "";
                }

                //after reading print result
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"{keys[i]}: {values[i]}");
                }
            }
        }

        //true if word has this letter
        static bool Contains(string word, char letter)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letter)
                {
                    return true;
                }
            }
            return false;
        }

        static void Task5Test()
        {
            Write(pathToFile + "Task5.txt", text);
            CountWords(pathToFile + "Task5.txt");
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

//result matched