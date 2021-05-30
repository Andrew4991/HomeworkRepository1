using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Currencies.Entities;

namespace Currencies
{
    public interface ICurrenciesApi
    {
        Task<Currency[]> GetCurrencies();
        Task<CurrencyRate> GetCurrencyRate(int currencyId, DateTime? ondate = null);
        Task<CurrencyRate> GetCurrencyRate(string currencyAbbreviation, DateTime? ondate = null);

        Task<List<CurrencyRate>> GetCurrencyRates(DateTime? ondate = null);
    }
}
