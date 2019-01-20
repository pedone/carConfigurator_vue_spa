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
    public class ModelController : Controller
    {
        [Route("configuration/models", Name = Constants.Routes.ModelOverview)]
        public ActionResult Index()
        {
            return View();
        }
    }
}