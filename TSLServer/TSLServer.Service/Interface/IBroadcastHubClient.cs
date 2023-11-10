using TSLServer.Service.ViewModel;

namespace TSLServer.Service.Interface
{
    /// <summary>
    /// IBroadcastHubClient
    /// </summary>
    public interface IBroadcastHubClient
    {
        /// <summary>
        /// Sending an update to subscriber
        /// </summary>
        /// <returns></returns>
        Task SendUpdates(CurrencyViewModel message);
    }
}
