using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Currencies.Entities;

namespace Currencies.Services
{
    public class CurrencyInfoService : ICurrencyInfoService
    {
        private readonly ICurrenciesApi _api = new CurrenciesApi();
        private static readonly Dictionary<string, List<CurrencyRate>> _currentyRates= new ();

        public async Task<CurrencyRate> GetCurrencyRate(int currencyId, DateTime? ondate)
        {
            var key = ondate.HasValue ? ondate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");

            await SetCurrencyRates(key, ondate);

            var result = _currentyRates[key].SingleOrDefault(x => x.Id == currencyId);

            if (result == null)
            {
                throw new ArgumentException("Invalid currenty rate Id!");
            }

            return result;
        }

        public async Task<CurrencyRate> GetCurrencyRate(string currencyAbbreviation, DateTime? ondate)
        {
            return await GetCurrencyRate(await GetCurrencyId(currencyAbbreviation, ondate), ondate);
        }

        public async Task<int> GetCurrencyId(string currencyAbbreviation, DateTime? ondate)
        {
            var key = ondate.HasValue ? ondate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");

            await SetCurrencyRates(key, ondate);

            return _currentyRates[key].Single(x => x.Abbreviation == currencyAbbreviation).Id;
        }

        private async Task SetCurrencyRates(string key, DateTime? ondate)
        {
            if (!_currentyRates.ContainsKey(key))
            {
                _currentyRates[key] = await _api.GetCurrencyRates(ondate);
            }
        }
    }
}
