using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hubs;
public class ChatHub : Hub
{
    static List<Message> Messages = new();
    public async Task SendMessageAsync(string message, string callerId)
    {
        Messages.Add(new Message() { Detail=message, ConnectionId=callerId});
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

public class Message
{
    public string Detail { get; set; }
    public string ConnectionId { get; set; }
}
