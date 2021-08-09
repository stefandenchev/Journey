namespace Journey.Tests.Controllers
{
    using System.Linq;

    using FakeItEasy;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.Controllers;
    using Journey.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class NewsControllerTest
    {
        [Fact]
        public void GetAllShouldWorkCorrectly()
        {
            var fakeNewsService = A.Fake<INewsService>();

            A.CallTo(() => fakeNewsService.GetAllInList<NewsInListViewModel>(1, 3))
                    .Returns(A.CollectionOfFake<NewsInListViewModel>(10));

            var newsController = new NewsController(fakeNewsService);

            var result = newsController.All();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<NewsListViewModel>(viewResult.Model);
            Assert.Equal(10, model.News.Count());
        }

        [Fact]
        public void SinglePostShouldWorkCorrectly()
        {
            var fakeNewsService = A.Fake<INewsService>();

            A.CallTo(() => fakeNewsService.GetById<NewsPostViewModel>(1))
                .Returns(A.Fake<NewsPostViewModel>());

            var newsController = new NewsController(fakeNewsService);

            var result = newsController.Post(1);

            var post = fakeNewsService.GetById<NewsPostViewModel>(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<NewsPostViewModel>(viewResult.Model);
            Assert.NotNull(post);
        }

        [Fact]
        public void SinglePostShouldRedirectToNotFound()
        {
            var fakeNewsService = A.Fake<INewsService>();

            A.CallTo(() => fakeNewsService.GetById<NewsPostViewModel>(-1))
                .Returns(null);

            var newsController = new NewsController(fakeNewsService);

            var result = newsController.Post(-1);

            var post = fakeNewsService.GetById<NewsPostViewModel>(-1);

            var viewResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Null(post);
        }
    }
}
