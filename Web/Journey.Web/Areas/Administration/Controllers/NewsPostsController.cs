namespace Journey.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Administration.NewsPosts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class NewsPostsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<NewsPost> dataRepository;
        private readonly INewsService newsService;

        public NewsPostsController(
            IDeletableEntityRepository<NewsPost> newsPosts,
            INewsService newsService)
        {
            this.dataRepository = newsPosts;
            this.newsService = newsService;
        }

        // GET: Administration/NewsPosts
        public IActionResult Index(string sortOrder)
        {
            this.ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;

            var viewModel = new NewsPostsListViewModel
            {
                News = this.newsService.GetAll<NewsPostsInListViewModel>()
                .OrderBy(x => x.Title),
            };

            if (sortOrder == "name_desc")
            {
                viewModel.News = this.newsService.GetAll<NewsPostsInListViewModel>()
                .OrderByDescending(x => x.Title);
            }

            return this.View(viewModel);
        }

        // GET: Administration/NewsPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var newsPost = await this.dataRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsPost == null)
            {
                return this.NotFound();
            }

            return this.View(newsPost);
        }

        // GET: Administration/NewsPosts/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/NewsPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,ShortContent,ImageOrVideoUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] NewsPost newsPost)
        {
            if (this.ModelState.IsValid)
            {
                await this.dataRepository.AddAsync(newsPost);
                await this.dataRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(newsPost);
        }

        // GET: Administration/NewsPosts/Edit/5
        public IActionResult Edit(int id)
        {
            var inputModel = this.newsService.GetById<NewsPostAdminInputModel>(id);

            return this.View(inputModel);
        }

        // POST: Administration/NewsPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Content,ShortContent,ImageOrVideoUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] NewsPost newsPost)
        {
            if (id != newsPost.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(newsPost);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.NewsPostExists(newsPost.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(newsPost);
        }

        // GET: Administration/NewsPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var newsPost = await this.dataRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsPost == null)
            {
                return this.NotFound();
            }

            return this.View(newsPost);
        }

        // POST: Administration/NewsPosts/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsPost = await this.dataRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.dataRepository.Delete(newsPost);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool NewsPostExists(int id)
        {
            return this.dataRepository.All().Any(e => e.Id == id);
        }
    }
}
