﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {

            Deck deck = new Deck();

            int count = deck.Cards.Count(x => x.Face == Face.Ace);

            List<Card> newList = deck.Cards.Where(x => x.Face == Face.King).ToList();


            Console.WriteLine(count);

            //Deck deck = new Deck();
            //deck.Shuffle(3);

            //foreach (Card card in deck.Cards)
            //{ 
            //    Console.WriteLine(card.Face + " of " + card.Suit);
            //}
            Console.ResetColor();
        }
    }
}