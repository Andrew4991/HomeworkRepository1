using System.Threading.Tasks;
using Currencies.Services;

namespace Currencies
{
    public class CurrenciesConvertor : ICurrenciesConvertor
    {
        private readonly ICurrencyInfoService _apiService = new CurrencyInfoService();

        public async Task<double> ConvertFromByn(int currencyId, double amount)
        {
            return amount * await GetRate(currencyId);
        }

        public async Task<double> ConvertToByn(int currencyId, double amount)
        {
            return amount / await GetRate(currencyId);
        }

        private async Task<double> GetRate(int currencyId)
        {
            var currencyRate = await _apiService.GetCurrencyRate(currencyId);
            return currencyRate.Scale / currencyRate.Rate;
        }
    }
}
