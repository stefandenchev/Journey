namespace Journey.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Models;

    public class DefaultUserImageSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.UserImages.Any())
            {
                return;
            }

            var image = new UserImage
            {
                UploadName = "default-user",
                Extension = "png",
            };

            await dbContext.UserImages.AddAsync(image);
        }
    }
}
