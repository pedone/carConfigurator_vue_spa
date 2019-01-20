using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Helper
{
    public static class MiscHelper
    {
        public static string GenerateShortGuid()
        {
            var result = new StringBuilder(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            //replace url unsafe characters
            result.Replace("/", "-");
            result.Replace("+", "_");

            return result.ToString().TrimEnd('=');
        }
    }
}