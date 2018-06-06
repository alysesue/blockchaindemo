using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCDemo.Models;

namespace BCDemo.ViewModels
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            //HomeAddBlockViewModel = new HomeAddBlockViewModel();
            HomeDisplayBlocksViewModel = new HomeDisplayBlocksViewModel();
            HomeDisplayWalletViewModel = new HomeDisplayWalletViewModel();
            //HomeAddWalletViewModel = new HomeAddWalletViewModel();
            //HomeAddCoinInViewModel = new HomeAddCoinInViewModel();
            //HomeDisplayCoinInViewModel = new HomeDisplayCoinInViewModel();
            //HomeAddCoinOutViewModel = new HomeAddCoinOutViewModel();
        }
        //public HomeAddBlockViewModel HomeAddBlockViewModel { get; set; }
        public HomeDisplayBlocksViewModel HomeDisplayBlocksViewModel { get; set; }
        public HomeDisplayWalletViewModel HomeDisplayWalletViewModel { get; set; }
        //public HomeAddWalletViewModel HomeAddWalletViewModel { get; set; }
        //public HomeAddCoinInViewModel HomeAddCoinInViewModel { get; set; }
        //public HomeDisplayCoinInViewModel HomeDisplayCoinInViewModel { get; set; }

        //public HomeAddCoinOutViewModel HomeAddCoinOutViewModel { get; set; }
    }
}
