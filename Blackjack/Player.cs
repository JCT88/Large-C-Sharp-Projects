using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class Player
    {
        // Constructor for the player class
        public Player (string name, int beginningBalance)
        {
            Hand = new List<Card>();
            Balance = beginningBalance;
            Name = name;
        }
        public List<Card> Hand { get; set; }
        public int Balance { get; set; }
        public string Name { get; set; }
        public bool IsActivelyPlaying { get; set; }
        // This Stay property should be put into a new BlackjackPlayer class that would be less abstract
        public bool Stay { get; set; }
        // Give the user the ability to bet
        public bool Bet(int amount)
        {
            // check if the user has enough to make their bet
            if (Balance - amount < 0)
            {
                Console.WriteLine("You do not have enough to make that bet");
                return false;
            }
            else
            {
                // Take the users bet from their bank
                Balance -= amount;
                return true;
            }
        }

        // Overloading an operator will make the operator perform the method attached to it
        public static Game operator +(Game game, Player player)
        {
            game.Players.Add(player);
            return game;
        }
        public static Game operator -(Game game, Player player)
        {
            game.Players.Remove(player);
            return game;
        }
    }
}
