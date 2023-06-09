using System;
using CustomCollections;

namespace GameMechanics
{
    static public class FileManipulator
    {
        public static string[] ReadStringList(string path)
        {
            SLList<string> strings = new();

            using (StreamReader sr = new(path))
            {
                while (!sr.EndOfStream)
                {
                    strings.Add(sr.ReadLine());
                }
            }

            return strings.ToArray();
        }
    }
}