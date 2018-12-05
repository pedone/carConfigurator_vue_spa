using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class ConfigurationController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ConfigurationController));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UpdateActiveConfiguration(int? engineSettingsId, int[] accessoryIds, int? paintId, int? rimId)
        {
            if (!Request.IsAjaxRequest())
            {
                Log.Error("SetCarModel was called without ajax");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return "This action must be called with ajax";
            }

            if (engineSettingsId.HasValue)
                SessionData.ActiveConfiguration.EngineSettingsId = engineSettingsId.Value;
            if (accessoryIds != null)
                SessionData.ActiveConfiguration.AccessoryIds = accessoryIds;
            if (paintId.HasValue)
                SessionData.ActiveConfiguration.PaintId = paintId.Value;
            if (rimId.HasValue)
                SessionData.ActiveConfiguration.RimId = rimId.Value;

            return null;
        }
    }
}