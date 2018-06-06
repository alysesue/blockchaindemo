using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCDemo.Models;

namespace BCDemo.ViewModels
{
    public class HomeAddCoinOutViewModel
    {
        public string Address { get; set; }
        public double Amount { get; set; }
        public string PublicKey { get; set; }
        public int WalletId { get; set; }
        //public List<Transaction> TransactionList { get; set; }
    }
}
