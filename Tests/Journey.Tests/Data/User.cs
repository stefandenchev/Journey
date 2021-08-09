namespace Journey.Tests.Data
{
    using Journey.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Moq;

    public class User
    {
        public static Mock<UserManager<ApplicationUser>> New
            => new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
    }
}
