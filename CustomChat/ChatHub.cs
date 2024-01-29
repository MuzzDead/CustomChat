using Microsoft.AspNetCore.SignalR;

namespace CustomChat;

public class ChatHub : Hub
{
	public async Task Send(string message, string userName) // додай час 
	{
		await Clients.All.SendAsync("Receive", message, userName);
	}
}

