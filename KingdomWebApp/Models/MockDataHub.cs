using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace KingdomWebApp.Models
{
    public class MockDataHub : Hub
    {
        public async Task SendMockDataAsync(List<MockData> MockData) =>
            await Clients.All.SendAsync("ReceiveMockData", MockData);
    }
}
