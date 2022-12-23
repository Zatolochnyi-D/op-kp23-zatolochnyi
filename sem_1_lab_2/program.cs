using System;

namespace Assignment2
{
    class Perlocate
    {
        //size of matrix side
        static int n;
        //amount of opened sites
        static int opened = 0;
        //matrix to demonstrate results
        static int[,] matrix;
        //adjacency list of graph
        static Dictionary<(int, int), List<(int, int)>> adjacency = new Dictionary<(int, int), List<(int, int)>>();

        static void Main()
        {
            Console.WriteLine("Enter following numbers to continue:");
            Console.WriteLine("1 - start test scenario");
            Console.WriteLine("2 - create new scheme");
            Console.WriteLine("another key to exit");
            switch (Convert.ToChar(Console.ReadLine()))
            {
                case '1':
                    Test();
                    break;
                case '2':
                    Console.WriteLine("");
                    Console.WriteLine("Enter size:");
                    n = Convert.ToInt32(Console.ReadLine());
                    Init();
                    PrintMatrix();
                    Console.WriteLine("");
                    Console.WriteLine("Enter coordinates of sites (splited by 1 space) or press Enter to continue:");
                    string[] tmp;
                    while (true)
                    {
                        tmp = Console.ReadLine().Split(" ");
                        if (tmp[0] == "")
                        {
                            break;
                        }
                        Open(int.Parse(tmp[0]), int.Parse(tmp[1]));
                    }
                    Console.WriteLine("");
                    Console.WriteLine($"Number of opened sites: {NumberOfOpenSites()}");
                    Console.WriteLine("");

                    Graph();
                    if (Percolating(BFS((-1, -1), (-2, -2))))
                    {
                        Console.WriteLine("Scheme is percolating");
                    }
                    else
                    {
                        Console.WriteLine("Scheme is not percolating");
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Enter following numbers to continue:");
                    while (true)
                    {
                        Console.WriteLine("1 - print matrix");
                        Console.WriteLine("2 - look for opened sites");
                        Console.WriteLine("3 - look for full sites");
                        Console.WriteLine("another key to exit");
                        switch (Convert.ToChar(Console.ReadLine()))
                        {
                            case '1':
                                PrintMatrix();
                                break;
                            case '2':
                                Console.WriteLine("");
                                Console.WriteLine("Enter coordinates of sites (splited by 1 space) or press Enter to continue:");
                                while (true)
                                {
                                    tmp = Console.ReadLine().Split(" ");
                                    if (tmp.Length == 0)
                                    {
                                        break;
                                    }
                                    Console.WriteLine(IsOpen(int.Parse(tmp[0]), int.Parse(tmp[1])));
                                }
                                break;
                            case '3':
                                Console.WriteLine("");
                                Console.WriteLine("Enter coordinates of sites (splited by 1 space) or press Enter to continue:");
                                while (true)
                                {
                                    tmp = Console.ReadLine().Split(" ");
                                    if (tmp.Length == 0)
                                    {
                                        break;
                                    }
                                    Console.WriteLine(IsFull(int.Parse(tmp[0]), int.Parse(tmp[1])));
                                }
                                break;
                            default:
                                Environment.Exit(0);
                                break;
                        }
                    }
                default:
                    break;
            }
        }

        //create 2D matrix nxn with blocked sites and init adjacency list with 2 imaginary points
        static void Init()
        {
            matrix = new int[n, n];

            adjacency[(-1, -1)] = new List<(int, int)>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    adjacency[(i, j)] = new List<(int, int)>();
                }
            }

            adjacency[(-2, -2)] = new List<(int, int)>();
        }

        //open choosed site
        static void Open(int x, int y)
        {
            if ((x > 0 && y < n + 1) && (y > 0 && y < n + 1))
            {
                matrix[n - y, x - 1] = 1;
                opened += 1;
            }
            else
            {
                Console.WriteLine("This site does not exist.");
            }
        }

        //return true if choosed site is opened
        static bool IsOpen(int x, int y)
        {
            if ((x > 0 && x < n + 1) && (y > 0 && y < n + 1))
            {
                switch (matrix[n - y, x - 1])
                {
                    case 1:
                        return true;
                    case 2:
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        //return true if choosed site is full
        static bool IsFull(int x, int y)
        {
            if ((x > 0 && x < n + 1) && (y > 0 && y < n + 1))
            {
                switch (matrix[n - y, x - 1])
                {
                    case 2:
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        //return number of opened sites
        static int NumberOfOpenSites()
        {
            return opened;
        }

        //union 2 points into one graph
        static void Union((int, int) x, (int, int) y)
        {
            if (adjacency[x].Contains(y))
            {
                return;
            }
            adjacency[x].Add(y);
            adjacency[y].Add(x);
        }

        //connect points into the graph
        static void Graph()
        {
            //connect imaginary input to top of the matrix
            for (int i = 0; i < n; i++)
            {
                if (matrix[0, i] == 1)
                {
                    Union((-1, -1), (0, i));
                }
            }

            //connect points in the matrix
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        if (i - 1 != -1)
                        {
                            if (matrix[i - 1, j] == 1)
                            {
                                Union((i, j), (i - 1, j));
                            }
                        }
                        if (i + 1 != n)
                        {
                            if (matrix[i + 1, j] == 1)
                            {
                                Union((i, j), (i + 1, j));
                            }
                        }
                        if (j - 1 != -1)
                        {
                            if (matrix[i, j - 1] == 1)
                            {
                                Union((i, j), (i, j - 1));
                            }
                        }
                        if (j + 1 != n)
                        {
                            if (matrix[i, j + 1] == 1)
                            {
                                Union((i, j), (i, j + 1));
                            }
                        }
                    }
                }
            }

            //connect imaginary output to top of the matrix
            for (int i = 0; i < n; i++)
            {
                if (matrix[n - 1, i] == 1)
                {
                    Union((-2, -2), (n - 1, i));
                }
            }
        }

        static Dictionary<(int, int), (int, int)> BFS((int, int) start, (int, int) goal)
        {
            var stack = new List<(int, int)>() { start };
            var visited = new Dictionary<(int, int), (int, int)>();
            visited[start] = start;
            (int, int) curNode;
            var nextNodes = new List<(int, int)>();

            while (stack.Count != 0)
            {
                curNode = stack[0];
                stack.RemoveAt(0);
                if (curNode == goal)
                {
                    break;
                }

                nextNodes = adjacency[curNode];
                foreach (var nextNode in nextNodes)
                {
                    if (!visited.ContainsKey(nextNode))
                    {
                        stack.Add(nextNode);
                        visited[nextNode] = curNode;
                    }
                }
            }

            return visited;
        }

        static bool Percolating(Dictionary<(int, int), (int, int)> dict)
        {
            if (dict.ContainsKey((-2, -2)))
            {
                (int, int) coords = dict[(-2, -2)];
                while (coords != (-1, -1))
                {
                    matrix[coords.Item1, coords.Item2] = 2;
                    coords = dict[coords];
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //print matrix
        static void PrintMatrix()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine("");
            }
        }

        static void PrintList()
        {
            foreach (var e in adjacency)
            {
                Console.Write($"({e.Key.Item1}, {e.Key.Item2}): ");
                foreach (var coords in e.Value)
                {
                    Console.Write($"({coords.Item1}, {coords.Item2}) ");
                }
                Console.WriteLine("");
            }
        }

        //test of all functions
        static void Test()
        {
            Console.WriteLine("Matrix side size: n = 7. Expected: ");
            Console.WriteLine("0 0 0 0 0 0 0\n0 0 0 0 0 0 0\n0 0 0 0 0 0 0\n0 0 0 0 0 0 0\n0 0 0 0 0 0 0\n0 0 0 0 0 0 0\n0 0 0 0 0 0 0");
            Console.WriteLine("");
            Console.WriteLine("Got:");

            n = 7;
            Init();
            PrintMatrix();

            Console.WriteLine("");
            Console.WriteLine("Open sites (2,7), (2, 6), (3,6), (4, 6), (4, 5), (4, 4), (4, 3), (4, 2), (5,2), (3, 3), (2, 3), (2, 2), (2, 1), (7, 7), (7, 6), (6, 6), (6, 5), (6, 4), (6, 3), (7, 4), (7, 3). Expected:");
            Console.WriteLine("0 1 0 0 0 0 1\n0 1 1 1 0 1 1\n0 0 0 1 0 1 0\n0 0 0 1 0 1 1\n0 1 1 1 0 1 1\n0 1 0 1 1 0 0\n0 1 0 0 0 0 0");
            Console.WriteLine("");
            Console.WriteLine("Got:");

            Open(2, 7);
            Open(2, 6);
            Open(3, 6);
            Open(4, 6);
            Open(4, 5);
            Open(4, 4);
            Open(4, 3);
            Open(4, 2);
            Open(5, 2);
            Open(3, 3);
            Open(2, 3);
            Open(2, 2);
            Open(2, 1);
            Open(7, 7);
            Open(7, 6);
            Open(6, 6);
            Open(6, 5);
            Open(6, 4);
            Open(6, 3);
            Open(7, 4);
            Open(7, 3);
            PrintMatrix();

            Console.WriteLine("");
            Console.WriteLine("Are sites (2,1), (2, 2), (5,7), (4, 4), (6, 2) opened. Expected:");
            Console.WriteLine("true\ntrue\nfalse\ntrue\nfalse");
            Console.WriteLine("");
            Console.WriteLine("Got:");

            Console.WriteLine(IsOpen(2, 1));
            Console.WriteLine(IsOpen(2, 2));
            Console.WriteLine(IsOpen(5, 7));
            Console.WriteLine(IsOpen(4, 4));
            Console.WriteLine(IsOpen(6, 2));

            Console.WriteLine("");
            Console.WriteLine("Amount of opened sites. Expected:");
            Console.WriteLine("Opened sites: 21");
            Console.WriteLine("");
            Console.WriteLine("Got:");

            Console.WriteLine($"Opened sites: {NumberOfOpenSites()}");

            Console.WriteLine("");
            Console.WriteLine("Adjacency list. Expected:");
            Console.WriteLine("(-1, -1): (0, 1) (0, 6) \n(0, 0): \n(0, 1): (-1, -1) (1, 1) \n(0, 2): \n(0, 3): \n(0, 4): \n(0, 5): \n(0, 6): (-1, -1) (1, 6) \n(1, 0): \n(1, 1): (0, 1) (1, 2) \n(1, 2): (1, 1) (1, 3) \n(1, 3): (1, 2) (2, 3) \n(1, 4): \n(1, 5): (2, 5) (1, 6) \n(1, 6): (0, 6) (1, 5) \n(2, 0): \n(2, 1): \n(2, 2): \n(2, 3): (1, 3) (3, 3) \n(2, 4): \n(2, 5): (1, 5) (3, 5) \n(2, 6): \n(3, 0): \n(3, 1): \n(3, 2): \n(3, 3): (2, 3) (4, 3) \n(3, 4): \n(3, 5): (2, 5) (4, 5) (3, 6) \n(3, 6): (3, 5) (4, 6) \n(4, 0): \n(4, 1): (5, 1) (4, 2) \n(4, 2): (4, 1) (4, 3) \n(4, 3): (3, 3) (4, 2) (5, 3) \n(4, 4): \n(4, 5): (3, 5) (4, 6) \n(4, 6): (3, 6) (4, 5) \n(5, 0): \n(5, 1): (4, 1) (6, 1) \n(5, 2): \n(5, 3): (4, 3) (5, 4) \n(5, 4): (5, 3) \n(5, 5): \n(5, 6): \n(6, 0): \n(6, 1): (5, 1) (-2, -2) \n(6, 2): \n(6, 3): \n(6, 4): \n(6, 5): \n(6, 6): \n(-2, -2): (6, 1)");
            Console.WriteLine("");
            Console.WriteLine("Got:");

            Graph();
            PrintList();

            Console.WriteLine("");
            Console.WriteLine("Is system percolating. Expected:");
            Console.WriteLine("true");
            Console.WriteLine("0 2 0 0 0 0 1\n0 2 2 2 0 1 1\n0 0 0 2 0 1 0\n0 0 0 2 0 1 1\n0 2 2 2 0 1 1\n0 2 0 1 1 0 0\n0 2 0 0 0 0 0");
            Console.WriteLine("");
            Console.WriteLine("Got:");

            Console.WriteLine(Percolating(BFS((-1, -1), (-2, -2))));
            PrintMatrix();

            Console.WriteLine("");
            Console.WriteLine("Is sites full. Expected:");
            Console.WriteLine("true\nfalse");
            Console.WriteLine("");
            Console.WriteLine("Got:");

            Console.WriteLine(IsFull(2, 2));
            Console.WriteLine(IsFull(3, 7));
        }
    }
}