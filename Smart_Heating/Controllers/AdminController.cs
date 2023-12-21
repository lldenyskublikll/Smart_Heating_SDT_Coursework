using Smart_Heating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Smart_Heating.Controllers
{
    [AdminCheck]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminDashBoard()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("LoginPage", "Home");

            return View();
        }

        public ActionResult AdminViewMaintenance() 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var query = db.Maintenance_List_AdminView;

                return View(query.ToList());
            }

                 
        }

        
    }
}