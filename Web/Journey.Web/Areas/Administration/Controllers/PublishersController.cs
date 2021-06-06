namespace Journey.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Administration.Publishers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class PublishersController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Publisher> dataRepository;
        private readonly IPublishersService publishersService;

        public PublishersController(
            IDeletableEntityRepository<Publisher> dataRepository,
            IPublishersService publishersService)
        {
            this.dataRepository = dataRepository;
            this.publishersService = publishersService;
        }

        // GET: Administration/Publishers
        public IActionResult Index(string sortOrder)
        {
            this.ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;

            var viewModel = new PublishersListViewModel
            {
                Publishers = this.publishersService.GetAll<PublishersInListViewModel>()
                .OrderBy(x => x.Name),
            };

            if (sortOrder == "name_desc")
            {
                viewModel.Publishers = this.publishersService.GetAll<PublishersInListViewModel>()
                .OrderByDescending(x => x.Name);
            }

            return this.View(viewModel);
        }

        // GET: Administration/Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var publisher = await this.dataRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publisher == null)
            {
                return this.NotFound();
            }

            return this.View(publisher);
        }

        // GET: Administration/Publishers/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn_17114092,ModifiedOn_17114092")] Publisher publisher)
        {
            if (this.ModelState.IsValid)
            {
                await this.dataRepository.AddAsync(publisher);
                await this.dataRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(publisher);
        }

        // GET: Administration/Publishers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var inputModel = this.publishersService.GetById<PublisherAdminInputModel>(id);

            return this.View(inputModel);
        }

        // POST: Administration/Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn_17114092,ModifiedOn_17114092")] Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(publisher);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.PublisherExists(publisher.Id))
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

            return this.View(publisher);
        }

        // GET: Administration/Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var publisher = await this.dataRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publisher == null)
            {
                return this.NotFound();
            }

            return this.View(publisher);
        }

        // POST: Administration/Publishers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await this.dataRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.dataRepository.Delete(publisher);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool PublisherExists(int id)
        {
            return this.dataRepository.All().Any(e => e.Id == id);
        }
    }
}
