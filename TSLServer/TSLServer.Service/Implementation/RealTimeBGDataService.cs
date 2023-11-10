using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TSLServer.Core.Constants;
using TSLServer.Service.Interface;

namespace TSLServer.Service.Implementation
{
    /// <summary>
    /// RealTimeBGDataService
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Hosting.BackgroundService" />
    public class RealTimeBGDataService : BackgroundService
    {
        /// <summary>
        /// Get or set Ilogger
        /// </summary>
        private readonly ILogger<RealTimeBGDataService> _logger;

        /// <summary>
        /// Get or set IHubContext
        /// </summary>
        private readonly IHubContext<BroadCastingHub> _hubContext;

        /// <summary>
        /// Get or set ICurrencyService
        /// </summary>
        private readonly ICurrencyService _service;

        /// <summary>
        /// Initializes a new instance of the RealTimeBGDataService
        /// </summary>
        /// <param name="logger">The Ilogger.</param>
        /// <param name="hubContext">The IHubContext.</param>
        /// <param name="service">The ICurrencyService.</param>
        public RealTimeBGDataService(ILogger<RealTimeBGDataService> logger, 
            IHubContext<BroadCastingHub> hubContext, ICurrencyService service)
        {
            _logger = logger;
            _hubContext = hubContext;
            _service = service;
        }

        /// <summary>
        /// Execute the task in background
        /// </summary>
        /// <param name="stoppingToken">The cancellation token.</param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Service is running.");

            ExecuteTask();

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(2));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    ExecuteTask();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Timed Hosted Service is stopping.");
            }
        }

        /// <summary>
        /// Task to execute
        /// </summary>
        /// <returns></returns>
        private void ExecuteTask()
        {
            var task = Task.Run(async () => await _service.GenerateCurrency(WSConstants.GBPToUSDGrp));
            _hubContext.Clients.Group(WSConstants.GBPToUSDGrp).SendAsync(WSConstants.MsgSubject,
                task.Result);
        }
    }
}
