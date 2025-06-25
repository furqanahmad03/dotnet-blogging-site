using Microsoft.AspNetCore.SignalR;
using X.Models;

namespace X.Hubs
{
  public class BlogHub : Hub
  {
    public async Task SendMessage(Blog blog)
    {
      await Clients.All.SendAsync("ReceiveNewBlog", blog);
    }
  }
}