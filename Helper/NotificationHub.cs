using Microsoft.AspNetCore.SignalR;

namespace ECARE.Helper
{
    public class NotificationHub : Hub
    {
        // Clients call this method after connecting to join the appropriate group
        public async Task JoinGroup(string labId)
        {
            // Use a consistent group name format
            await Groups.AddToGroupAsync(Context.ConnectionId, $"LabGroup-{labId}");
        }
    }
}
