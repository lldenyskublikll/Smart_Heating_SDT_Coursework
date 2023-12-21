using Smart_Heating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smart_Heating.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application
        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("LoginPage", "Home");

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                //var roles = db.UserRoles.ToList();
                
                if (Session["Role"].Equals(db.UserRoles.First(role => role.RoleID == 1).RoleID.ToString())) //check for role
                    return RedirectToAction("AdminDashBoard", "Admin");
                
                if (Session["Role"].Equals(db.UserRoles.First(role => role.RoleID == 2).RoleID.ToString()))
                    return RedirectToAction("StaffDashBoard", "Staff");
                
                if (Session["Role"].Equals(db.UserRoles.First(role => role.RoleID == 3).RoleID.ToString()))
                    return RedirectToAction("OMSDashBoard", "OMS");

                if (Session["Role"].Equals(db.UserRoles.First(role => role.RoleID == 4).RoleID.ToString()))
                    return RedirectToAction("ORSfDashBoard", "ORS");

                if (Session["Role"].Equals(db.UserRoles.First(role => role.RoleID == 5).RoleID.ToString()))
                    return RedirectToAction("OSBBDashBoard", "OSBB");
                
                return RedirectToAction("StandartUserDashBoard", "StandartUser");
            }
        }


    }
}