using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public abstract class Game
    {
        // lists need to be instantiated before they are used
        public List<Player> Players { get; set; }
        public string Name { get; set; }
        public string Dealer { get; set; }

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
