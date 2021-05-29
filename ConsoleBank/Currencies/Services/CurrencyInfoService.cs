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
        private readonly List<Currency> _currenties = new();

        public async Task SetAbbreviation()
        {
            foreach (var cur in await _api.GetCurrencies())
            {
                _currenties.Add(cur);
            }
        }

        public Task<int> GetCurrencyId(string currencyAbbreviation)
        {
            return Task.FromResult(_currenties.Where(x => x.Abbreviation == currencyAbbreviation).Last().Id);
        }

        public Task<CurrencyRate> GetCurrencyRate(int currencyId, DateTime? ondate)
        {
            return _api.GetCurrencyRate(currencyId, ondate);
        }

        public Task<CurrencyRate> GetCurrencyRate(string currencyAbbreviation, DateTime? ondate)
        {
            return _api.GetCurrencyRate(currencyAbbreviation, ondate);
        }
    }
}
