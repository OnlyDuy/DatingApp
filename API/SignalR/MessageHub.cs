using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly UserRepository _userRepository;
        public MessageHub(IMessageRepository messageRepository, IMapper mapper, UserRepository userRepository)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public override async Task OnConnectedAsync()
        {
            // khi tạo kết nối đến trung tâm này chúng ta sẽ chuyển tên người dùng khác
            // bằng khóa người dùng và nhận được thông tin như dưới 
            var HttpContext = Context.GetHttpContext();
            var otherUser = HttpContext.Request.Query["User"].ToString();
            var groupName = GetGroupName(Context.User.GetUsername(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            // khi người dùng đã tham gia một nhóm
            // Lưu tin nhắn của người dùng
            var messages = await _messageRepository.
                GetMessageThread(Context.User.GetUsername(), otherUser);

            await Clients.Group(groupName).SendAsync("ReceiveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        // phương thức gửi tin nhắn
        public async Task SendMessage(CreateMessageDto createMessageDto) 
        {
            // Lấy tên người dùng
            var username = Context.User.GetUsername();
            // kiểm tra tên người dungf có bằng với tên người nhận trong CreateMessageDto không
            if (username == createMessageDto.RecipientUsername.ToLower())
                throw new HubException("You cannot send messages to yourself");

            var sender = await _userRepository.GetUserByUsernameAsync(username);
            var recipient = await _userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

            // Nếu không có nhười nhận, kiểm tra điều này và xem liệu nó có rỗng không
            if (recipient == null) throw new HubException("Not found user");

            // tạo tin nhắn mới và lưu trữ tin nhắn 
            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            _messageRepository.AddMessage(message);
            // lưu trữ nhóm tin nhắn
            if (await _messageRepository.SaveAllAsync()) {
                var group = GetGroupName(sender.UserName, recipient.UserName);
                await Clients.Group(group).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
            }

        }

        // phương thức nhóm các User trong cùng message
         private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}