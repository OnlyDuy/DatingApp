namespace API.SignalR
{
    public class PresenceTracker
    {
        private static readonly Dictionary<string, List<string>> OnlineUsers = 
            new Dictionary<string, List<string>>();

        // Tạo phương thức để thêm người dùng vào từ điển khi họ kết nối cùng với id kết nối của họ 
        public Task UserConnected(string username, string connectionId)
        {
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(username))
                {
                    OnlineUsers[username].Add(connectionId);
                }
                else
                {
                    OnlineUsers.Add(username, new List<string> { connectionId });
                }
            }

            return Task.CompletedTask;
        }
        // Xử lý trường hợp họ ngắt kết nối
         public Task UserDisconnected(string username, string connectionId)
        {
            lock (OnlineUsers)
            {
                // Kiểm tra người dùng đang trực tuyến có chưa khóa nếu khóa này không tồn tại thì...
                if (!OnlineUsers.ContainsKey(username)) return Task.CompletedTask;

                OnlineUsers[username].Remove(connectionId);
                if (OnlineUsers[username].Count == 0)
                {
                    OnlineUsers.Remove(username);
                }
            }

            return Task.CompletedTask;
        }

        // Phương thức lấy tất cả người dùng đang Online
         public Task<string[]> GetOnlineUsers()
        {
            string[] onlineUsers;
            lock (OnlineUsers)
            {
                onlineUsers = OnlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
            }

            return Task.FromResult(onlineUsers);
        }
    }
}