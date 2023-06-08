using System;
using CustomCollections;

namespace GameMechanics
{
    public class World
    {
        public const int StartingCities = 3;
        public const int StartingClients = 15;

        protected Player _player;
        protected SLList<City> _cities;
        protected SortedSLList<Client> _clients;

        protected int _turnCounter;

        public Player Player => _player;
        public SLList<City> Cities => _cities;
        public SortedSLList<Client> Clients => _clients;
        public int Turn => _turnCounter;

        public World(string name)
        {
            _player = new(name);

            _cities = new();
            _clients = new((x, y) =>
            {
                if (x.IsHolder && !y.IsHolder) return true;
                if (!x.IsHolder && y.IsHolder) return false;

                if (!x.IsHolder && !y.IsHolder)
                {
                    if (x.Requirement.CompareTo(y.Requirement) == 1) return true;
                    if (x.Requirement.CompareTo(y.Requirement) == -1) return false;

                    if (x.Requirement.Size > y.Requirement.Size) return true;
                    if (x.Requirement.Size < y.Requirement.Size) return false;
                }

                return true;
            });

            for (int i = 0; i < StartingCities; i++)
            {
                _cities.Add(new());
            }

            for (int i = 0; i < StartingClients; i++)
            {
                _clients.Add(new());
            }
        }

        public void Expand()
        {
            if (_player.Money >= 100)
            {
                _player.Money -= 100;
                _cities.Add(new());
            }
        }

        public void NextTurn()
        {
            _turnCounter++;
            _player.NextTurn();

            Random random = new();

            if (random.Next(1, 101) < 40)
            {
                _clients.Add(new());
            }
        }
    }
}