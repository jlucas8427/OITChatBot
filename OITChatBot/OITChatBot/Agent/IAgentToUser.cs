using Microsoft.Bot.Connector;
using System.Threading;
using System.Threading.Tasks;

namespace OITChatBot
{
    public interface IAgentToUser
    {
        Task SendToUserAsync(Activity message, CancellationToken cancellationToken);
    }
}
