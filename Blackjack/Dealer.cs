using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    public class Dealer
    {
        public string Name { get; set; }
        public Deck Deck { get; set; }
        public int Balance { get; set; }
        public void Deal(List<Card> Hand)
        {
            // Take the first card, deal it
            Hand.Add(Deck.Cards.First());
            Console.WriteLine(Deck.Cards.First().ToString() + "\n");
            // take the card out of the Deck.Cards List
            Deck.Cards.RemoveAt(0);
        }
    }
}
