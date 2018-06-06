using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCDemo.Models
{
    public class Block
    {
        public int BlockId { get; set; }
        public int Index { get; set; }
        public string CurrentHash { get; set; }
        public string PreviousHash { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Data { get; set; }
        public int Difficulty { get; set; }
        public int Nonce { get; set; }
    }
}

