namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Forum.Categories;
    using Moq;
    using Xunit;

    public class CategoriesServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Category>> categoriesRepo;
        private readonly List<Category> categoriesList;
        private readonly CategoriesService catService;

        public CategoriesServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.categoriesRepo = new Mock<IDeletableEntityRepository<Category>>();
            this.categoriesList = new List<Category>();
            this.catService = new CategoriesService(this.categoriesRepo.Object);

            this.categoriesRepo.Setup(x => x.All()).Returns(this.categoriesList.AsQueryable());
            this.categoriesRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback(
                (Category item) => this.categoriesList.Add(item));
            this.categoriesRepo.Setup(x => x.Delete(It.IsAny<Category>())).Callback(
                (Category item) => this.categoriesList.Remove(item));
        }

        [Fact]
        public void GetAllShouldReturnAllCategories()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.categoriesRepo.Object.AddAsync(new Category
            {
                Id = 1,
                Title = "Category Test 1",
            });

            this.categoriesRepo.Object.AddAsync(new Category
            {
                Id = 2,
                Title = "Category Test 2",
            });

            var result = this.catService.GetAll<CategoriesListViewModel>();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetByIdShouldReturnCorrectCategory()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.categoriesRepo.Object.AddAsync(new Category
            {
                Id = 1,
                Title = "Category Test 1",
            });

            this.categoriesRepo.Object.AddAsync(new Category
            {
                Id = 2,
                Title = "Category Test 2",
            });

            var result = this.catService.GetById<CategoryViewModel>(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public void GetByTitleShouldReturnCorrectCategory()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.categoriesRepo.Object.AddAsync(new Category
            {
                Id = 1,
                Title = "Category Test 1",
            });

            this.categoriesRepo.Object.AddAsync(new Category
            {
                Id = 2,
                Title = "Category Test 2",
            });

            var result = this.catService.GetByTitle<CategoryViewModel>("Category Test 2");

            Assert.NotNull(result);
            Assert.Equal("Category Test 2", result.Title);
        }
    }
}
