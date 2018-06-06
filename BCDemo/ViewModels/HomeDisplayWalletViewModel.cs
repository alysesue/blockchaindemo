using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCDemo.Models;

namespace BCDemo.ViewModels
{
    public class HomeDisplayWalletViewModel
    {
        public IEnumerable<Wallet> WalletList { get; set; }
        public IEnumerable<CoinInput> CoinInputList { get; set; }
    }
}
