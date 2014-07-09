using System;
using System.Collections.Generic;
using System.Linq;
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
            SendChatMessage(gameId, "System", "Player joined the chat.");
        }

        public void SendChatMessage(Guid gameId, string senderName, string message)
        {
            ForGame(gameId, null, eachClient => eachClient.updateChat(senderName, message));
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            Registry.ClientToGames.Remove(Context.ConnectionId);
            return base.OnDisconnected();
        }

        public void ForceClientRefresh(Guid gameId, IHubContext ctx = null)
        {
            ForGame(gameId, ctx, eachClient => eachClient.refresh());
        }

        private static void ForGame(Guid gameId, IHubContext ctx, Action<dynamic> actionOnValidClient)
        {
            foreach (var registration in Registry.ClientToGames.Where(x => x.Value == gameId))
            {
                actionOnValidClient(ctx.Clients().Client(registration.Key));
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