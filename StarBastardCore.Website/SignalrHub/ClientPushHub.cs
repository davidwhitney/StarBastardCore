using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace StarBastardCore.Website.SignalrHub
{
    public class ClientPushHub : Hub
    {
        private static readonly ClientRegistry Registry = new ClientRegistry();


        public void RegisterForUpdates(Guid gameId)
        {
            Registry.ClientToGames.Add(Context.ConnectionId, gameId);
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            Registry.ClientToGames.Remove(Context.ConnectionId);

            return base.OnDisconnected();
        }

        public void ForceClientRefresh(Guid gameId, IHubContext ctx = null)
        {
            foreach (var registration in Registry.ClientToGames.Where(x => x.Value == gameId))
            {
                ctx.Clients().Client(registration.Key).refresh();
            }
        }
    }

    public static class ExtensionsForClients
    {
        public static IHubConnectionContext Clients(this IHubContext passedClient)
        {
            return passedClient == null
                       ? GlobalHost.ConnectionManager.GetHubContext<ClientPushHub>().Clients
                       : passedClient.Clients;
        }
    }

    public class ClientRegistry
    {
        public Dictionary<string, Guid> ClientToGames { get; private set; }

        public ClientRegistry()
        {
            ClientToGames = new Dictionary<string, Guid>();
        }
    }
}