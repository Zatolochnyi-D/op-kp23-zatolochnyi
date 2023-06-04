using System;
using CustomCollections;

namespace GameMechanics
{
    public class World
    {
        public const int StartingCities = 3;
        public const int StartingClients = 8;

        protected Player _player;
        protected SLList<City> _cities;
        protected SLList<Client> _clients;

        public Player Player => _player;
        public SLList<City> Cities => _cities;

        public World(string name)
        {
            _player = new(name);

            _cities = new();
            _clients = new();

            for (int i = 0; i < StartingCities; i++)
            {
                _cities.Add(new());
            }

            for (int i = 0; i < StartingClients; i++)
            {
                _clients.Add(new());
            }
        }

        public void NextTurn()
        {
            _player.NextTurn();
        }
    }
}