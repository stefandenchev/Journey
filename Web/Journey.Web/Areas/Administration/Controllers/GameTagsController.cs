namespace Journey.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Administration.GameTags;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class GameTagsController : AdministrationController
    {
        private readonly IRepository<GameTag> gamesTagsRepository;
        private readonly IDeletableEntityRepository<Game> gamesRepository;
        private readonly IDeletableEntityRepository<Tag> tagsRepository;
        private readonly IGamesService gamesService;
        private readonly ITagsService tagsService;

        public GameTagsController(
            IRepository<GameTag> gamesTagsRepository,
            IDeletableEntityRepository<Game> gamesRepository,
            IDeletableEntityRepository<Tag> tagsRepository,
            IGamesService gamesService,
            ITagsService tagsService)
        {
            this.gamesTagsRepository = gamesTagsRepository;
            this.gamesRepository = gamesRepository;
            this.tagsRepository = tagsRepository;
            this.gamesService = gamesService;
            this.tagsService = tagsService;
        }

        // GET: Administration/GameTags
        public async Task<IActionResult> Index(string sortOrder)
        {
            this.ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;
            this.ViewBag.TagSortParam = sortOrder == "tag_asc" ? "tag_desc" : "tag_asc";

            var viewModel = new GameTagAdminViewModel
            {
                GamesTags = this.tagsService.GetAll<GamesTagsListViewModel>()
                .OrderBy(x => x.Game.Title),
            };

            if (sortOrder == "title_desc")
            {
                viewModel.GamesTags = this.tagsService.GetAll<GamesTagsListViewModel>()
                .OrderByDescending(x => x.Game.Title);
            }

            if (sortOrder == "tag_desc")
            {
                viewModel.GamesTags = this.tagsService.GetAll<GamesTagsListViewModel>()
                .OrderByDescending(x => x.Tag.Name);
            }

            if (sortOrder == "tag_asc")
            {
                viewModel.GamesTags = this.tagsService.GetAll<GamesTagsListViewModel>()
                .OrderBy(x => x.Tag.Name);
            }

            return this.View(viewModel);
        }

        // GET: Administration/GameTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var gameTag = await this.gamesTagsRepository.All()
                .Include(g => g.Game)
                .Include(g => g.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameTag == null)
            {
                return this.NotFound();
            }

            return this.View(gameTag);
        }

        // GET: Administration/GameTags/Create
        public IActionResult Create(GameTagAdminInputModel input)
        {
            input.TagsItems = this.tagsService.GetAllAsKeyValuePairs();
            input.GamesItems = this.gamesService.GetAllAsKeyValuePairs()
                .OrderBy(x => x.Value);

            this.ViewData["GameId"] = new SelectList(this.gamesRepository.All(), "Id", "Id");
            this.ViewData["TagId"] = new SelectList(this.tagsRepository.All(), "Id", "Id");
            return this.View(input);
        }

        // POST: Administration/GameTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,TagId,Id,CreatedOn_17114092,ModifiedOn_17114092")] GameTag gameTag)
        {
            if (this.ModelState.IsValid)
            {
                await this.gamesTagsRepository.AddAsync(gameTag);
                await this.gamesTagsRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["GameId"] = new SelectList(this.gamesRepository.All(), "Id", "Id", gameTag.GameId);
            this.ViewData["TagId"] = new SelectList(this.tagsRepository.All(), "Id", "Id", gameTag.TagId);
            return this.View(gameTag);
        }

        // GET: Administration/GameTags/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var inputModel = this.tagsService.GetById<GameTagAdminInputModel>(id);
            inputModel.TagsItems = this.tagsService.GetAllAsKeyValuePairs();
            inputModel.GamesItems = this.gamesService.GetAllAsKeyValuePairs()
                .OrderBy(x => x.Value);

            return this.View(inputModel);
        }

        // POST: Administration/GameTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,TagId,Id,CreatedOn_17114092,ModifiedOn_17114092")] GameTagAdminInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.tagsService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.Edit), new { id });
        }

        // GET: Administration/GameTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var gameTag = await this.gamesTagsRepository.All()
                .Include(g => g.Game)
                .Include(g => g.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameTag == null)
            {
                return this.NotFound();
            }

            return this.View(gameTag);
        }

        // POST: Administration/GameTags/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameTag = this.gamesTagsRepository.All().FirstOrDefault(x => x.Id == id);
            this.gamesTagsRepository.Delete(gameTag);
            await this.gamesTagsRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool GameTagExists(int id)
        {
            return this.gamesTagsRepository.All().Any(e => e.Id == id);
        }
    }
}
