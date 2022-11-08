using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public struct Card
    {
        public Suit Suit { get; set; }
        public Face Face { get; set; }
        // Override the ToString method for this class
        public override string ToString()
        {
            return string.Format("{0} of {1}", Face, Suit);
        }
    }
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
    public enum Face
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
}
