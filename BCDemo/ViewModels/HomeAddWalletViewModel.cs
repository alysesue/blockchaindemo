using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCDemo.Models;

namespace BCDemo.ViewModels
{
    public class HomeAddWalletViewModel
    {
        public string Name { get; set; }




        public string Id { get; set; }
        public string CurrentHash { get; set; }
        public string OwnerPublicKey { get; set; }
        public string OwnerPrivateKey { get; set; }
        public string PrevOwnerSignature { get; set; }
        public List<Transaction> TransactionList { get; set; }



        //public List<CoinInput> CoinInputList { get; set; }
        //public List<CoinOutput> CoinOutputList { get; set; }
    }
}
