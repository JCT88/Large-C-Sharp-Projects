using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public abstract class Game
    {
        public List<Player> Players { get; set; }
        public string Name { get; set; }
        // Dictionary to store player bets
        public Dictionary<Player, int> Bets { get; set; }
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
