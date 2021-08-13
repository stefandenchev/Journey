namespace Journey.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class UsersService : IUsersService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "jpeg", "png", "PNG" };
        private readonly IDeletableEntityRepository<UserImage> imagesRepository;

        public UsersService(
            IDeletableEntityRepository<UserImage> imagesRepository)
        {
            this.imagesRepository = imagesRepository;
        }

        public async Task AddProfilePicture(IFormFile image, string userId, string imagePath)
        {
            var extentsion = Path.GetExtension(image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extentsion.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extentsion}");
            }

            var newImage = this.imagesRepository.All().Where(x => x.UserId == userId).FirstOrDefault();

            if (newImage != null)
            {
                this.imagesRepository.HardDelete(newImage);
            }

            newImage = new UserImage
            {
                UserId = userId,
                Extension = extentsion,
            };

            var path = $"{imagePath}/{newImage.Id}.{extentsion}";

            using Stream fileStream = new FileStream(path, FileMode.Create);
            await image.CopyToAsync(fileStream);

            await this.imagesRepository.AddAsync(newImage);
            await this.imagesRepository.SaveChangesAsync();
        }

        public T GetProfilePicture<T>(string userId)
        {
            var image = this.imagesRepository
                .All()
                .Where(x => x.UserId == userId)
                .To<T>()
                .FirstOrDefault();

            return image;
        }
    }
}
