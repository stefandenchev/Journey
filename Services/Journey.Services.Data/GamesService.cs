namespace Journey.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Games.Create;
    using Journey.Web.ViewModels.Games.Edit;

    public class GamesService : IGamesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "jpeg", "png" };

        private readonly IDeletableEntityRepository<Game> gamesRepository;
        private readonly IDeletableEntityRepository<Language> languagesRepository;
        private readonly IDeletableEntityRepository<Tag> tagsRepository;

        public GamesService(
            IDeletableEntityRepository<Game> gamesRepository,
            IDeletableEntityRepository<Language> languagesRepository,
            IDeletableEntityRepository<Tag> tagsRepository)
        {
            this.gamesRepository = gamesRepository;
            this.languagesRepository = languagesRepository;
            this.tagsRepository = tagsRepository;
        }

        public async Task CreateAsync(CreateGameInputModel input, string imagePath)
        {
            var game = new Game
            {
                Title = input.Title,
                Price = input.Price,
                CurrentPrice = input.Price,
                Description = input.Description,
                ReleaseDate = input.ReleaseDate,
                GenreId = input.GenreId,
                PublisherId = input.PublisherId,
                MininumRequirements = input.MininumRequirements,
                RecommendedRequirements = input.RecommendedRequirements,
            };

            foreach (var inputLanguage in input.Languages)
            {
                var language = this.languagesRepository
                    .All()
                    .FirstOrDefault(x => x.Id == inputLanguage);

                game.Languages.Add(new GameLanguage
                {
                    Language = language,
                });
            }

            foreach (var inputTag in input.Tags)
            {
                var tag = this.tagsRepository
                    .All()
                    .FirstOrDefault(x => x.Id == inputTag);

                game.Tags.Add(new GameTag
                {
                    Tag = tag,
                });
            }

            foreach (var image in input.Images)
            {
                var extentsion = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extentsion.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extentsion}");
                }

                var dbImage = new Image
                {
                    UploadName = image.FileName,
                    Extension = extentsion,
                };

                game.Images.Add(dbImage);

                var path = $"{imagePath}/{dbImage.Id}.{extentsion}";

                using Stream fileStream = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.gamesRepository.AddAsync(game);
            await this.gamesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllInList<T>(int page, int itemsPerPage = 16)
        {
            var games = this.gamesRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return games;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var games = this.gamesRepository
                .All()
                .To<T>()
                .ToList();

            return games;
        }

        public IEnumerable<T> GetLatest<T>(int count = 12)
        {
            var games = this.gamesRepository
                .All()
                .OrderByDescending(x => x.ReleaseDate)
                .Take(count)
                .To<T>()
                .ToList();

            return games;
        }

        public T GetById<T>(int id)
        {
            var game = this.gamesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return game;
        }

        public int GetCount()
        {
            return this.gamesRepository
                .All()
                .Count();
        }

        public async Task UpdateAsync(int id, EditGameInputModel input)
        {
            var game = this.gamesRepository.All().FirstOrDefault(x => x.Id == id);
            game.Title = input.Title;
            game.Price = input.Price;
            game.Description = input.Description;
            game.Drm = input.Drm;
            game.MininumRequirements = input.MininumRequirements;
            game.RecommendedRequirements = input.RecommendedRequirements;
            game.GenreId = input.GenreId;
            game.PublisherId = input.PublisherId;

            game.IsOnSale = input.IsOnSale;
            game.SalePercentage = input.SalePercentage;

            if (game.IsOnSale)
            {
                game.CurrentPrice = game.Price - (game.Price * input.SalePercentage / 100);
                if (game.CurrentPrice.ToString("f2").EndsWith("0"))
                {
                    game.CurrentPrice -= 0.01m;
                }
            }
            else
            {
                game.SalePercentage = 0;
                game.CurrentPrice = game.Price;
            }

            await this.gamesRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.gamesRepository
                .AllAsNoTracking()
                .Select(x => new
            {
                x.Id,
                x.Title,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Title));
        }

        public IEnumerable<T> GetCurated<T>(int count = 12)
        {
            var idList = new List<int>
            {
                472, 555, 569, 575, 578, 579,
                580, 581, 583, 585, 586, 587,
            };

            var games = this.gamesRepository
                .AllAsNoTracking()
                .Where(t => idList.Contains(t.Id))
                .Take(count)
                .To<T>()
                .ToList();

            return games;
        }

        public IEnumerable<T> GetGamesFromOrder<T>(IEnumerable<int> ids)
        {
            var gamesToReturn = this.gamesRepository
                .AllAsNoTracking()
                .Where(g => ids.Contains(g.Id))
                .To<T>()
                .ToList();

            return gamesToReturn;
        }
    }
}
