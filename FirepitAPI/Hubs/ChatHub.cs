using Microsoft.AspNetCore.SignalR;
using NLog;
using System.Diagnostics;
using System.Threading.Tasks;
using Firepit.Shared;
using System.Collections.Generic;
using System;

namespace FirepitAPI.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, string> userLookup = new Dictionary<string, string>();

        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync(Messages.RECEIVE, username, message);
        }

        public async Task Register(string username)
        {
            var currentId = Context.ConnectionId;
            if (!userLookup.ContainsKey(currentId))
            {
                userLookup.Add(currentId, username);

                await Clients.AllExcept(currentId).SendAsync(Messages.RECEIVE, username, $"{username} joined the chat");
            }
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            string id = Context.ConnectionId;
            if (!userLookup.TryGetValue(id, out string username))
                username = "[unknown]";

            userLookup.Remove(id);
            await Clients.AllExcept(Context.ConnectionId).SendAsync(
                Messages.RECEIVE,
                username, $"{username} has left the chat");

            await base.OnDisconnectedAsync(e);
        }
    }

}