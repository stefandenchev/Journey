namespace Journey.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Data.Models.Chat;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ChatService : IChatService
    {
        private readonly IDeletableEntityRepository<Chat> chatsRepository;
        private readonly IDeletableEntityRepository<Message> messagesRepository;
        private readonly IRepository<ChatUser> chatUsersRepository;
        private readonly IRepository<ApplicationUser> usersRepository;

        public ChatService(
            IDeletableEntityRepository<Chat> chatsRepository,
            IDeletableEntityRepository<Message> messagesRepository,
            IRepository<ChatUser> chatUsersRepository,
            IRepository<ApplicationUser> usersRepository)
        {
            this.chatsRepository = chatsRepository;
            this.messagesRepository = messagesRepository;
            this.chatUsersRepository = chatUsersRepository;
            this.usersRepository = usersRepository;
        }

        public IEnumerable<Chat> GetChats(string userId)
        {
            return this.chatsRepository.All()
                .Include(x => x.Users)
                .Where(x => !x.Users
                    .Any(y => y.UserId == userId)
                 && x.Type != ChatType.Private)
                .ToList();
        }

        public async Task CreateRoom(string name, string chatId, string userId)
        {
            var chat = new Chat
            {
                Id = chatId,
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

        public async Task<Message> CreateMessage(string chatId, string message, string userId)
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

        public async Task<string> CreatePrivateRoom(string rootId, string chatId, string targetId)
        {
            var chatCheck = this.chatsRepository
                .All()
                .Where(x => x.Type == ChatType.Private && (x.Name == rootId + targetId || x.Name == targetId + rootId)).FirstOrDefault();

            var chat = new Chat();

            if (chatCheck == null)
            {
                chat = new Chat
                {
                    Id = chatId,
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
            }
            else
            {
                chat = chatCheck;
            }

            return chat.Id;
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

        public T GetChat<T>(string id)
        {
            var chat = this.chatsRepository
                .All()
                .Include(x => x.Messages)
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return chat;
        }

        public async Task JoinRoom(string chatId, string userId)
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

        public bool CheckRoomPrivacy(string chatId)
        {
            var chat = this.chatsRepository.All().Where(x => x.Id == chatId).FirstOrDefault();
            if (chat.Type == ChatType.Private)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<T> GetOtherUsers<T>(string userId)
        {
            return this.usersRepository
                .All()
                .Where(x => x.Id != userId)
                .To<T>()
                .ToList();
        }
    }
}
