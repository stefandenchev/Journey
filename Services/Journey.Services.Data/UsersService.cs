namespace Journey.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
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

        public string GetProfilePicturePath(string userId)
        {
            var image = this.imagesRepository
                .All()
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            if (image != null)
            {
                return "/images/users/" + image.Id + "." + image.Extension;
            }
            else
            {
                return "/images/users/default-user.png";
            }
        }

        public string GetProfileRank(int games)
        {
            var rank = string.Empty;
            if (games >= 5)
            {
                rank = "Bronze";
            }
            else if (games >= 25)
            {
                rank = "Silver";
            }
            else if (games >= 50)
            {
                rank = "Gold";
            }

            return rank;
        }

        public string GetProfileBadge(int games)
        {
            var badge = string.Empty;
            if (games >= 1 && games < 5)
            {
                badge = "/images/badges/games-1.png";
            }
            else if (games >= 5 && games < 10)
            {
                badge = "/images/badges/games-5.png";
            }
            else if (games >= 10 && games < 25)
            {
                badge = "/images/badges/games-10.png";
            }
            else if (games >= 25 && games < 50)
            {
                badge = "/images/badges/games-25.png";
            }
            else if (games >= 50 && games < 100)
            {
                badge = "/images/badges/games-50.png";
            }
            else if (games >= 100)
            {
                badge = "/images/badges/games-100.png";
            }

            return badge;
        }
    }
}
