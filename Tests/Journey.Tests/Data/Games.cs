namespace Journey.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Journey.Data.Models;

    public static class Games
    {
        public static List<Game> GetGames(int count)
        {
            var games = Enumerable
                .Range(1, count)
                .Select(i => new Game
                {
                    Id = i,
                    Title = $"Game {i}",
                    Description = $"Game Description {i}",
                    PublisherId = 1,
                    MininumRequirements = "min",
                    RecommendedRequirements = "rec",
                    Price = 9.99m,
                    CurrentPrice = 7.99m,
                    GenreId = 1,
                    IsOnSale = true,
                })
                .ToList();

            return games;
        }
    }
}
