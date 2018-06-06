using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCDemo.ViewModels
{
    public class HomeAddCoinInViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Signature { get; set; }
        public int Amount { get; set; }

        public int WalletId { get; set; }

        //
        public string OwnerPublicKey { get; set; }
    }
}
