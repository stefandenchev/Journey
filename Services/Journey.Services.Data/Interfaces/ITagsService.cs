namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Web.ViewModels.Administration.GameTags;

    public interface ITagsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        public IEnumerable<T> GetAll<T>();

        Task UpdateAsync(int id, GameTagAdminInputModel input);

        T GetById<T>(int id);
    }
}
