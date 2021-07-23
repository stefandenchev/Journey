namespace Journey.Web
{
    using System;
    using System.Reflection;

    using Journey.Data;
    using Journey.Data.Common;
    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Data.Repositories;
    using Journey.Data.Seeding;
    using Journey.Services;
    using Journey.Services.Data;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;
    using Journey.Services.Messaging;
    using Journey.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddDistributedMemoryCache();

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<IGameStoreScraperService, GameStoreScraperService>();
            services.AddTransient<IGamesService, GamesService>();
            services.AddTransient<IGenresService, GenresService>();
            services.AddTransient<ILanguagesService, LanguagesService>();
            services.AddTransient<ITagsService, TagsService>();
            services.AddTransient<IPublishersService, PublishersService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<ICreditCardsService, CreditCardsService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IVotesService, VotesService>();
            services.AddTransient<INewsUrlGenerator, NewsUrlGenerator>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IForumService, ForumService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<IForumVotesService, ForumVotesService>();
            services.AddTransient<IWishlistService, WishlistService>();
            services.AddTransient<ICartService, CartService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseStatusCodePagesWithRedirects("/Home/NotFound");

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
