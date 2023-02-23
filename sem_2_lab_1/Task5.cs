using System;
using System.IO;

namespace Assignment1
{
    public class Task5
    {
        static string pathToFile = "../../../";

        static string[] text = new string[]
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed in est at tellus fermentum congue sed vitae odio.",
            "Pellentesque eget urna at lectus viverra volutpat. Duis eu molestie metus.",
            "Mauris ac enim ut nisi maximus vulputate et ut dolor. Quisque ac feugiat leo, sed accumsan enim.",
            "Integer accumsan nisl eget.",
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