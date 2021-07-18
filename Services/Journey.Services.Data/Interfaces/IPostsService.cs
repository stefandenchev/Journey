namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task<int> CreateAsync(string title, string content, int categoryId, string userId);

        T GetById<T>(int id);

        int GetCountByCategoryId(int categoryId);

        int GetCount(int categoryId);

        IEnumerable<T> GetAllInList<T>(int categoryId, int page, int itemsPerPage = 12);
    }
}
