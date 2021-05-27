using System.Threading.Tasks;

namespace Currencies
{
    public class CurrenciesConvertor : ICurrenciesConvertor
    {
        private readonly CurrenciesApi _api = new();

        public async Task<decimal> ConvertFromByn(string currencyAbbreviation, decimal amount)
        {
            return amount / await GetRate(currencyAbbreviation);
        }

        public async Task<decimal> ConvertToByn(string currencyAbbreviation, decimal amount)
        {
            return amount * await GetRate(currencyAbbreviation);
        }

        private async Task<decimal> GetRate(string currencyAbbreviation)
        {
            return (decimal)(await _api.GetCurrencyRate(currencyAbbreviation)).Rate;
        }
    }
}
