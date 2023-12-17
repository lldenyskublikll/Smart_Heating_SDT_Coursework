using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smart_heating.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Про цей застосунок";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Зворотній зв'язок";

            return View();
        }

        public ActionResult LogIn() 
        {
            return View();        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login() 
        {
            
        }
    }
}