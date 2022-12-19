using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Casino;
using Casino.Blackjack;
using System.Data.SqlClient;
using System.Data;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            // Greet the user and store thier name in "playerName"
            Console.WriteLine("Welcome to the Grand Hotel Casino. Let's start by telling me your name.");
            string playerName = Console.ReadLine();
            // If you are an admin, then get a list of exceptions
            if(playerName == "admin")
            {
                List<ExceptionEntity> Exceptions = ReadExceptions();
                foreach(var exception in Exceptions)
                {
                    Console.Write(exception.Id + " | ");
                    Console.Write(exception.ExceptionType + " | ");
                    Console.Write(exception.ExceptionMessage + " | ");
                    Console.Write(exception.TimeStamp + " | ");
                    Console.WriteLine();
                }
                Console.Read();
                return;
            }
            // Ask the user how much they want to spend and store it in "bank"
            bool validAnswer = false;
            int bank = 0;
            while (!validAnswer)
            {
                Console.WriteLine("How much money did you bring today?");
                // Try to parse the user input as a number and wait for a response
                validAnswer = int.TryParse(Console.ReadLine(), out bank);
                // If they still don't give a proper whole number, then return false and a message
                if (!validAnswer) Console.WriteLine("Please enter digits only");
            }
            // Ask  if the user wants to play
            Console.WriteLine("Hello, {0}. Would you like to join a game of  blackjack right now?", playerName);
            string answer = Console.ReadLine().ToLower();
            // Check if the player wants to play
            if (answer == "yes" || answer == "yeah" || answer == "ya")
            {
                // Instantiate player object with their info
                Player player = new Player(playerName, bank);
                // Create a player Id
                player.Id = Guid.NewGuid();
                // Log the player Id with their initial balance
                using (StreamWriter file = new StreamWriter("C:\\Users\\School & Work\\source\\repos\\JCT88\\Large-C-Sharp-Projects\\Blackjack\\log.txt", true))
                {
                    file.WriteLine(player.Name + ": " +player.Id + " Balance: " + player.Balance);
                }
                // Start a new game
                Game game = new BlackjackGame();
                // Add the player to the game
                game += player;
                // Create an exit from a while loop if they choose to leave
                player.IsActivelyPlaying = true;
                // Continue checking if the player is still playing and if they have enough money to play
                while (player.IsActivelyPlaying && player.Balance > 0)
                {
                    try
                    {
                        game.Play();
                    }
                    catch (FraudException ex)
                    {
                        Console.WriteLine(ex.Message);
                        UpdateDbWithException(ex);
                        Console.ReadLine();
                        return;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Something you entered was incorrect");
                        UpdateDbWithException(ex);
                        Console.ReadLine();
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occured, please contact your system admin.");
                        UpdateDbWithException(ex);
                        Console.ReadLine();
                        return;
                    }
                }
                // When the game is over, remove the player and thank them for playing
                game -= player;
                Console.WriteLine("Thank you for playing.");
            }
            Console.WriteLine("Feel free to look around the casino. Bye for now.");
        }
        private static void UpdateDbWithException(Exception ex)
        {
            string connectionString = @"Data Source=(localdb)\ProjectsV13;
                                        Initial Catalog=BlackjackGame;Integrated Security=True;
                                        Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                        ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string queryString = @"INSERT INTO Exceptions (ExceptionType, ExceptionMessage, TimeStamp) VALUES
                                    (@ExceptionType, @ExceptionMessage, @TimeStamp)";
            // "using" statements help close connections to resources outside the Common Language Runtime
            // This helps prevent SQL injection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                // Add parameter types to the variables
                command.Parameters.Add("@ExceptionType", SqlDbType.VarChar);
                command.Parameters.Add("@ExceptionMessage", SqlDbType.VarChar);
                command.Parameters.Add("@TimeStamp", SqlDbType.DateTime);
                // Set the values for the parameters
                command.Parameters["@ExceptionType"].Value = ex.GetType().ToString();
                command.Parameters["@ExceptionMessage"].Value = ex.Message;
                command.Parameters["@TimeStamp"].Value = DateTime.Now;
                // Connection and db write
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private static List<ExceptionEntity> ReadExceptions()
        {
            string connectionString = @"Data Source=(localdb)\ProjectsV13;
                                        Initial Catalog=BlackjackGame;Integrated Security=True;
                                        Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                        ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string queryString = @"SELECT Id, ExceptionType, ExceptionMessage, TimeStamp From Exceptions";
            List<ExceptionEntity> Exceptions = new List<ExceptionEntity>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                // Connection and db write
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ExceptionEntity exception = new ExceptionEntity();
                    exception.Id = Convert.ToInt32(reader["Id"]);
                    exception.ExceptionType = reader["ExceptionType"].ToString();
                    exception.ExceptionMessage = reader["ExceptionMessage"].ToString();
                    exception.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);
                    Exceptions.Add(exception);
                }
                connection.Close();
            }
            return Exceptions;
        }
    }
}