using System;
using CustomCollections;

namespace GameMechanics
{
    class Tester
    {
        static void Main()
        {
            Console.WriteLine("Start of testing:\n");

            //SortedSLListTest();

            //Pause();

            //CityLandSortingRuleTest();

            //Pause();

            //ClientGenerationTest();

            //Pause();

            //CityGenerationTest();

            //Pause();

            LandManagementTest();

            Pause();

            //SharesTradingTest();

            //Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.WriteLine();
        }

        static void SortedSLListTest()
        {
            Console.WriteLine("Sorted list test. Sort by descending array of integers: [1, 2, 7, 3, 4, 6, 2, 3, 8, 9, 4, 6, 3, 9, 2]");

            SortedSLList<int> list = new((x, y) => x > y, new int[] { 1, 2, 7, 3, 4, 6, 2, 3, 8, 9, 4, 6, 3, 9, 2 });

            Console.WriteLine("Expected output: 1, 2, 2, 2, 3, 3, 3, 4, 4, 6, 6, 7, 8, 9, 9");
            Console.WriteLine("Got:");
            foreach (int n in list)
            {
                Console.Write(n + ", ");
            }
            Console.WriteLine();
        }

        static void CityLandSortingRuleTest()
        {
            SortedSLList<Land> lands = new((x, y) =>
            {
                // compare belonging to the player
                if (x.PlayerProperty && !y.PlayerProperty) return true;
                if (!x.PlayerProperty && y.PlayerProperty) return false;

                // compare presence of the building
                if (!x.HaveBuilding && y.HaveBuilding) return true;
                if (x.HaveBuilding && !y.HaveBuilding) return false;

                // compare lands with buildings
                if (x.HaveBuilding && y.HaveBuilding)
                {
                    // compare types of buildings
                    if (x.Building.Requirement.CompareTo(y.Building.Requirement) == 1) return true;
                    if (x.Building.Requirement.CompareTo(y.Building.Requirement) == -1) return false;

                    // compare sizes of buildings
                    if (x.Building.Requirement.Size > y.Building.Requirement.Size) return true;
                    if (x.Building.Requirement.Size < y.Building.Requirement.Size) return false;
                }
                // compare lands without buildings
                else
                {
                    // compare sizes of lands
                    if (x.Size > y.Size) return true;
                    if (x.Size < y.Size) return false;
                }

                return true;
            });

            City city = new(0);

            Land[] landsArray = new Land[10];

            landsArray[0] = new(city, 1);
            landsArray[1] = new(city, 3);
            landsArray[2] = new(city, 1);
            landsArray[3] = new(city, 1);
            landsArray[4] = new(city, 2);
            landsArray[5] = new(city, 2);
            landsArray[6] = new(city, 2);
            landsArray[7] = new(city, 3);
            landsArray[8] = new(city, 3);
            landsArray[9] = new(city, 3);

            landsArray[1].PlayerProperty = true;
            landsArray[3].PlayerProperty = true;
            landsArray[5].PlayerProperty = true;
            landsArray[7].PlayerProperty = true;
            landsArray[9].PlayerProperty = true;

            landsArray[0].Build(1, "Factory");
            landsArray[1].Build(2, "Factory");
            landsArray[4].Build(1, "Shop");
            landsArray[5].Build(1, "Shop");
            landsArray[8].Build(2, "Factory");
            landsArray[9].Build(1, "Factory");


            foreach (Land land in landsArray)
            {
                lands.Add(land);
            }

            Console.WriteLine("Expected 0, 8, 4, 2, 6, 9, 1, 5, 3, 7.\n");
            foreach (Land land in lands)
            {
                Console.Write(Array.IndexOf(landsArray, land) + "  ");
            }
            Console.WriteLine("\nSorting rule test completed. \n");
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
            Console.WriteLine($"Requires: {client.Requirement.Type.ToString()} of size {client.Requirement.Size}");
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

            foreach (Land land in city)
            {
                PrintLandInfo(land);
            }
        }

        static void PrintLandInfo(Land land)
        {
            Console.WriteLine($"    Land size: {land.Size}");
            Console.WriteLine($"    Land cost: {land.LandCost}");
            Console.WriteLine($"    Land taxation: {land.LandTax}");
            if (land.HaveBuilding)
            {
                Console.WriteLine($"    Land building:");
                Console.WriteLine($"        Building type: {land.Building.Requirement.Type}");
                Console.WriteLine($"        Building size: {land.Building.Requirement.Size}");
                Console.WriteLine($"        Building profit and maintenance: {land.Building.Profit} {land.Building.Maintenance}");
                Console.WriteLine($"        Building build and raze cost: {land.Building.BuilCost} {land.Building.RazeCost}");
            }
            else
            {
                Console.WriteLine($"    Land building: -");
            }
            Console.WriteLine();
        }

        static void LandManagementTest()
        {
            Console.WriteLine("Creating actors: city (tax = 10%), land in city (size = 1), client (requires factory of size 1), player");

            City city = new(10);
            city.AddLand(1);
            Land refLand = city[0];

            Client client = new(1, "Factory");

            Player player = new("Player");

            PrintCityInfo(city);
            PrintClientInfo(client);
            PrintPlayerInfo(player);

            Console.WriteLine("Buy land. -50.0, income -5.0");

            player.BuyLand(refLand);

            //Console.WriteLine("Build factory of size 1. -75.0, income -15.0 (-20.0)");

            //player.BuildBuilding(refLand, new Factory(refLand, 1));
            //city.Lands[0].Building.CityTax = 10.0;

            //Console.WriteLine("Make turn. Player money: 1000.0 - 125.0 - 20.0 = 855.0");

            //player.NextTurn();

            //PrintPlayerInfo(player);

            //Console.WriteLine("Assign client to the factory. income +27.0 (+7.0)");

            //player.RentOutBuilding(city.Lands[0], client);

            //Console.WriteLine("Make 5 turns. Player money: 855.0 + 7.0 * 5 = 890.0");

            //for (int i = 0; i < 5; i++)
            //{
            //    player.NextTurn();
            //}

            //PrintPlayerInfo(player);

            //Console.WriteLine("Raze factory");

            //player.RazeBuilding(city.Lands[0]);

            //PrintPlayerInfo(player);

            //Console.WriteLine("Nothing changed. Factory is occupied. Disable autoextention and wait 15 turns:");
            //Console.WriteLine("Player money: 890 + 7.0 * 15 = 995.0");

            //city.Lands[0].Building.AutoExtention = false;

            //for (int i = 0; i < 15; i++)
            //{
            //    player.NextTurn();
            //}

            //Console.WriteLine("Now factory is free:");

            //PrintClientInfo(client);

            //Console.WriteLine("Raze it! -15.0. Income = -5.0, Player money: 995.0 - 15.0 = 980.0");

            //player.RazeBuilding(city.Lands[0]);

            //PrintPlayerInfo(player);

            //Console.WriteLine("Land management complete successfully.\n");
        }

        static void PrintPlayerInfo(Player player)
        {
            Console.WriteLine("Player stats:");
            Console.WriteLine($"    Player name: {player.Name}");
            Console.WriteLine($"    Player money: {player.Money}");
            Console.WriteLine($"    Player income: {player.Income}");
            Console.WriteLine($"    Player reputation: {player.Reputation}");
            Console.WriteLine($"    Player shares: {player.Shares}");
            Console.WriteLine($"    Shares price: {player.SharePrice}");
            Console.WriteLine($"    Shares on exchange: {player.SharesOnExchange}");
            Console.WriteLine();
        }

        //static void SharesTradingTest()
        //{
        //    Console.WriteLine("Creating actors: player, city (tax = 0%), 5 clients (factories, size 2), factories");

        //    Player player = new("player");

        //    Client[] clients = new Client[] { new(2, typeof(Factory)), new(2, typeof(Factory)), new(2, typeof(Factory)), new(2, typeof(Factory)), new(2, typeof(Factory)) };

        //    City city = new(0);

        //    for (int i = 0; i < 5; i++)
        //    {
        //        city.Lands.Add(new(0.0, 2));
        //        city.Lands[i].Build(new Factory(2));
        //        city.Lands[i].Building.CityTax = 0.0;
        //        player.BuyLand(city.Lands[i]);
        //        player.RentOutBuilding(city.Lands[i], clients[i]);
        //    }

        //    PlayerStats(player);

        //    Console.WriteLine("Sell 50% of shares:");

        //    player.SharesToSell(50.0);

        //    PlayerStats(player);

        //    Console.WriteLine("Wait 10 turns:");

        //    for (int i = 0; i < 10; i++)
        //    {
        //        player.NextTurn();
        //    }

        //    PlayerStats(player);


        //    //Player player = new("Name");

        //    //PlayerStats(player);

        //    //Console.WriteLine($"Sell {percent}% of shares:");
        //    //player.SharesToSell(percent);

        //    //PlayerStats(player);

        //    //Console.WriteLine();

        //    //TODO: make test when player will have income
        //}
    }
}