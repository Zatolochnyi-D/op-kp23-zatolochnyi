using System;
using CustomCollections;

namespace GameMechanics
{
    class Tester
    {
        static void Main()
        {
            Console.WriteLine("Start of testing:\n");

            ClientGenerationTest();

            Pause();

            CityGenerationTest();

            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.WriteLine();
        }

        static void ClientGenerationTest()
        {
            Console.WriteLine("Testing client generation. Create 3 clients.");

            Client client1 = new();
            Client client2 = new();
            Client client3 = new();

            Console.WriteLine("3 randomly generated clients:\n");
            PrintClientInfo(client1);
            PrintClientInfo(client2);
            PrintClientInfo(client3);

            Console.WriteLine("Clients are generated successfully.");
            Console.WriteLine();
        }

        static void PrintClientInfo(Client client)
        {
            Console.WriteLine($"Name: {client.Name}");
            Console.WriteLine($"Holds property?: {client.IsHolder}");
            Console.WriteLine($"Requires: {client.Requirement.BuildingType.ToString()} of size {client.Requirement.Size}");
            Console.WriteLine();
        }

        static void CityGenerationTest()
        {
            Console.WriteLine("Testing city generation. Create 3 cities.");

            City city1 = new();
            City city2 = new();
            City city3 = new();

            Console.WriteLine("3 randomly generated cities:");
            PrintCityInfo(city1);
            PrintCityInfo(city2);
            PrintCityInfo(city3);

            Console.WriteLine("Cities are generated successfully.");
            Console.WriteLine();
        }

        static void PrintCityInfo(City city)
        {
            Console.WriteLine($"City name: {city.Name}");
            Console.WriteLine($"City taxation: {city.Taxation}");
            Console.WriteLine($"City lands:");

            foreach (Land land in city.Lands)
            {
                Console.WriteLine($"    Land size: {land.Size}");
                if (land.Building != null)
                {
                    Console.WriteLine($"    Land building:");
                    Console.WriteLine($"        Building type: {land.Building.Requirement.BuildingType}");
                    Console.WriteLine($"        Building size: {land.Building.Requirement.Size}");
                    Console.WriteLine($"        Building income and maintenance: {land.Building.Income} {land.Building.Maintenance}");
                }
                else
                {
                    Console.WriteLine($"    Land building: -");
                }
                Console.WriteLine();
            }
        }


        static void PlayerStats(Player player)
        {
            Console.WriteLine("Player stats:");
            Console.WriteLine($"Player shares: {player.PlayerShares.Percent}");
            Console.WriteLine($"Shares on exchange: {player.SharesOnExchange}");
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

            Console.WriteLine();

            //TODO: make test when player will have income
        }
    }
}