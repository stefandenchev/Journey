namespace Journey.Web.Controllers
{
    using System.Linq;

    using Journey.Data;
    using Journey.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class GameController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public GameController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult GameDetails(int id)
        {
            var viewModel = new GameViewModel();
            viewModel.Title = this.dbContext.Games.Where(x => x.Id == id)
                .FirstOrDefault().Title;
            viewModel.Price = this.dbContext.Games.Where(x => x.Id == id)
                .FirstOrDefault().Price;
            viewModel.Description = this.dbContext.Games.Where(x => x.Id == id)
                .FirstOrDefault().Description;
            viewModel.ReleaseDate = this.dbContext.Games.Where(x => x.Id == id)
                .FirstOrDefault().ReleaseDate;

            return this.View(viewModel);
        }
    }
}
