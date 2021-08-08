namespace Journey.Tests.Data
{
    using Journey.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class Games
    {
        public static IEnumerable<Game> TwelveGames
            => Enumerable.Range(0, 12).Select(x => new Game
            {

            });
    }
}
