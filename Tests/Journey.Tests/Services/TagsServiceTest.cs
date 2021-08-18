namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Moq;
    using Xunit;

    public class TagsServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Tag>> tagsRepo;
        private readonly Mock<IRepository<GameTag>> gameTagsRepo;
        private readonly List<Tag> tagsList;
        private readonly TagsService service;

        public TagsServiceTest()
        {
            this.tagsRepo = new Mock<IDeletableEntityRepository<Tag>>();
            this.gameTagsRepo = new Mock<IRepository<GameTag>>();

            this.tagsList = new List<Tag>();
            this.service = new TagsService(this.tagsRepo.Object, this.gameTagsRepo.Object);

            this.tagsRepo.Setup(x => x.AllAsNoTracking()).Returns(this.tagsList.AsQueryable());
            this.tagsRepo.Setup(x => x.AddAsync(It.IsAny<Tag>())).Callback(
                (Tag item) => this.tagsList.Add(item));
        }

        [Fact]
        public void GetAllGenresAsKeyValuePairsWorksCorrectly()
        {
            this.tagsRepo.Object.AddAsync(new()
            {
                Id = 1,
                Name = "Singleplayer",
            });

            this.tagsRepo.Object.AddAsync(new()
            {
                Id = 2,
                Name = "Multiplayer",
            });

            this.tagsRepo.Object.AddAsync(new()
            {
                Id = 3,
                Name = "VR Support",
            });

            var result = this.service.GetAllAsKeyValuePairs();

            Assert.Equal(3, result.Count());
        }
    }
}