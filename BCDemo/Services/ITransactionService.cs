using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCDemo.Models;

namespace BCDemo.Services
{
    public interface ITransactionService
    {
        void CreateNewTransaction(Transaction t, string name);
        void CreateWallet(Wallet w);

        void SendCoin(CoinOutput co); //withdrawal

        void ReceiveCoin(CoinInput ci); //deposit

        List<Transaction> GetTransactionList();

        List<Wallet> GetWalletList();

        Wallet FindWallet(string name);

        void AddCoinIn(CoinInput input);

        void AddCoinOut(CoinOutput output);

        void GetCoinOutsByWalletId(string ownerPublicKey);

        List<CoinInput> GetCoinInList();
        IEnumerable<CoinInput> GetCoinInsByWalletId(int walletId);
    }
}
