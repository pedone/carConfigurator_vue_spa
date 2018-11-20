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
    public class PaintController : Controller
    {
        [Route("configuration/models/model-{id}/paint", Name = Constants.Routes.Paints)]
        public ActionResult Index()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                return View(context.Paints.ToList().Select(cur => new PaintViewModel(cur)).ToList());
            }
        }
    }
}
