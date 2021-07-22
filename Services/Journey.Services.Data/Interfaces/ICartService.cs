namespace Journey.Services.Data.Interfaces
{
    public interface ICartService
    {
        bool CheckCart(string userId, int gameId);
    }
}
