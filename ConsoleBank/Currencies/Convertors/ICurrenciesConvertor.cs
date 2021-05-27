using System.Threading.Tasks;

namespace Currencies
{
    public interface ICurrenciesConvertor
    {
       Task<decimal> ConvertToByn(string currencyAbbreviation, decimal amount);

       Task<decimal> ConvertFromByn(string currencyAbbreviation, decimal amount);
    }
}
