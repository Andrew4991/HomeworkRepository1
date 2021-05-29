using System.Threading.Tasks;

namespace Currencies
{
    public class CurrenciesConvertor : ICurrenciesConvertor
    {
        private readonly CurrenciesApi _api = new();

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
            var currencyRate = await _api.GetCurrencyRate(currencyId, null);
            return currencyRate.Scale / currencyRate.Rate;
        }
    }
}
