using TSLServer.Service.ViewModel;

namespace TSLServer.Service.Interface
{
    public interface ICurrencyService
    {
        Task<CurrencyViewModel> GenerateCurrency(string currencyCode);
    }
}
