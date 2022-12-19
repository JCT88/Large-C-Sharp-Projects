using System;
using System.Collections.Generic;
using System.Text;

namespace Casino
{
    // A fraud exception that inherits from the exception base class
    public class FraudException : Exception
    {
        // Use constructor chains to customize the constructor in Exception.cs
        public FraudException()
            :base(){ }
        // Overload with a message parameter
        public FraudException(string message)
            : base(message) { }
    }
}
