namespace Journey.Tests.Routing
{
    using Journey.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class SearchControllerTest
    {
        [Fact]
        public void ResultsRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Search/Results")
                .To<SearchController>(c => c.Results(null, null));

        [Fact]
        public void GenreRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Search/Genre")
                .To<SearchController>(c => c.Genre(null, null));

        [Fact]
        public void PublisherRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Search/Publisher")
                .To<SearchController>(c => c.Publisher(null, null));

        [Fact]
        public void SalesRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Search/Sales")
                .To<SearchController>(c => c.Sales(null));
    }
}
