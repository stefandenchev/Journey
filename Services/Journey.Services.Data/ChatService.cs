namespace Journey.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models.Chat;
    using Journey.Services.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ChatService : IChatService
    {
        private readonly IDeletableEntityRepository<Chat> chatsRepository;
        private readonly IDeletableEntityRepository<Message> messagesRepository;
        private readonly IRepository<ChatUser> chatUsersRepository;

        public ChatService(
            IDeletableEntityRepository<Chat> chatsRepository,
            IDeletableEntityRepository<Message> messagesRepository,
            IRepository<ChatUser> chatUsersRepository)
        {
            this.chatsRepository = chatsRepository;
            this.messagesRepository = messagesRepository;
            this.chatUsersRepository = chatUsersRepository;
        }

        public IEnumerable<Chat> GetChats(string userId)
        {
            return this.chatsRepository.All()
                .Include(x => x.Users)
                .Where(x => !x.Users
                    .Any(y => y.UserId == userId))
                .ToList();
        }

        public async Task CreateRoom(string name, string userId)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room,
            };

            chat.Users.Add(new ChatUser
            {
                UserId = userId,
            });

            await this.chatsRepository.AddAsync(chat);
            await this.chatsRepository.SaveChangesAsync();
        }

        public async Task<Message> CreateMessage(int chatId, string message, string userId)
        {
            var msg = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = userId,
                Timestamp = DateTime.Now,
            };

            await this.messagesRepository.AddAsync(msg);
            await this.messagesRepository.SaveChangesAsync();

            return msg;
        }

        public async Task<int> CreatePrivateRoom(string rootId, string targetId)
        {
            var chat = new Chat
            {
                Type = ChatType.Private,
            };

            chat.Users.Add(new ChatUser
            {
                UserId = targetId,
            });

            chat.Users.Add(new ChatUser
            {
                UserId = rootId,
            });

            await this.chatsRepository.AddAsync(chat);
            await this.chatsRepository.SaveChangesAsync();

            return chat.Id;
        }

        public Chat GetChat(int id)
        {
            var chat = this.chatsRepository.All()
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.Id == id);

            return chat;
        }

        public IEnumerable<Chat> GetPrivateChats(string userId)
        {
            return this.chatsRepository.All()
                   .Include(x => x.Users)
                       .ThenInclude(x => x.User)
                   .Where(x => x.Type == ChatType.Private
                       && x.Users
                           .Any(y => y.UserId == userId))
                   .ToList();
        }

        public async Task JoinRoom(int chatId, string userId)
        {
            var chatUser = new ChatUser
            {
                ChatId = chatId,
                UserId = userId,
            };

            await this.chatUsersRepository.AddAsync(chatUser);
            await this.chatUsersRepository.SaveChangesAsync();
        }

        public IEnumerable<Chat> GetUserChats(string userId)
        {
            return this.chatUsersRepository.All()
                .Include(x => x.Chat)
                .Where(x => x.UserId == userId
                    && x.Chat.Type == ChatType.Room)
                .Select(x => x.Chat)
                .ToList();
        }
    }
}
