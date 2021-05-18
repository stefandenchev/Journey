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

    public class GamesService : IGamesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "jpeg", "png" };

        private readonly IDeletableEntityRepository<Game> gamesRepository;
        private readonly IRepository<GameLanguage> gameLanguagesRepository;
        private readonly IRepository<GameTag> gameTagsRepository;

        public GamesService(
            IDeletableEntityRepository<Game> gamesRepository,
            IRepository<GameLanguage> gameLanguagesRepository,
            IRepository<GameTag> gameTagsRepository)
        {
            this.gamesRepository = gamesRepository;
            this.gameLanguagesRepository = gameLanguagesRepository;
            this.gameTagsRepository = gameTagsRepository;
        }

        public async Task CreateAsync(CreateGameInputModel input, string imagePath)
        {
            var game = new Game
            {
                Title = input.Title,
                Price = input.Price,
                Description = input.Description,
                ReleaseDate = input.ReleaseDate,
                GenreId = input.GenreId,
                PublisherId = input.PublisherId,
                MininumRequirements = input.MininumRequirements,
                RecommendedRequirements = input.RecommendedRequirements,
            };

            foreach (var inputLanguage in input.Languages)
            {
                var language = this.gameLanguagesRepository
                    .All()
                    .FirstOrDefault(x => x.Language.Id == inputLanguage);

                game.Languages.Add(language);
            }

            foreach (var inputTag in input.Tags)
            {
                var tag = this.gameTagsRepository
                    .All()
                    .FirstOrDefault(x => x.Tag.Id == inputTag);

                game.Tags.Add(tag);
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

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 16)
        {
            var games = this.gamesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return games;
        }

        public IEnumerable<T> GetLatest<T>(int count = 12)
        {
            var games = this.gamesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.ReleaseDate)
                .Take(count)
                .To<T>()
                .ToList();

            return games;
        }

        public T GetById<T>(int id)
        {
            var game = this.gamesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return game;
        }

        public int GetCount()
        {
            return this.gamesRepository.All().Count();
        }
    }
}
