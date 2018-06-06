using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCDemo.Models
{
    public class CoinInput
    {
        public int CoinInputId { get; set; }
        public int Index { get; set; }
        public string Signature { get; set; }
        public int Amount { get; set; }
        public string PublicKey { get; set; }
 
        //public int TransactionId { get; set; }

        public int WalletId { get; set; }
    }
}
