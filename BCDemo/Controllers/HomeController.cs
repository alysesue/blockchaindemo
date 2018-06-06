using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCDemo.Models;
using BCDemo.Services;
using BCDemo.ViewModels;

namespace BCDemo.Controllers
{
    public class HomeController : Controller
    {
        private IBlockService _blockservice;
        private ITransactionService _transactionservice;

        public HomeController(IBlockService bservice, ITransactionService tservice)
        {
            _blockservice = bservice;
            _transactionservice = tservice;
        }

        public int Difficulty = 0;

        [HttpGet]
        public IActionResult Index()
        {
            HomeIndexViewModel vm = new HomeIndexViewModel();

            IEnumerable<Block> sessionBlockList = SessionHelper.Get<IEnumerable<Block>>(HttpContext.Session, "BlockList");

            if (sessionBlockList == null)
            {
                sessionBlockList = new List<Block>
                {
                    new Block
                    {
                        Index = 0,
                        CurrentHash = "0tUACuv4vofd3fUCYbntgiA==:DSAHZaATyFXEAYNrkUukhQ==",
                        PreviousHash = "0",
                        TimeStamp = new DateTime(2018, 02, 02, 12, 00, 00, 0000),
                        Data = "This is the genesis block",
                        Difficulty = Difficulty,
                        Nonce = 0
                    },
                };
                SessionHelper.Set<IEnumerable<Block>>(HttpContext.Session, "BlockList", sessionBlockList);
            }

            vm.HomeDisplayBlocksViewModel = new HomeDisplayBlocksViewModel
            {
                BlockList = sessionBlockList
            };

            IEnumerable<Wallet> sessionWalletList = SessionHelper.Get<IEnumerable<Wallet>>(HttpContext.Session, "WalletList");
            IEnumerable<CoinInput> sessionCoinInList = SessionHelper.Get<IEnumerable<CoinInput>>(HttpContext.Session, "CoinInList");

            if (sessionWalletList == null)
            {
                sessionWalletList = new List<Wallet>
                {
                    new Wallet
                    {
                        WalletId = 1,
                        Name = "Tom",
                        PrivateKey = "123private",
                        PublicKey = "12345678"
                    },

                    new Wallet
                    {
                        WalletId = 2,
                        Name = "Jane",
                        PrivateKey = "123private",
                        PublicKey = "87654321"
                    },
                };
                SessionHelper.Set<IEnumerable<Wallet>>(HttpContext.Session, "WalletList", sessionWalletList);
            }

            if (sessionCoinInList == null)
            {
                sessionCoinInList = new List<CoinInput>
                {
                    new CoinInput
                    {
                        CoinInputId = 1,
                        Index = 1,
                        Amount = 10,
                        Signature = "this is the signature",
                        PublicKey = "12345678",
                        WalletId = 1
                    },

                    new CoinInput
                    {
                        CoinInputId = 2,
                        Index = 1,
                        Amount = 20,
                        Signature = "this is the signature",
                        PublicKey = "87654321",
                        WalletId = 2
                    },
                };
                SessionHelper.Set<IEnumerable<CoinInput>>(HttpContext.Session, "CoinInList", sessionCoinInList);
            }

            vm.HomeDisplayWalletViewModel = new HomeDisplayWalletViewModel
            {
                WalletList = sessionWalletList,
                CoinInputList = sessionCoinInList
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult AddBlock()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBlock(HomeAddBlockViewModel vm)
        {
            IEnumerable<Block> sessionBlockList = SessionHelper.Get<IEnumerable<Block>>(HttpContext.Session, "BlockList");

            Block block = new Block();

            block.Index = sessionBlockList.Max(s => s.Index) + 1;
            block.PreviousHash = sessionBlockList.Last().CurrentHash;
            block.TimeStamp = DateTime.Now;
            block.Data = vm.Data;
            block.CurrentHash = HashHelper.CalculateHash(block.Index + block.PreviousHash + block.TimeStamp + block.Data + block.Nonce);
            block.Difficulty = Difficulty;
            block.Nonce = 0;

            bool ValidateHashMatchesDiff(string currentHash)
            {
                currentHash = block.CurrentHash;
                int count = 0;

                for (int i = 0; i < currentHash.Length; i++)
                {
                    if (currentHash[i] == 0)
                    {
                        count++;
                    }
                    if (currentHash[i] != 0)
                    {
                        break;
                    }
                }

                if (count == Difficulty)
                {
                    return true;
                }
                return false;
            }


            while (!ValidateHashMatchesDiff(block.CurrentHash))
            {
                block.Nonce = block.Nonce++;
                block.TimeStamp = DateTime.Now;
                block.CurrentHash = block.Difficulty + (HashHelper.CalculateHash(block.Index + block.PreviousHash + block.TimeStamp + block.Data + block.Nonce));
            };

            sessionBlockList = sessionBlockList.Append(block);

            SessionHelper.Set<IEnumerable<Block>>(HttpContext.Session, "BlockList", sessionBlockList);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DisplayBlocks()
        {
            IEnumerable<Block> sessionBlockList = SessionHelper.Get<IEnumerable<Block>>(HttpContext.Session, "BlockList");

            if (sessionBlockList != null)
            {
                HomeDisplayBlocksViewModel vm = new HomeDisplayBlocksViewModel
                {
                    BlockList = sessionBlockList
                };

                return View(vm);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult AddWallet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWallet(HomeAddWalletViewModel vm)
        {
            IEnumerable<Wallet> sessionWalletList = SessionHelper.Get<IEnumerable<Wallet>>(HttpContext.Session, "WalletList");

            Wallet wallet = new Wallet();

            wallet.PrivateKey = HashHelper.RandomNumberGen();
            wallet.PublicKey = HashHelper.RandomNumberGen(wallet.PrivateKey);
            wallet.Name = vm.Name;

            sessionWalletList = sessionWalletList.Append(wallet);

            SessionHelper.Set<IEnumerable<Wallet>>(HttpContext.Session, "BlockList", sessionWalletList);

            return RedirectToAction("Index", "Home");
        }

        //[HttpGet]
        //public IActionResult DisplayWallet()
        //{
        //    List<Wallet> walletList = _transactionservice.GetWalletList();

        //    if (walletList != null)
        //    {
        //        HomeDisplayWalletViewModel vm = new HomeDisplayWalletViewModel();

        //        vm.WalletList = walletList;

        //        foreach (var item in walletList)
        //        {
        //            vm.CoinInputList = _transactionservice.GetCoinInsByWalletId(item.WalletId);
        //        }

        //        return View(vm);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        [HttpGet]
        public IActionResult DisplayWallet()
        {
            IEnumerable<Wallet> sessionWalletList = SessionHelper.Get<IEnumerable<Wallet>>(HttpContext.Session, "WalletList");
            IEnumerable<CoinInput> sessionCoinInList = SessionHelper.Get<IEnumerable<CoinInput>>(HttpContext.Session, "CoinInList");

            if (sessionWalletList != null)
            {
                HomeDisplayWalletViewModel vm = new HomeDisplayWalletViewModel
                {
                    WalletList = sessionWalletList,
                    CoinInputList = sessionCoinInList
                };

                return View(vm);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult DisplayMyWallet()
        {
            IEnumerable<CoinOutput> sessionCoinOutList = SessionHelper.Get<IEnumerable<CoinOutput>>(HttpContext.Session, "CoinOutList");

            HomeDisplayMyWalletViewModel vm = new HomeDisplayMyWalletViewModel
            {
                Wallet = new Wallet
                {
                    Name = "My Wallet",
                    PrivateKey = "private",
                    PublicKey = "public",
                    WalletId = 90898989,
                    CoinInputList = sessionCoinOutList //this is wrong as it coin in vs coin out
                }
            };

            return View(vm);
        }


        [HttpGet]
        public IActionResult AddCoinOut(string publicKey)
        {
            IEnumerable<Wallet> sessionWalletList = SessionHelper.Get<IEnumerable<Wallet>>(HttpContext.Session, "WalletList");

            Wallet wallet = sessionWalletList.Where(w => w.PublicKey == publicKey).First();

            HomeAddCoinOutViewModel vm = new HomeAddCoinOutViewModel
            {
                PublicKey = wallet.PublicKey,
                WalletId = wallet.WalletId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddCoinOut(HomeAddCoinOutViewModel vm)
        {
            IEnumerable<CoinOutput> sessionCoinOutList = SessionHelper.Get<IEnumerable<CoinOutput>>(HttpContext.Session, "CoinOutList");

            CoinOutput coinOutput = new CoinOutput();

            coinOutput.PublicKey = vm.PublicKey;
            coinOutput.Address = vm.Address;
            coinOutput.Amount = vm.Amount;
            coinOutput.WalletId = vm.WalletId;

            sessionCoinOutList = sessionCoinOutList.Append(coinOutput);

            SessionHelper.Set<IEnumerable<CoinOutput>>(HttpContext.Session, "CoinOutList", sessionCoinOutList);

            IEnumerable<Block> sessionBlockList = SessionHelper.Get<IEnumerable<Block>>(HttpContext.Session, "BlockList");

            Block block = new Block
            {
                Data = HashHelper.CalculateHash(coinOutput.ToString()),
            };

            sessionBlockList = sessionBlockList.Append(block);

            SessionHelper.Set<IEnumerable<Block>>(HttpContext.Session, "BlockList", sessionBlockList);

            return RedirectToAction("Index", "Home");
        }


        //Coin in

        [HttpGet]
        public IActionResult AddCoinIn(string publicKey)
        {
            Wallet wallet = _transactionservice.FindWallet(publicKey);

            HomeAddCoinInViewModel vm = new HomeAddCoinInViewModel
            {
                OwnerPublicKey = wallet.PublicKey,
                WalletId = wallet.WalletId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddCoinIn(HomeAddCoinInViewModel vm)
        {
            CoinInput coinInput = new CoinInput
            {
                PublicKey = vm.OwnerPublicKey,
                Signature = "placeholder signature",
                Amount = vm.Amount,
                WalletId = vm.WalletId
            };

            _transactionservice.AddCoinIn(coinInput);

            Block block = new Block
            {
                Data = HashHelper.CalculateHash(coinInput.ToString()),
            };

            _blockservice.AddNextBlock(block);

            return RedirectToAction("Index", "Home");
        }

    }
}
