using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hubs;
public class ChatHub : Hub
{
    public Dictionary<string, string> Messages = new Dictionary<string, string>();
    public async Task SendMessageAsync(string message, string callerId)
    {
        Messages.Add(callerId,message);
        await Clients.All.SendAsync("MessageReceived", Messages);
    }
    public override Task OnConnectedAsync()
    {
        var x = Clients;
        // Hub'a bağlanan ilk nokta
        // authorization? => Controller
        return Task.CompletedTask;
    }
}
