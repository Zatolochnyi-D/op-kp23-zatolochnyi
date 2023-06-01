using System;
using CustomCollections;

namespace GameMechanics
{
    class Tester
    {
        static void Main()
        {
            //SLList<Client> clients = new();
            //for (int i = 0; i < 10; i++)
            //{
            //    clients.Add(new());
            //}

            //foreach (Client c in clients)
            //{
            //    Console.WriteLine(c.Name);
            //}

            //SharesTradingTest(21.2);

            RequirementTest();

            CityTest();
        }

        static void CityTest()
        {
            City city = new();

            Console.WriteLine($"City tax percent: {city.Taxation}");
            Console.WriteLine($"City lands:");
            foreach (Land land in city.Lands)
            {
                Console.WriteLine($"    Land taxation: {land.Taxation}");
                Console.WriteLine($"    Land building:");
                Console.WriteLine($"    {land.Building?.GetType()}");
                Console.WriteLine();
            }

        }

        static void RequirementTest()
        {
            Requirement req = new();

            Building a = req.GetBuilding();

            Console.WriteLine("Requirement test.");
            Console.WriteLine(a.GetType());
            Console.WriteLine("End of Requirement test\n");
        }

        static void PlayerStats(Player player)
        {
            Console.WriteLine("Player stats:");
            Console.WriteLine($"Player shares: {player.PlayerShares.Percent}");
            Console.WriteLine($"Shares on exchange: {player.SharesOnExchange.Percent}");
            Console.WriteLine($"Player reputation: {player.Reputation}");
            Console.WriteLine($"Player money: {player.Money}");
            Console.WriteLine($"Shares price: {player.SharePrice}");
            Console.WriteLine($"Investor shares: {player.InvestorShares}");
            Console.WriteLine();
        }

        static void SharesTradingTest(double percent)
        {
            Player player = new("Name");

            PlayerStats(player);

            Console.WriteLine($"Sell {percent}% of shares:");
            player.SharesToSell(percent);

            PlayerStats(player);

            Console.WriteLine("Next turn while player have shares to sell:");
            while (player.SharesOnExchange.Percent != 0.0)
            {
                player.NextTurn();
                PlayerStats(player);
            }

            Console.WriteLine("Buy shares back:");
            player.BuyShares(percent);

            PlayerStats(player);
        }
    }
}