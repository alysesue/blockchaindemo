using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCDemo.Models;
using System.Security.Cryptography;

namespace BCDemo.Services
{
    public class TransactionService : ITransactionService
    {
        private List<Transaction> _transactions = new List<Transaction>();
        private List<Wallet> _wallets;
        private List<CoinInput> _coininputs;
        private List<CoinOutput> _coinoutputs = new List<CoinOutput>();

        private double _coins = 0;

        //private string privateKeyLocation = "node/wallet/private_key";

        public TransactionService()
        {
            _wallets = new List<Wallet>
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

            _coininputs = new List<CoinInput>
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
        }

        public void CreateNewTransaction(Transaction t, string name)
        {
            //t.Id = HashHelper.CalculateHash(t.CoinInputList.ToString() + t.CoinOutputList.ToString());
            t.OwnerPublicKey = FindWallet(name).PublicKey;
            t.CurrentHash = HashHelper.CalculateHash(FindWallet(name).PublicKey + _transactions.Last().CurrentHash + _transactions.Last().PrevOwnerSignature);
            t.PrevOwnerSignature = _transactions.Last().OwnerPublicKey + _transactions.Last().OwnerPrivateKey;
            t.OwnerPrivateKey = FindWallet(name).PrivateKey;
        }

        public void CreateWallet(Wallet w)
        {
            w.PrivateKey = HashHelper.RandomNumberGen();
            w.PublicKey = HashHelper.RandomNumberGen(w.PrivateKey);
            //w.PrivateKey = RandomNumberGenerator.Create().ToString();
            //w.PrivateKey = HashHelper.CalculateHash("jjjjgjg").ToString();
            //w.PublicKey = RandomNumberGenerator.Create().ToString();
            //w.PublicKey = HashHelper.CalculateHash(w.PrivateKey).ToString();
            //w.MyTransactionList = w.MyTransactionList;
            w.Name = w.Name;

            _wallets.Add(w);
        }

        public void SendCoin(CoinOutput co)
        {
            double balance = _coins - co.Amount;
        }

        public void ReceiveCoin(CoinInput ci)
        {
            double balance = _coins + ci.Index;
        }

        public List<Transaction> GetTransactionList()
        {
            return _transactions;
        }

        public List<Wallet> GetWalletList()
        {
            return _wallets;
        }

        public Wallet FindWallet(string publicKey)
        {
            Wallet wallet = _wallets.Find(s => s.PublicKey == publicKey);
            return wallet;
        }

        public void AddCoinIn(CoinInput input)
        {
            _coininputs.Add(input);
        }

        public void AddCoinOut(CoinOutput output)
        {
            _coinoutputs.Add(output);
        }

        public void GetCoinOutsByWalletId(string ownerPublicKey)
        {
            _coinoutputs.Where(s => s.PublicKey == ownerPublicKey);
        }

        public List<CoinInput> GetCoinInList()
        {
            return _coininputs;
        }

        public IEnumerable<CoinInput> GetCoinInsByWalletId(int walletId)
        {
            return _coininputs.Where(s => s.WalletId == walletId);
        }
    }
}
