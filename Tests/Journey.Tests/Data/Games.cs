namespace Journey.Tests.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Journey.Data.Models;
    using Journey.Web.ViewModels.Games.Create;
    using Microsoft.AspNetCore.Http;

    public static class Games
    {
        public static CreateGameInputModel GetGame()
        {
            var languagesList = new List<int> { 1 };
            var tagsList = new List<int> { 1 };

            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");

            var game = new CreateGameInputModel
            {
                Title = $"Game Test",
                Description = $"Game Description Test",
                PublisherId = 1,
                MininumRequirements = "min",
                RecommendedRequirements = "rec",
                Price = 9.99m,
                GenreId = 1,
                Languages = languagesList,
                Tags = tagsList,
                Images = new List<IFormFile>
                {
                    file,
                },
            };

            return game;
        }
    }
}
