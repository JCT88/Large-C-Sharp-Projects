using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            Dealer.Deck.Shuffle(3);
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
            // Deal all players 2 cards each
            for (int i  = 0; i < 2; i++)
            {
                Console.WriteLine("Dealing...");
                // Loop through players
                foreach (Player player in Players)
                {
                    Console.WriteLine("{0}", player.Name);
                    // Take a card out of the dealers Deck and give it to the player
                    Dealer.Deal(player.Hand);
                    // Check for Blackjack (21)
                    if(i == 1)
                    {
                        bool blackJack = BlackjackRules.CheckForBlackjack(player.Hand);
                        if (blackJack)
                        {
                            Console.WriteLine("Blackjack! {0} wins", player.Name, Bets[player]);
                            // In Blackjack, you win your bet times 1.5 and you get your bet back
                            player.Balance += Convert.ToInt32((Bets[player] * 1.5) + Bets[player]);
                            return;
                        }
                    }
                }
                Console.Write("Dealer: ");
                Dealer.Deal(Dealer.Hand);
                // Check if the dealer has Blackjack
                if (i == 1)
                {
                    bool blackJack = BlackjackRules.CheckForBlackjack(Dealer.Hand);
                    if (blackJack)
                    {
                        Console.WriteLine("Dealer has blackjack! Everyone loses!");
                        // Add all the balances to the dealers balance
                        foreach(KeyValuePair<Player, int> entry in Bets)
                        {
                            Dealer.Balance += entry.Value;
                        }
                        return;
                    }
                }
            }
            foreach(Player player in Players)
            {
                while (!player.Stay)
                {
                    // Show the player their cards
                    Console.WriteLine("Your cards are: ");
                    foreach (Card card in player.Hand)
                    {
                        // Use the overridden ToString method in Card.cs
                        // to show the Face and Suit enums for cards in
                        // the players hand
                        Console.Write("{0} ", card.ToString());
                    }
                    // Ask the user if they want to hit or stay
                    Console.WriteLine("\n\nHit or stay?");
                    string answer = Console.ReadLine().ToLower();
                    // Check the users answer
                    if (answer == "stay")
                    {
                        // Stop checking once the user stays
                        player.Stay = true;
                        break;
                    }
                    else if (answer == "hit")
                    {
                        Dealer.Deal(player.Hand);
                    }
                    bool busted = BlackjackRules.IsBusted(player.Hand);
                    if (busted)
                    {
                        Dealer.Balance += Bets[player];
                        Console.WriteLine("{0] busted! You lose a bet of {1}. Your balance is now {2}.", player.Name, Bets[player], player.Balance);
                        Console.WriteLine("Do you want to play again?");
                        answer = Console.ReadLine().ToLower();
                        if(answer == "yes" || answer == "yeah")
                        {
                            player.IsActivelyPlaying = true;
                        }
                        else
                        {
                            player.IsActivelyPlaying = false;
                        }
                    }
                }
            }
            // Check if the dealer is busted
            Dealer.IsBusted = BlackjackRules.IsBusted(Dealer.Hand);
            // Check if the dealers stay conditions are met
            Dealer.Stay = BlackjackRules.ShouldDealerStay(Dealer.Hand);
            // If the dealer stays and isn't busted
            while (!Dealer.Stay && !Dealer.IsBusted)
            {
                Console.WriteLine("Dealer is hitting...");
                // Deal a card to the dealer
                Dealer.Deal(Dealer.Hand);
                // Check if the dealer is busted or if they stay
                // Either of these will break the while loop
                Dealer.IsBusted = BlackjackRules.IsBusted(Dealer.Hand);
                Dealer.Stay = BlackjackRules.ShouldDealerStay(Dealer.Hand);
            }
            if (Dealer.Stay)
            {
                Console.WriteLine("Dealer is staying...");
            }
            if (Dealer.IsBusted)
            {
                Console.WriteLine("Dealer is busted!");
                // If the dealer busts, then the players get their winnings
                foreach(KeyValuePair<Player, int> entry in Bets)
                {
                    // Show the player their winnings
                    // entry.Key.Name is how to access the key
                    Console.WriteLine("{0} won {1}!", entry.Key.Name, entry.Value);
                    // Give the player their bet x2
                    // For now, this game only has functionality for one player
                    // so that's why we only take the .First() value
                    Players.Where(x => x.Name == entry.Key.Name).First().Balance += (entry.Value *2);
                    // Take the balance from the dealers and end the round
                    Dealer.Balance -= entry.Value;
                }
            }
            // Compare the players hand to the dealers hand
            foreach (Player player in Players)
            {
                // Turn playerWon boolean into a nullable datatype
                bool? playerWon = BlackjackRules.CompareHands(player.Hand, Dealer.Hand);
                // In the case of a tie
                if (playerWon == null)
                {
                    Console.WriteLine("Push! No one winds.");
                    // Give the player his money back
                    player.Balance += Bets[player];
                    // Take the player out of the bets dictionary
                    Bets.Remove(player);
                }
                // If the player wins
                else if (playerWon == true)
                {
                    Console.WriteLine("{0} won {1}!", player.Name, Bets[player]);
                    // Give the player their bet x 2
                    player.Balance += (Bets[player] * 2);
                    Dealer.Balance -= Bets[player];
                }
                // If the dealer wins
                else
                {
                    Console.WriteLine("Dealer wins {0}!", Bets[player]);
                    player.Balance -= Bets[player];
                    Dealer.Balance += Bets[player];
                }
                // Check if the user wants to play again
                Console.WriteLine("Play again?");
                string answer = Console.ReadLine().ToLower();
                // Store their answer as a boolean for the main while loop
                if (answer == "yes" || answer == "yeah")
                {
                    player.IsActivelyPlaying = true;
                    return; // End the void function Play()
                }
                else
                {
                    player.IsActivelyPlaying = false;
                    return; // End the void function Play()
                }
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
