using System.Threading.Tasks;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Results;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        public async Task<double> GetPrice(string symbol)
        {
            using (FinancialModelingPrepHttpClient client = new FinancialModelingPrepHttpClient())
            {
                string uri = "stock/real-time-price/" + symbol;

                StockPriceResult stockPriceResult = await client.GetAsync<StockPriceResult>(uri);

                if (stockPriceResult.Price == 0)
                {
                    throw new InvalidSymbolException(symbol);
                }
                return stockPriceResult.Price;
            }
        }
    }
}