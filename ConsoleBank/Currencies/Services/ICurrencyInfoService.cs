using System;
using System.Threading.Tasks;
using Currencies.Entities;

namespace Currencies.Services
{
    public interface ICurrencyInfoService
    {
        Task<CurrencyRate> GetCurrencyRate(int currencyId, DateTime? ondate);

        Task<CurrencyRate> GetCurrencyRate(string currencyAbbreviation, DateTime? ondate);

        Task<int> GetCurrencyId(string currencyAbbreviation);
    }
}
