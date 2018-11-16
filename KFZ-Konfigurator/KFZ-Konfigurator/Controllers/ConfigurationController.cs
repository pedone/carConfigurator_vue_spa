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
                return View((await context.Cars.ToListAsync()).Select(cur => new CarViewModel(cur)));
            }
        }
    }
}
