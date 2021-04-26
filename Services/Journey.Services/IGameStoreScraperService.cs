namespace Journey.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IGameStoreScraperService
    {
        Task PopulateDbAsync(int count);
    }
}
