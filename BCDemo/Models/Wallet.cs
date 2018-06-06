using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCDemo.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string Name { get; set; }
        //public List<Transaction> TransactionList { get; set; }
        public List<CoinInput> CoinInputList { get; set; }
    }
}
