using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class HomeController : Controller
    {
        [Route("", Name = Constants.Routes.Home)]
        public ActionResult Index()
        {
            return View();
        }

    }
}
