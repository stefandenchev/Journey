namespace Journey.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using AngleSharp;
    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Models;

    // ACTUAL ABOMINATION, SCROLL AT YOUR OWN PERIL
    public class GameStoreScraperService : IGameStoreScraperService
    {
        private readonly IConfiguration config;
        private readonly IBrowsingContext context;

        private readonly IDeletableEntityRepository<Genre> genresRepository;
        private readonly IDeletableEntityRepository<Tag> tagsRepository;
        private readonly IDeletableEntityRepository<Language> languagesRepository;
        private readonly IDeletableEntityRepository<Publisher> publishersRepository;
        private readonly IDeletableEntityRepository<Game> gamesRepository;
        private readonly IRepository<GameLanguage> gamesLanguagesRepository;
        private readonly IRepository<GameTag> gamesTagsRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IRepository<GameGenre> gamesGenresRepository;

        public GameStoreScraperService(
            IDeletableEntityRepository<Genre> genresRepository,
            IDeletableEntityRepository<Tag> tagsRepository,
            IDeletableEntityRepository<Language> languagesRepository,
            IDeletableEntityRepository<Publisher> publishersRepository,
            IDeletableEntityRepository<Game> gamesRepository,
            IRepository<GameLanguage> gamesLanguagesRepository,
            IRepository<GameTag> gamesTagsRepository,
            IRepository<Image> imagesRepository,
            IRepository<GameGenre> gamesGenresRepository)
        {
            this.genresRepository = genresRepository;
            this.tagsRepository = tagsRepository;
            this.languagesRepository = languagesRepository;
            this.publishersRepository = publishersRepository;
            this.gamesRepository = gamesRepository;
            this.gamesLanguagesRepository = gamesLanguagesRepository;
            this.gamesTagsRepository = gamesTagsRepository;
            this.imagesRepository = imagesRepository;
            this.gamesGenresRepository = gamesGenresRepository;

            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(this.config);
        }

        public async Task PopulateDbAsync(int count)
        {
            var concurrentBag = new ConcurrentBag<GameStoreDto>();

            /*Parallel.For(6700, count, (i) =>
                        {
                            try
                            {
                                var game = this.GetGame(i);
                                concurrentBag.Add(game);
                            }
                            catch
                            {
                            }
             });*/

            for (int i = 1; i < count; i++)
            {
                try
                {
                    var game = this.GetGame(i);
                    concurrentBag.Add(game);
                }
                catch
                {
                }
            }

            foreach (var game in concurrentBag)
            {
                var publisherId = await this.GetOrCreatePublisherAsync(game.Publisher);

                var gameExists = this.gamesRepository
                    .AllAsNoTracking()
                    .Any(x => x.Title == game.Title);

                if (gameExists)
                {
                    continue;
                }

                var newGame = new Game()
                {
                    Title = game.Title,
                    Description = game.Description,
                    PublisherId = publisherId,
                    ReleaseDate = DateTime.ParseExact(game.ReleaseDate, "M/d/yyyy", CultureInfo.InvariantCulture),
                    Drm = game.Drm,
                    MininumRequirements = game.MininumRequirements,
                    RecommendedRequirements = game.RecommendedRequirements,
                    Price = game.Price,
                };

                await this.gamesRepository.AddAsync(newGame);
                await this.gamesRepository.SaveChangesAsync();

                foreach (var currentImage in game.Images)
                {
                    var image = new Image
                    {
                        GameId = newGame.Id,
                        OriginalUrl = currentImage.OriginalUrl,
                    };

                    await this.imagesRepository.AddAsync(image);
                    await this.imagesRepository.SaveChangesAsync();
                }

                foreach (var item in game.Genres)
                {
                    var genreId = await this.GetOrGenreAsync(item.Name);

                    var gameGenre = new GameGenre
                    {
                        GameId = newGame.Id,
                        GenreId = genreId,
                    };

                    await this.gamesGenresRepository.AddAsync(gameGenre);
                    await this.gamesGenresRepository.SaveChangesAsync();
                }

                foreach (var item in game.Languages)
                {
                    var languageId = await this.GetOrCreateLanguageAsync(item);

                    var gameLanguage = new GameLanguage
                    {
                        GameId = newGame.Id,
                        LanguageId = languageId,
                    };

                    await this.gamesLanguagesRepository.AddAsync(gameLanguage);
                    await this.gamesLanguagesRepository.SaveChangesAsync();
                }

                foreach (var item in game.Tags)
                {
                    var tagId = await this.GetOrCreateTagAsync(item);

                    var gameTag = new GameTag
                    {
                        GameId = newGame.Id,
                        TagId = tagId,
                    };

                    await this.gamesTagsRepository.AddAsync(gameTag);
                    await this.gamesTagsRepository.SaveChangesAsync();
                }
            }
        }

        private async Task<string> GetOrCreateImageAsync(string imageUrl)
        {
            var image = this.imagesRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.OriginalUrl == imageUrl);

            if (image == null)
            {
                image = new Image
                {
                    OriginalUrl = imageUrl,
                };

                await this.imagesRepository.AddAsync(image);
                await this.imagesRepository.SaveChangesAsync();
            }

            return image.OriginalUrl;
        }

        private async Task<int> GetOrCreateLanguageAsync(string languageName)
        {
            var language = this.languagesRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == languageName);

            if (language == null)
            {
                language = new Language
                {
                    Name = languageName,
                };

                await this.languagesRepository.AddAsync(language);
                await this.languagesRepository.SaveChangesAsync();
            }

            return language.Id;
        }

        private async Task<int> GetOrCreateTagAsync(string tagName)
        {
            var tag = this.tagsRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == tagName);

            if (tag == null)
            {
                tag = new Tag
                {
                    Name = tagName,
                };

                await this.tagsRepository.AddAsync(tag);
                await this.tagsRepository.SaveChangesAsync();
            }

            return tag.Id;
        }

        private async Task<int> GetOrCreatePublisherAsync(string publisherName)
        {
            var publisher = this.publishersRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == publisherName);

            if (publisher == null)
            {
                publisher = new Publisher()
                {
                    Name = publisherName,
                };

                await this.publishersRepository.AddAsync(publisher);
                await this.publishersRepository.SaveChangesAsync();
            }

            return publisher.Id;
        }

        private async Task<int> GetOrGenreAsync(string genreName)
        {
            var genre = this.genresRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == genreName);

            if (genre == null)
            {
                genre = new Genre
                {
                    Name = genreName,
                };

                await this.genresRepository.AddAsync(genre);
                await this.genresRepository.SaveChangesAsync();
            }

            return genre.Id;
        }

        private GameStoreDto GetGame(int id)
        {
            var document = this.context
                .OpenAsync($"https://www.wingamestore.com/product/{id}")
                .GetAwaiter()
                .GetResult();

            if (document.DocumentElement.OuterHtml.Contains("Currently Not Being Sold")
                || document.DocumentElement.OuterHtml.Contains("Product Discontinued")
                || document.DocumentElement.OuterHtml.Contains("Featured & Specials")
                || document.DocumentElement.InnerHtml.Contains(".local-player > source")
                || document.DocumentElement.InnerHtml.Contains("Partial Nudity")
                || document.DocumentElement.InnerHtml.Contains("Sexual Themes")
                || document.DocumentElement.OuterHtml.Contains("DLC"))
            {
                throw new InvalidOperationException();
            }

            var game = new GameStoreDto();

            game.Images = new List<Image>();
            game.Genres = new List<Genre>();

            // NAME
            var name = document.QuerySelectorAll("#core-guts-title");
            game.Title = name[0].TextContent.Replace("™", string.Empty);

            var detailElements = document.QuerySelectorAll("#detail-bits > table > tbody > tr");

            if (detailElements.Length != 6)
            {
                throw new InvalidOperationException();
            }

            // DATE
            // var dateName = detailElements[0].TextContent.Substring(0, 12);
            var actualDate = detailElements[0].TextContent[12..];
            game.ReleaseDate = actualDate;

            // GENRE
            var genreRow = detailElements[1].TextContent;

            string actualGenre = null;
            string[] actualGenres = null;

            if (genreRow.Contains(","))
            {
                actualGenres = detailElements[1].TextContent[6..].Split(", ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var currentGenre in actualGenres)
                {
                    Genre genre = new Genre
                    {
                        Name = currentGenre,
                    };

                    game.Genres.Add(genre);
                }
            }
            else
            {
                actualGenre = detailElements[1].TextContent[5..];
                Genre genre = new Genre
                {
                    Name = actualGenre,
                };
                game.Genres.Add(genre);
            }

            // PUBLISHER
            var actualPublisher = detailElements[2].TextContent[9..];
            if (actualPublisher == string.Empty)
            {
                throw new InvalidOperationException();
            }

            game.Publisher = actualPublisher;

            // DRM
            var actualDrm = detailElements[3].TextContent[3..];
            game.Drm = actualDrm;

            // INCLUDES
            static IEnumerable<string> SplitInlcludes(string input)
            {
                return Regex.Split(input, @"(Single-player|Multi-player|Cross-Platform Multiplayer|Full Controller Support|Online PvP|Partial Controller Support|Co-op)").Where(str => !string.IsNullOrEmpty(str));
            }

            var inclElements = detailElements[4].QuerySelectorAll("tr > td");

            foreach (var item in inclElements)
            {
                var list = string.Join(Environment.NewLine, SplitInlcludes(item.TextContent)).Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                foreach (var tag in list)
                {
                    game.Tags.Add(tag);
                }
            }

            var actualLanguages = detailElements[5].TextContent[9..].Split();

            foreach (var lang in actualLanguages)
            {
                if (lang == string.Empty || lang == "-"
                    || lang == "Portuguese-Braazil" || lang == "Russina"
                    || lang.Contains("Traditional") || lang.Contains("all")
                    || lang.Contains("rights") || lang.Contains("reserved")
                    || lang == "Simplified")
                {
                    continue;
                }

                game.Languages.Add(lang);
            }

            // PRICE
            var priceResult = document.QuerySelectorAll("#price-bar > .price");
            var priceWithCurrency = priceResult[0].TextContent;
            string priceWithoutCurrency = priceWithCurrency.Substring(1);
            decimal finalPrice = decimal.Parse(priceWithoutCurrency);
            game.Price = finalPrice;

            // DESCRIPTION
            var description = document.QuerySelectorAll(".section.txtlists")
                .Select(x => x.TextContent)
                .ToList();
            StringBuilder sb = new StringBuilder();

            foreach (var item in description)
            {
                sb.AppendLine(item);
            }

            game.Description = sb.ToString().TrimEnd();

            // REQUIREMENTS
            var requirements = document.QuerySelectorAll(".valign-t > .split > .side");

            game.MininumRequirements = requirements[0].TextContent[8..];
            game.RecommendedRequirements = requirements[1].TextContent[12..];

            // URL
            game.OriginalUrl = $"https://www.wingamestore.com/product/{id}";

            // MAIN IMAGE
            var main = document.QuerySelector("#detail-badge > div.boxhole.img16x9 > img").GetAttribute("src");

            Image image = new Image
            {
                OriginalUrl = "https://www.wingamestore.com" + main,
            };

            game.Images.Add(image);

            // IMAGES
            var images = document.QuerySelectorAll("#roundabout > li > a > img");

            string imageResult = null;

            foreach (var item in images)
            {
                imageResult = item.GetAttribute("src");

                Image image2 = new Image
                {
                    OriginalUrl = "https://www.wingamestore.com" + imageResult,
                };

                game.Images.Add(image2);
            }

            return game;
        }
    }
}
