using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class BlackjackDealer : Dealer
    {
        private List<Card> _hand = new List<Card>();
        public List<Card> Hand { get { return _hand; } set { _hand = value; } }
        public bool Stay { get; set; }
        public bool IsBusted { get; set; }
    }
}
