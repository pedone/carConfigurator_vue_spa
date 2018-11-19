using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;

namespace KFZ_Konfigurator.Controllers
{
    public class ConfigurationController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                return View((await context.CarModels.ToListAsync()).Select(cur => new CarViewModel(cur)));
            }
        }

        public ActionResult Model(int id)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                WebApiApplication.ActiveConfiguration.CarModel = context.CarModels.First(cur => cur.Id == id);
            }
            return RedirectToAction("Index", "Engine");
        }
    }
}
