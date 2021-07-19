using System.ComponentModel.DataAnnotations;

namespace Journey.Web.ViewModels.Votes
{
    public class GameVoteInputModel
    {
        public int GameId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}
