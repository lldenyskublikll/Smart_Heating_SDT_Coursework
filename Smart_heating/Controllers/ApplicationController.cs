using Smart_heating.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Smart_heating.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application
        Random rnd = new Random();
        public ActionResult UserDashBoard()
        {
            return View();
        }
    }
}