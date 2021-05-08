namespace Journey.Web.ViewModels.Games.Create
{
    using System.ComponentModel.DataAnnotations;

    public class GameGenreInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
