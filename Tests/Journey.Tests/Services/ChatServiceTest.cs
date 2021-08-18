namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Data.Models.Chat;
    using Journey.Services.Data;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Cart;
    using Journey.Web.ViewModels.Chat;
    using Moq;
    using Xunit;

    public class ChatServiceTest
    {
        private readonly ChatService chatService;
        private readonly Mock<IDeletableEntityRepository<Chat>> chatsRepo;
        private readonly Mock<IDeletableEntityRepository<Message>> messagesRepo;
        private readonly Mock<IRepository<ChatUser>> chatUsersRepo;
        private readonly Mock<IRepository<ApplicationUser>> appUsersRepo;
        private readonly List<Chat> chatList;
        private readonly List<Message> messageList;
        private readonly List<ChatUser> chatUserList;
        private readonly List<ApplicationUser> userList;

        public ChatServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.chatsRepo = new Mock<IDeletableEntityRepository<Chat>>();
            this.messagesRepo = new Mock<IDeletableEntityRepository<Message>>();
            this.chatUsersRepo = new Mock<IRepository<ChatUser>>();
            this.appUsersRepo = new Mock<IRepository<ApplicationUser>>();

            this.chatList = new List<Chat>();
            this.messageList = new List<Message>();
            this.chatUserList = new List<ChatUser>();
            this.userList = new List<ApplicationUser>();

            this.chatService = new ChatService(
                this.chatsRepo.Object,
                this.messagesRepo.Object,
                this.chatUsersRepo.Object,
                this.appUsersRepo.Object);

            this.chatsRepo.Setup(x => x.All()).Returns(this.chatList.AsQueryable());
            this.chatsRepo.Setup(x => x.AddAsync(It.IsAny<Chat>())).Callback(
                (Chat item) => this.chatList.Add(item));
            this.chatsRepo.Setup(x => x.Delete(It.IsAny<Chat>())).Callback(
                (Chat item) => this.chatList.Remove(item));

            this.messagesRepo.Setup(x => x.All()).Returns(this.messageList.AsQueryable());
            this.messagesRepo.Setup(x => x.AllAsNoTracking()).Returns(this.messageList.AsQueryable());
            this.messagesRepo.Setup(x => x.AddAsync(It.IsAny<Message>())).Callback(
                (Message item) => this.messageList.Add(item));

            this.chatUsersRepo.Setup(x => x.All()).Returns(this.chatUserList.AsQueryable());
            this.chatUsersRepo.Setup(x => x.AddAsync(It.IsAny<ChatUser>())).Callback(
                (ChatUser item) => this.chatUserList.Add(item));

            this.appUsersRepo.Setup(x => x.All()).Returns(this.userList.AsQueryable());
            this.appUsersRepo.Setup(x => x.AddAsync(It.IsAny<ApplicationUser>())).Callback(
                (ApplicationUser item) => this.userList.Add(item));
        }

        [Fact]
        public async Task CreateChatShouldWorkCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            var roomId = "TestRoomId";
            var roomName = "TestRoomName";

            await this.chatService.CreateChat(roomName, roomId, user.Identity.Name);

            Assert.Single(this.chatList);
        }

        [Fact]
        public async Task JoinChatShouldWorkCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            var roomId = "TestRoomId";

            await this.chatService.JoinChat(roomId, user.Identity.Name);

            Assert.Single(this.chatUserList);
        }

        [Fact]
        public async Task CreateMessageShouldWorkCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            var roomId = "TestRoomId";
            var message = "Hello there!";

            await this.chatService.CreateMessage(roomId, message, user.Identity.Name);

            Assert.Single(this.messageList);
        }

/*        [Fact]
        public async Task GetChatShouldWorkCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            var roomId = "TestRoomId";
            var roomName = "TestRoomName";

            await this.chatService.CreateChat(roomName, roomId, user.Identity.Name);
            var chat = this.chatService.GetChat<ChatViewModel>(roomId);

            Assert.NotNull(chat);
        }*/

        [Fact]
        public void CheckPrivacyShouldReturnTrueWhenPrivate()
        {
            this.chatList.Add(new Chat
            {
                Id = "TestRoomId",
                Type = ChatType.Private,
            });

            Assert.True(this.chatService.CheckChatPrivacy(this.chatList[0].Id));
        }

        [Fact]
        public void CheckPrivacyShouldReturnFalseWhenPublic()
        {
            this.chatList.Add(new Chat
            {
                Id = "TestRoomId",
                Type = ChatType.Room,
            });

            Assert.False(this.chatService.CheckChatPrivacy(this.chatList[0].Id));
        }

        [Fact]
        public void GetOtherUsersShouldWorkCorrectly()
        {
            this.userList.Add(new ApplicationUser
            {
                Id = "User1",
            });

            this.userList.Add(new ApplicationUser
            {
                Id = "User2",
            });

            this.userList.Add(new ApplicationUser
            {
                Id = "User3",
            });

            var result = this.chatService.GetOtherUsers<UserViewModel>("User2");

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetChatsShouldWorkCorrectly()
        {
            this.chatList.Add(new Chat
            {
                Id = "Test1",
                Name = "TestName1",
                Type = ChatType.Room,
            });

            this.chatList.Add(new Chat
            {
                Id = "Test2",
                Name = "TestName2",
                Type = ChatType.Private,
            });

            this.chatList.Add(new Chat
            {
                Id = "Test3",
                Name = "TestName3",
                Type = ChatType.Room,
            });

            var result = this.chatService.GetChats<ChatViewModel>("User");

            Assert.Equal(2, result.Count());
        }
    }
}
