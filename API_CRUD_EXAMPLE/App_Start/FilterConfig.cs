﻿using System.Web;
using System.Web.Mvc;

namespace API_CRUD_EXAMPLE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
