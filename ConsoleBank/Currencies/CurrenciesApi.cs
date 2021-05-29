using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Currencies.Entities;
using Flurl;
using Flurl.Http;

namespace Currencies
{
    internal class CurrenciesApi : ICurrenciesApi
    {
        private const string CurrencyRatesApiUrl = "https://www.nbrb.by/api/exrates/rates";
        private const string CurrenciesApiUrl = "https://www.nbrb.by/api/exrates/currencies";
        private readonly List<CurrencyRate> _currentyRates = new();
        private int _now;

        public Task<Currency[]> GetCurrencies()
        {
            return CallApi(() => CurrenciesApiUrl.GetJsonAsync<Currency[]>());
        }

        public async Task<CurrencyRate> GetCurrencyRate(int currencyId, DateTime? ondate)
        {
            if (!ondate.HasValue)
            {
                if (_now != DateTime.Now.DayOfYear)
                {
                    await SetListCurrencyRates();
                }

                return await Task.FromResult(_currentyRates.Where(x => x.Id == currencyId).FirstOrDefault());
            }

            return await CallApi(() => CurrencyRatesApiUrl
                    .AppendPathSegment(currencyId)
                    .SetQueryParams(new { ondate = ondate?.ToString("yyyy-MM-dd") })
                    .GetJsonAsync<CurrencyRate>());
        }

        private async Task SetListCurrencyRates()
        {
            _currentyRates.AddRange(await CallApi(() => CurrencyRatesApiUrl
                                .SetQueryParams(new { periodicity = 0 })
                                .GetJsonAsync<CurrencyRate[]>()));

            _now = DateTime.Now.DayOfYear;
        }

        public async Task<CurrencyRate> GetCurrencyRate(string currencyAbbreviation, DateTime? ondate)
        {
            if (!ondate.HasValue)
            {
                if (_now != DateTime.Now.DayOfYear)
                {
                    await SetListCurrencyRates();
                }

                return await Task.FromResult(_currentyRates.Where(x => x.Abbreviation == currencyAbbreviation).FirstOrDefault());
            }

            return await CallApi(() => CurrencyRatesApiUrl
                    .AppendPathSegment(currencyAbbreviation)
                    .SetQueryParams(new { parammode = 2})
                    .SetQueryParams(new { ondate = ondate?.ToString("yyyy-MM-dd") })
                    .GetJsonAsync<CurrencyRate>());
        }

        private static async Task<T> CallApi<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (FlurlHttpException e) when (e.StatusCode == 404)
            {
                throw new CurrencyNotAvailableException("Currency not available");
            }
        }
    }
}
