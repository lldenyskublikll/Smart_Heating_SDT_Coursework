using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smart_Heating.Controllers
{
    public class ORSController : Controller
    {
        // GET: ORS
        public ActionResult ORSDashBoard()
        {
            /// If UserID is Null then redirects to the LoginPage view
            if (Session["UserID"] == null)
                return RedirectToAction("LoginPage", "Home");

            /// Else returns ORSDashBoard view that helps ORS user to perform all actions that he/she can do in this system
            return View();
        }
    }
}