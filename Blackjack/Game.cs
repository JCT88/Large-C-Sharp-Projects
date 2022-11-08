using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public abstract class Game
    {
        // Always ensure that an empty list is instantiated to avoid NullReferenceException
        private List<Player> _player = new List<Player>();
        private Dictionary<Player, int> _bets = new Dictionary<Player, int>();
        public List<Player> Players { get { return _player; } set { _player = value; } }
        public string Name { get; set; }
        // Dictionary to store player bets 
        public Dictionary<Player, int> Bets { get { return _bets; } set { _bets = value; } }
        public abstract void Play();
        public virtual void ListPlayers ()
        {
            foreach(Player player in Players)
            {
                Console.WriteLine(player.Name);
            }
        }
    }
}
