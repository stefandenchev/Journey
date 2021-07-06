namespace Journey.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Administration.GameLanguage;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class GameLanguagesController : AdministrationController
    {
        private readonly IRepository<GameLanguage> gamesLanguagesRepository;
        private readonly IDeletableEntityRepository<Game> gamesRepository;
        private readonly IDeletableEntityRepository<Language> languagesRepository;
        private readonly IGamesService gamesService;
        private readonly ILanguagesService languagesService;

        public GameLanguagesController(
            IRepository<GameLanguage> gamesLanguagesRepository,
            IDeletableEntityRepository<Game> gamesRepository,
            IDeletableEntityRepository<Language> languagesRepository,
            IGamesService gamesService,
            ILanguagesService languagesService)
        {
            this.gamesLanguagesRepository = gamesLanguagesRepository;
            this.gamesRepository = gamesRepository;
            this.languagesRepository = languagesRepository;
            this.gamesService = gamesService;
            this.languagesService = languagesService;
        }

        // GET: Administration/GameLanguages
        public IActionResult Index(string sortOrder)
        {
            this.ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;
            this.ViewBag.LanguageSortParam = sortOrder == "lang_asc" ? "lang_desc" : "lang_asc";

            var viewModel = new GameLanguageAdminViewModel
            {
                GamesLanguages = this.languagesService.GetAll<GamesLanguagesListViewModel>()
                .OrderBy(x => x.Game.Title),
            };

            if (sortOrder == "title_desc")
            {
                viewModel.GamesLanguages = this.languagesService.GetAll<GamesLanguagesListViewModel>()
                .OrderByDescending(x => x.Game.Title);
            }

            if (sortOrder == "lang_desc")
            {
                viewModel.GamesLanguages = this.languagesService.GetAll<GamesLanguagesListViewModel>()
                .OrderByDescending(x => x.Language.Name);
            }

            if (sortOrder == "lang_asc")
            {
                viewModel.GamesLanguages = this.languagesService.GetAll<GamesLanguagesListViewModel>()
                .OrderBy(x => x.Language.Name);
            }

            return this.View(viewModel);
        }

        // GET: Administration/GameLanguages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var gameLanguage = await this.gamesLanguagesRepository.All()
                .Include(g => g.Game)
                .Include(g => g.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameLanguage == null)
            {
                return this.NotFound();
            }

            return this.View(gameLanguage);
        }

        // GET: Administration/GameLanguages/Create
        public IActionResult Create(GameLanguageAdminInputModel input)
        {
            input.LanguagesItems = this.languagesService.GetAllAsKeyValuePairs();
            input.GamesItems = this.gamesService.GetAllAsKeyValuePairs()
                .OrderBy(x => x.Value);

            this.ViewData["GameId"] = new SelectList(this.gamesRepository.All(), "Id", "Id");
            this.ViewData["LanguageId"] = new SelectList(this.languagesRepository.All(), "Id", "Id");
            return this.View(input);
        }

        // POST: Administration/GameLanguages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,LanguageId,Id,CreatedOn,ModifiedOn")] GameLanguage gameLanguage)
        {
            if (this.ModelState.IsValid)
            {
                await this.gamesLanguagesRepository.AddAsync(gameLanguage);
                await this.gamesLanguagesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["GameId"] = new SelectList(this.gamesRepository.All(), "Id", "Id", gameLanguage.GameId);
            this.ViewData["LanguageId"] = new SelectList(this.languagesRepository.All(), "Id", "Id", gameLanguage.LanguageId);
            return this.View(gameLanguage);
        }

        // GET: Administration/GameLanguages/Edit/5
        public IActionResult Edit(int id)
        {
            var inputModel = this.languagesService.GetById<GameLanguageAdminInputModel>(id);
            inputModel.LanguagesItems = this.languagesService.GetAllAsKeyValuePairs();
            inputModel.GamesItems = this.gamesService.GetAllAsKeyValuePairs()
                .OrderBy(x => x.Value);

            return this.View(inputModel);
        }

        // POST: Administration/GameLanguages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,LanguageId,Id,CreatedOn,ModifiedOn")] GameLanguageAdminInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.languagesService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.Edit), new { id });
        }

        // GET: Administration/GameLanguages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var gameLanguage = await this.gamesLanguagesRepository.All()
                .Include(g => g.Game)
                .Include(g => g.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameLanguage == null)
            {
                return this.NotFound();
            }

            return this.View(gameLanguage);
        }

        // POST: Administration/GameLanguages/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameLanguage = this.gamesLanguagesRepository.All().FirstOrDefault(x => x.Id == id);
            this.gamesLanguagesRepository.Delete(gameLanguage);
            await this.gamesLanguagesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool GameLanguageExists(int id)
        {
            return this.gamesLanguagesRepository.All().Any(e => e.Id == id);
        }
    }
}
