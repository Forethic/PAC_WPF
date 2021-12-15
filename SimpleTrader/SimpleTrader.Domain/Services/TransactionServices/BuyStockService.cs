using System;
using System.Threading.Tasks;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public class BuyStockService : IBuyStockService
    {
        private readonly IStockPriceService _StockPriceService;
        private readonly IDataService<Account> _AccountService;

        public BuyStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _StockPriceService = stockPriceService;
            _AccountService = accountService;
        }

        public async Task<Account> BuyStock(Account buyer, string symbol, int shares)
        {
            double stockPrice = await _StockPriceService.GetPrice(symbol);
            double transactionPrice = stockPrice * shares;

            if (transactionPrice > buyer.Balance)
            {
                throw new InsufficientFundsException(buyer.Balance, transactionPrice);
            }

            AssetTransaction transaction = new AssetTransaction()
            {
                Account = buyer,
                Asset = new Asset()
                {
                    PriceShare = stockPrice,
                    Symbol = symbol,
                },
                DateProcessed = DateTime.Now,
                Shares = shares,
                IsPurchase = true,
            };

            buyer.AssetTransactions.Add(transaction);
            buyer.Balance -= transactionPrice;

            await _AccountService.Update(buyer.Id, buyer);

            return buyer;
        }
    }
}