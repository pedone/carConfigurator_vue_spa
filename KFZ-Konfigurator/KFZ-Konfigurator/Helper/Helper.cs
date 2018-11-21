using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Helper
{
    public static class Helper
    {
        public static string If(bool conditional, string data)
        {
            return conditional ? data : null;
        }
    }
}