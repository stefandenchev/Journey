﻿namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Data.Models;
    using Journey.Web.ViewModels.Games.Create;
    using Journey.Web.ViewModels.Games.Edit;

    public interface IGamesService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 16);

        IEnumerable<T> GetLatest<T>(int count = 12);

        int GetCount();

        T GetById<T>(int id);

        Task CreateAsync(CreateGameInputModel input, string imagePath);

        Task UpdateAsync(int id, EditGameInputModel input);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
