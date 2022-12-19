using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BlackJack;

namespace Casino
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
            string card = string.Format(Deck.Cards.First().ToString() + "\n");
            Console.WriteLine(card);
            // Log to a text file
            using (StreamWriter file = new StreamWriter("C:\\Users\\School & Work\\source\\repos\\JCT88\\Large-C-Sharp-Projects\\Blackjack\\log.txt", true))
            {
                file.WriteLine(DateTime.Now);
                file.WriteLine(card);
            }
            // take the card out of the Deck.Cards List
            Deck.Cards.RemoveAt(0);
        }
    }
}
