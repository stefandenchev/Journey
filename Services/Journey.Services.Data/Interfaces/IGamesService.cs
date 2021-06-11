namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Web.ViewModels.Games.Create;
    using Journey.Web.ViewModels.Games.Edit;

    public interface IGamesService
    {
        IEnumerable<T> GetAllInList<T>(int page, int itemsPerPage = 16);

        IEnumerable<T> GetAll<T>();

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        IEnumerable<T> GetLatest<T>(int count = 12);

        int GetCount();

        T GetById<T>(int id);

        Task CreateAsync(CreateGameInputModel input, string imagePath);

        Task UpdateAsync(int id, EditGameInputModel input);

        IEnumerable<T> GetCurated<T>(int count = 12);
    }
}
