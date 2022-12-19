using System;
using System.Collections.Generic;
using System.Text;

namespace Casino
{
    // Entities refer to a database object
    public class ExceptionEntity
    {
        public int Id { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
