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
            /// If UserID is Null then redirects to the LoginPage view
            if (Session["UserID"] == null)
                return RedirectToAction("LoginPage", "Home");
            
            /// Else returns AdminDashBoard view that helps Administrator to perform all actions that he/she can do in this system
            return View(); 
        }

        /// Redirects to the view that shows Andmin all information about Maintenances, their statuses, start/end dates and Engineer that leads it
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