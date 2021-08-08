namespace Journey.Services
{
    using System.Threading.Tasks;

    public interface IGameStoreScraperService
    {
        Task PopulateDbAsync(int count);
    }
}
