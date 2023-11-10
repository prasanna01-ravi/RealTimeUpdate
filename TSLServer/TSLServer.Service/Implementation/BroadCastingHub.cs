using Microsoft.AspNetCore.SignalR;
using TSLServer.Core.Constants;
using TSLServer.Service.Interface;
using TSLServer.Service.ViewModel;

namespace TSLServer.Service.Implementation
{
    /// <summary>
    /// BroadCastingHub
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.SignalR.Hub" />
    public class BroadCastingHub: Hub<IBroadcastHubClient>
    {
        /// <summary>
        /// Get or set Connections
        /// </summary>
        private static readonly HashSet<string> _connections = new HashSet<string>();

        /// <summary>
        /// Triggered When client connected to Websocket
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            string connId = Context.ConnectionId;
            if(!_connections.Contains(connId))
            {
                _connections.Add(connId);
                await Groups.AddToGroupAsync(connId, WSConstants.GBPToUSDGrp);
            }
            
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Sending an update to subscriber
        /// </summary>
        /// <returns></returns>
        public async Task SendUpdates(CurrencyViewModel message)
        {
            await Clients.All.SendUpdates(message);
        }

        /// <summary>
        /// Triggered When client disconnected from Websocket
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connId = Context.ConnectionId;
            if(_connections.Contains(connId))
            {
                _connections.Remove(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(connId, WSConstants.GBPToUSDGrp);
                await base.OnDisconnectedAsync(exception);
            }            
        }
    }
}
