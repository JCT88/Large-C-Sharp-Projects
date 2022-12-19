using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Casino.Blackjack
{
    class BlackjackRules
    {
        // Create a face to value dictionary to add up values-------------------
        // Use an underscore in front of private names
        private static Dictionary<Face, int> _cardValues = new Dictionary<Face, int>()
        {
            [Face.Two] = 2,
            [Face.Three] = 3,
            [Face.Four] = 4,
            [Face.Five] = 5,
            [Face.Six] = 6,
            [Face.Seven] = 7,
            [Face.Eight] = 8,
            [Face.Nine] = 9,
            [Face.Ten] = 10,
            [Face.Jack] = 10,
            [Face.Queen] = 10,
            [Face.King] = 10,
            [Face.Ace] = 1 // add a check for whether this is worth 1 or 10

        };
        // Get all the possible values with  ace combinations-------------------
        private static int[] GetAllPossibleHandValues(List<Card> Hand)
        {
            // Check how many aces a player has
            int aceCount = Hand.Count(x => x.Face == Face.Ace);
            // Unique combinations of aces in a hand are the count + 1
            // aceCount + 1 is the array length
            int[] result = new int[aceCount + 1];
            // Check for the deafult sum of all cards in hand
            int value = Hand.Sum(x => _cardValues[x.Face]);
            // Add the value to the results array
            result[0] = value;
            // If there is no ace, then return the value
            if (result.Length == 1) return result;
            // Put in different values of ace
            // This should create all possible hand values
            for (int i = 1; i < result.Length; i++)
            {
                // add 10 per ace to the hands value
                value += (i * 10);
                result[i] = value;
            }
            return result;
        }
        // Check to see if the player has blackjack (21)--------------------------
        public static bool CheckForBlackjack(List<Card> Hand)
        {
            // Add all possible values to an int array
            int[] possibleValues = GetAllPossibleHandValues(Hand);
            int value = possibleValues.Max();
            if (value == 21) return true;
            else return false;
        }
        // Check to see if the hand is a bust-------------------------------------
        public static bool IsBusted(List<Card> Hand)
        {
            // Check for the smallest possible sum 
            int value = GetAllPossibleHandValues(Hand).Min();
            // Check if the value is higher than 21 and return 
            // true for bust or false for no bust
            if (value > 21) return true;
            else return false;
        }
        public static bool ShouldDealerStay(List<Card> Hand)
        {
            // Create a list of possible sums with the dealers hand
            int[] possibleHandValues = GetAllPossibleHandValues(Hand);
            // Check all of the values in the list of sums
            foreach(int value in possibleHandValues)
            {
                // Rules for what the dealer should stay on
                if (value > 16 && value < 22)
                {
                    return true;
                }
            }
            // If the stay conditions are not met, then return false
            return false;
        }
        // Compare the hands of the dealer and the player
        public static bool? CompareHands(List<Card> PlayerHand, List<Card> DealerHand)
        {
            // Get all possible hand values for the dealer and player
            int[] playerResults = GetAllPossibleHandValues(PlayerHand);
            int[] dealerResults = GetAllPossibleHandValues(DealerHand);
            // Get the largest player and dealer hand value less than 22
            int playerScore = playerResults.Where(x => x < 22).Max();
            int dealerScore = dealerResults.Where(x => x < 22).Max();
            // Check if the players score is higher or lower
            if (playerScore > dealerScore) return true; // Player wins
            if (playerScore < dealerScore) return false; // Player loses
            // In case of a tie
            else return null;

        }
    }
}
