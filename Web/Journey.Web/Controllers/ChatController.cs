namespace Journey.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

/*        public IActionResult CreateRoom(string name)
        {

        }*/
    }
}
