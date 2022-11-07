using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class BlackjackGame : Game, IWalkAway
    {
        public BlackjackDealer Dealer { get; set; }
        // This play method will encompass a full hand of blackjack
        public override void Play()
        {
            // Bring in a new dealer
            Dealer = new BlackjackDealer();
            // Reset each player
            foreach(Player player in Players)
            {
                // Empty the players hand
                player.Hand = new List<Card>();
                // Reset Stay property
                player.Stay = false;
            }
            // Empty the dealers hand
            Dealer.Hand = new List<Card>();
            // Reset the dealers stay propery
            Dealer.Stay = false;
            // Give the dealer a new deck
            Dealer.Deck = new Deck();
            Console.WriteLine("Place your bet!");
            foreach (Player player in Players)
            {
                int bet = Convert.ToInt32(Console.ReadLine());
                bool successfullyBet = player.Bet(bet);
                // If the user doesn't have enough for their bet
                // and IsActivelyPlaying is true, then the Play()
                // method will run again, so the user can try 
                // betting again
                if (!successfullyBet)
                {
                    return; // Run this to end the method
                }
                // Enter the user' bet into the Game.Bets dictionary
                Bets[player] = bet;
            }

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
