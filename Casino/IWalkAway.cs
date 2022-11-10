using System;
using System.Collections.Generic;
using System.Text;

namespace Casino
{
    // An interface can be used to add methods and properties to multiple classes
    // when inheriting isn't appropriate
    interface IWalkAway // the I is to indicate this is an interfzace
    {
        void Walkaway(Player player);
    }
}
