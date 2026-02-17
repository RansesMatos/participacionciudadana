using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ParticipacionDigital.Web.Hubs
{
    public class VoteHub : Hub
    {
        public async Task SendVoteUpdate(int encuestaId)
        {
            await Clients.All.SendAsync("ReceiveVoteUpdate", encuestaId);
        }
    }
}
