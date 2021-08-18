namespace Journey.Tests.Routing
{
    using Journey.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ChatControllerTest
    {
        [Fact]
        public void ChatRouteShouldBeMapped()
            => MyRouting
        .Configuration()
        .ShouldMap("/Chat")
        .To<ChatController>(c => c.Index());

        [Fact]
        public void PrivateRouteShouldBeMapped()
           => MyRouting
        .Configuration()
        .ShouldMap("/Chat/Private")
        .To<ChatController>(c => c.Private());

        [Fact]
        public void FindRouteShouldBeMapped()
          => MyRouting
        .Configuration()
        .ShouldMap("/Chat/Find")
        .To<ChatController>(c => c.Find());

        [Fact]
        public void SingleChatRouteShouldBeMapped()
          => MyRouting
        .Configuration()
        .ShouldMap("/Chat/Room/Test123")
        .To<ChatController>(c => c.Chat("Test123"));

    }
}
