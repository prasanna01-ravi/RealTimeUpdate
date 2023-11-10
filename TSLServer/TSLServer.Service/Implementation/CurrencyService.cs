using Microsoft.Extensions.Logging;
using TSLServer.Core.Constants;
using TSLServer.Core.Helper;
using TSLServer.Service.Interface;
using TSLServer.Service.ViewModel;

namespace TSLServer.Service.Implementation
{
    /// <summary>
    /// CurrencyService
    /// </summary>
    /// <seealso cref="TSLServer.Service.Interface.ICurrencyService" />
    public class CurrencyService: ICurrencyService
    {
        /// <summary>
        /// Get or set Ilogger
        /// </summary>
        private readonly ILogger<CurrencyService> _logger;

        /// <summary>
        /// Get or set CurrencyList
        /// </summary>
        private Dictionary<string, LinkedList<double>>? _currencyList;

        /// <summary>
        /// Initializes a new instance of the CurrencyService
        /// </summary>
        /// <param name="logger">The Ilogger.</param>
        public CurrencyService(ILogger<CurrencyService> logger)
        {
            _logger = logger;
            InitData();
        }

        /// <summary>
        /// Initializing the data
        /// </summary>
        /// <returns></returns>
        private void InitData()
        {
            _logger.LogInformation("Seeding Init Date.");
            if (_currencyList == null || _currencyList.Any())
            {
                _currencyList = new Dictionary<string, LinkedList<double>>();

                _currencyList.Add(WSConstants.GBPToUSDGrp, new LinkedList<double>(
                    RandomCurrencyGenerator.randomCurrencyGenerator(WSConstants.baseGBPToUSD, 4)));
            }
        }

        /// <summary>
        /// Generating new conversion value
        /// </summary>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns></returns>
        public async Task<CurrencyViewModel> GenerateCurrency(string currencyCode)
        {
            _logger.LogInformation("Removing and adding new Data.");
            lock (_currencyList)
            {
                if (currencyCode != null && _currencyList.ContainsKey(currencyCode))
                {
                    LinkedList<double>? currencyList = _currencyList.GetValueOrDefault(currencyCode);

                    currencyList.RemoveFirst();
                    currencyList.AddLast(RandomCurrencyGenerator.randomCurrencyGenerator(WSConstants.baseGBPToUSD, 1)
                        .FirstOrDefault());

                    _currencyList[currencyCode] = currencyList;
                    return new CurrencyViewModel()
                    {
                        Values = currencyList.ToList()
                    };
                }
                else
                {
                    return null;
                }
            }            
        }
    }
}
