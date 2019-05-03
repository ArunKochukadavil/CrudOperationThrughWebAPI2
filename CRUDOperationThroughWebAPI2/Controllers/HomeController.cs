﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDOperationThroughWebAPI2.Controllers
{
    public class HomeController : Controller
    {
		/// <summary>
		/// hits the api for fetching the table data
		/// </summary>
		/// <returns></returns>
        public ActionResult Index()
        {
			var dataHelper = new DataHelper();
            return View(dataHelper.GetValuesFromTable());
        }
    }
}