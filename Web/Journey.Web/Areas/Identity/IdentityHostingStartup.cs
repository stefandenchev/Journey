using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Journey.Web.Areas.Identity.IdentityHostingStartup))]

namespace Journey.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}