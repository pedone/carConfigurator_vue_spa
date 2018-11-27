using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class OrdersController : Controller
    {
        [Route("orders", Name = Constants.Routes.OrderOverview)]
        public ActionResult Index()
        {
            return View();
        }
    }
}