using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class ExteriorController : Controller
    {
        [Route("configuration/models/model-{id}/exterior", Name = Constants.Routes.Exterior)]
        public ActionResult Index()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var paints = context.Paints.ToList()
                    .Select(cur => new PaintViewModel(cur))
                    .ToList();
                return View(new ExteriorPageViewModel
                {
                    Paints = paints
                });
            }
        }
    }
}
