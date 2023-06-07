using System;
using System.Collections;
using CustomCollections;

namespace Assignment
{
    class Program
    {
        static void Main()
        {
            IrregularVerbsDictionary();
        }

        static void HTTestNoCollisions()
        {
            Console.WriteLine("Testing HashTable<int, string> of size 20 without collisions.");

            HashTable<int, string> ht = new(20);

            Console.WriteLine("Adding elements: 2 = a, 4 = b, 8 = c, 12 = d, 17 = e");

            ht[2] = "a";
            ht[4] = "b";
            ht[8] = "c";
            ht[12] = "d";
            ht[17] = "e";

            Console.WriteLine($"Print these elements: {ht[2]}, {ht[4]}, {ht[8]}, {ht[12]}, {ht[17]}");

            Console.WriteLine("Add new elements: 3 = f, 6 = g");
            ht.Add(3, "f");
            ht.Add(6, "g");

            Console.WriteLine("Replace a with h at 2");
            ht[2] = "h";

            Console.WriteLine($"Get elements at 3, 4: {ht.Get(3)}, {ht.Get(4)}");

            Console.WriteLine("Remove 12");
            ht.Remove(12);

            Console.WriteLine($"Contains 3, 12? {ht.ContainsKey(3)}, {ht.ContainsKey(12)}");

            Console.WriteLine($"Contains a, b? {ht.ContainsValue("a")}, {ht.ContainsValue("b")}");

            Console.WriteLine("Resize to 40 and print all elements:");
            ht.Resize(40);
            foreach (int key in ht.Keys)
            {
                Console.Write(ht[key] + "  ");
            }
            Console.WriteLine();
        }

        static void HTTestCollisions()
        {
            Console.WriteLine("Testing HashTable<int, string> of size 10 without collisions.");

            HashTable<int, string> ht = new(10);

            Console.WriteLine("Adding elements: 2 = a, 4 = b, 8 = c, 12 = d, 17 = e");

            ht[2] = "a";
            ht[4] = "b";
            ht[8] = "c";
            ht[12] = "d";
            ht[17] = "e";

            Console.WriteLine($"Print these elements: {ht[2]}, {ht[4]}, {ht[8]}, {ht[12]}, {ht[17]}");

            Console.WriteLine("Add new elements: 3 = f, 6 = g");
            ht.Add(3, "f");
            ht.Add(6, "g");

            Console.WriteLine("Replace a with h at 2");
            ht[2] = "h";

            Console.WriteLine($"Get elements at 3, 4: {ht.Get(3)}, {ht.Get(4)}");

            Console.WriteLine("Remove 12");
            ht.Remove(12);

            Console.WriteLine($"Contains 3, 12? {ht.ContainsKey(3)}, {ht.ContainsKey(12)}");

            Console.WriteLine($"Contains a, b? {ht.ContainsValue("a")}, {ht.ContainsValue("b")}");

            Console.WriteLine("Resize to 40 and print all elements:");
            ht.Resize(40);
            foreach (int key in ht.Keys)
            {
                Console.Write(ht[key] + "  ");
            }
            Console.WriteLine();
        }

        static void IrregularVerbsDictionary()
        {
            HashTable<string, string> ht = new();

            SLList<string> words = new()
            {
                "awake",
                "be",
                "beat",
                "become",
                "begin",
                "bend",
                "bet",
                "bid",
                "bite",
                "blow",
                "break",
                "bring",
                "broadcast",
                "build",
                "buy",
                "catch",
                "choose",
                "come",
                "cost",
                "cut",
                "dig",
                "do",
                "draw",
                "drive",
                "drink",
                "eat",
                "fall",
                "feel",
                "fight",
                "find",
                "fly",
                "forget",
                "forgive",
                "get",
                "give",
                "go",
                "grow",
                "hang",
                "have",
                "hear",
                "hit",
                "hold",
                "keep",
                "know",
                "lay",
                "lead",
                "leave",
                "lend",
                "let",
                "lie",
                "lose",
                "make",
                "mean",
                "meet",
                "pay",
                "put",
                "read",
                "ride",
                "ring",
                "rise",
                "run",
                "say",
                "see",
                "sell",
                "send",
                "sing",
                "sit",
                "sleep",
                "speak",
                "spend",
                "stand",
                "swim",
                "take",
                "teach",
                "tear",
                "tell",
                "think",
                "throw",
                "understand",
                "wake",
                "win",
                "write",
            };

            SLList<string> forms = new()
            {
                "awoke, awoken",
                "was (were), been",
                "beat, beaten",
                "became, become",
                "began, begun",
                "bent, bent",
                "bet, bet",
                "bid, bid",
                "bit, bitten",
                "blew, blown",
                "broke, broken",
                "brought, brought",
                "broadcast, broadcast",
                "built, built",
                "bought, bought",
                "caught, caught",
                "chose, chosen",
                "came, come",
                "cost, cost",
                "cut, cut",
                "dug, dug",
                "did, done",
                "drew, drawn",
                "drove, driven",
                "drank, drunk",
                "ate, eaten",
                "fell, fallen",
                "felt, felt",
                "fought, fought",
                "found, found",
                "flew, flown",
                "forgot, forgotten",
                "forgave, forgiven",
                "got, got(gotten)",
                "gave, given",
                "went, gone",
                "grew, grown",
                "hung, hung",
                "had, had",
                "heard, heard",
                "hit, hit",
                "held, held",
                "kept, kept",
                "knew, known",
                "laid, laid",
                "led, led",
                "left, left",
                "lent, lent",
                "let, let",
                "lay, lain",
                "lost, lost",
                "made, made",
                "meant, meant",
                "met, met",
                "paid, paid",
                "put, put",
                "read, read",
                "rode, ridden",
                "rang, rung",
                "rose, risen",
                "ran, run",
                "said, said",
                "saw, seen",
                "sold, sold",
                "sent, sent",
                "sang, sung",
                "sat, sat",
                "slept, slept",
                "spoke, spoken",
                "spent, spent",
                "stood, stood",
                "swam, swum",
                "took, taken",
                "taught, taught",
                "tore, torn",
                "told, told",
                "thought, thought",
                "threw, thrown",
                "understood, understood",
                "woke, woken",
                "won, won",
                "wrote, written",
            };

            foreach (var wordsAndForms in words.Zip(forms))
            {
                ht[wordsAndForms.First] = wordsAndForms.Second;
            }

            while (true)
            {
                Console.WriteLine("Enter verd and get it forms:");
                string? input = Console.ReadLine();
                if (input == "stop")
                {
                    break;
                }
                else
                {
                    if (ht.ContainsKey(input))
                    {
                        Console.WriteLine($"{input}, {ht[input]}\n");
                    }
                    else
                    {
                        Console.WriteLine("No data\n");
                    }
                }
            }
        }
    }
}