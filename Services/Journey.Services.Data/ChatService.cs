namespace Journey.Services.Data
{
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

        public ChatService(IDeletableEntityRepository<Chat> chatsRepository)
        {
            this.chatsRepository = chatsRepository;
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
    }
}
