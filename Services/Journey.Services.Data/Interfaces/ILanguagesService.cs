namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Web.ViewModels.Administration.GameLanguage;

    public interface ILanguagesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        T GetById<T>(int id);

        Task UpdateAsync(int id, GameLanguageAdminInputModel input);

        public IEnumerable<T> GetAll<T>();
    }
}
