using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker _tracker;
        public PresenceHub(PresenceTracker tracker)
        {
            _tracker = tracker;
        }
        // tạo phương thức ghi đè để kết nối người dùng
        // Gửi dữ liệu là đang online
        public override async Task OnConnectedAsync()
        {
            await _tracker.UserConnected(Context.User.GetUsername(), Context.ConnectionId);
            await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUsername());

            // lấy lại danh sách các người dùng hiện đang trực tuyến
            var currentUsers = await _tracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);
        }

        // Gửi dữ liệu là đang ofline
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _tracker.UserDisconnected(Context.User.GetUsername(), Context.ConnectionId);
            await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUsername());

            // lấy lại danh sách các người dùng hiện đang trực tuyến
            var currentUsers = await _tracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);

            await base.OnDisconnectedAsync(exception);
        }


    }
}