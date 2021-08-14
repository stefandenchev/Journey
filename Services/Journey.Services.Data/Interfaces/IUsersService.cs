namespace Journey.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IUsersService
    {
        Task AddProfilePicture(IFormFile image, string userId, string imagePath);

        T GetProfilePicture<T>(string userId);
    }
}
