using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Casino;
using Casino.Blackjack;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            // Greet the user and store thier name in "playerName"
            Console.WriteLine("Welcome to the Grand Hotel Casino. Let's start by telling me your name.");
            string playerName = Console.ReadLine();
            // Ask the user how much they want to spend and store it in "bank"
            Console.WriteLine("And how much money did you bring today?");
            int bank = Convert.ToInt32(Console.ReadLine());
            // Ask  if the user wants to play
            Console.WriteLine("Hello, {0}. Would you like to join a game of  blackjack right now?", playerName);
            string answer = Console.ReadLine().ToLower();
            // Check if the player wants to play
            if (answer == "yes" || answer == "yeah" || answer == "ya")
            {
                // Instantiate player object with their info
                Player player = new Player(playerName, bank);
                // Start a new game
                Game game = new BlackjackGame();
                // Add the player to the game
                game += player;
                // Create an exit from a while loop if they choose to leave
                player.IsActivelyPlaying = true;
                // Continue checking if the player is still playing and if they have enough money to play
                while (player.IsActivelyPlaying && player.Balance > 0)
                {
                    game.Play();
                }
                // When the game is over, remove the player and thank them for playing
                game -= player;
                Console.WriteLine("Thank you for playing.");
            }
            Console.WriteLine("Feel free to look around the casino. Bye for now.");
        }
    }
}
