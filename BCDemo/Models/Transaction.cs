using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCDemo.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public string CurrentHash { get; set; }
        public string OwnerPublicKey { get; set; }
        public string OwnerPrivateKey { get; set; }
        public string PrevOwnerSignature { get; set; }
        public List<CoinInput> CoinInputList { get; set; }
        public List<CoinOutput> CoinOutputList { get; set; }
        public int WalletId { get; set; }
    }
}
