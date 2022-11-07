using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class BlackjackGame : Game, IWalkAway
    {
        public override void Play()
        {
            throw new NotImplementedException();
        }
        public override void ListPlayers()
        {
            Console.WriteLine("21 Players:");
            base.ListPlayers();
        }
        public void Walkaway(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
