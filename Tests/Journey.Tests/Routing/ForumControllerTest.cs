namespace Journey.Tests.Routing
{
    using Journey.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ForumControllerTest
    {
        [Fact]
        public void ForumRouteShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Forum")
              .To<ForumController>(c => c.Index());
    }
}
