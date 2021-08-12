namespace Journey.Tests.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Journey.Data.Models;
    using Journey.Web.ViewModels.Games.Create;
    using Microsoft.AspNetCore.Http;

    public static class Games
    {
        public static IEnumerable<Game> TenGames
               => Enumerable.Range(1, 10).Select(i => new Game
               {
                   Id = i,
               });

        public static IEnumerable<Game> ThreeGames
               => Enumerable.Range(1, 3).Select(i => new Game
               {
                   Id = i,
                   Title = $"Game Test {i}",
                   Description = $"Game Description Test {i}",
                   PublisherId = 1,
                   MininumRequirements = $"min {i}",
                   RecommendedRequirements = $"rec {i}",
                   Price = 9.99m,
                   GenreId = 1,
                   Drm = $"Steam",
                   ReleaseDate = new DateTime(2020, 10, 10),
               });

        public static Game OneGame
               => new()
               {
                   Id = 1,
               };

        public static CreateGameInputModel GetGameInModel()
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
                Drm = "Steam",
                ReleaseDate = new DateTime(2020, 10, 10),
            };

            return game;
        }
    }
}
