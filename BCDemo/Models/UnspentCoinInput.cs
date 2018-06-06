using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCDemo.Models
{
    public class UnspentCoinInput
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Address { get; set; }
        public int Coin { get; set; }
    }
}
