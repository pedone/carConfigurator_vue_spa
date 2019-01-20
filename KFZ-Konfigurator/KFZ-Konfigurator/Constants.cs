using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator
{
    public static class Constants
    {
        public const double KW_to_PS = 1.35962f;
        public const int PageItemsCount = 10;

        public static class Routes
        {
            public const string ModelOverview = nameof(ModelOverview);
            public const string OrderList = nameof(OrderList);
            public const string Home = nameof(Home);
        }
    }
}