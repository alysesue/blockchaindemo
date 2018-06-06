using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCDemo.Models
{
    public class CoinOutput
    {
        public int CoinOutputId { get; set; }
        public string Address { get; set; }
        public double Amount { get; set; }

        //
        public string PublicKey { get; set; }
        //public int TransactionId { get; set; }

        public int WalletId { get; set; }
    }
}
