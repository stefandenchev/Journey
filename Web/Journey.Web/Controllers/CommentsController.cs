namespace Journey.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommentsController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
