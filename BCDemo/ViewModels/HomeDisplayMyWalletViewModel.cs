using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCDemo.Models;

namespace BCDemo.ViewModels
{
    public class HomeDisplayMyWalletViewModel
    {
        public Wallet Wallet { get; set; }
        public IEnumerable<CoinOutput> CoinOutList { get; set; }
    }
}
