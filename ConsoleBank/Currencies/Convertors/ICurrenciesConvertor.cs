using System.Threading.Tasks;

namespace Currencies
{
    public interface ICurrenciesConvertor
    {
        Task<double> ConvertToByn(int currencyId, double amount);

        Task<double> ConvertFromByn(int currencyId, double amount);
    }
}
