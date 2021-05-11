﻿namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Web.ViewModels.Games.Create;

    public interface IGamesService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        IEnumerable<T> GetLatest<T>(int count = 12);

        int GetCount();

        T GetById<T>(int id);

        Task CreateAsync(CreateGameInputModel input, string imagePath);
    }
}
