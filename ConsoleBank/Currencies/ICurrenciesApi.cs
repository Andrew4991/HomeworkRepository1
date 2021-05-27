using System;
using System.Threading.Tasks;
using Currencies.Entities;

namespace Currencies
{
    public interface ICurrenciesApi
    {
        Task<Currency[]> GetCurrencies();
        Task<CurrencyRate> GetCurrencyRate(int currencyId);
        Task<CurrencyRate> GetCurrencyRate(string currencyAbbreviation);
        Task<CurrencyRate> GetCurrencyRate(int currencyId, DateTime ondate);
        Task<CurrencyRate> GetCurrencyRate(string currencyAbbreviation, DateTime ondate);
    }
}
