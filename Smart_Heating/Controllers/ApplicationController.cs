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
            /// If USerID is null -- then redirects to the LoginPage AR and View
            if (Session["UserID"] == null)     
                return RedirectToAction("LoginPage", "Home");


            /// Else checks the saved RoleId in session and according to the RoleID redirects to the matching User Controller
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                //var roles = db.UserRoles.ToList();

                /// If session user's RoleID = 1, then redirect to the Administrator DashBoard
                if (Session["Role"].Equals(db.UserRoles.First(role => role.RoleID == 1).RoleID.ToString())) 
                    return RedirectToAction("AdminDashBoard", "Admin");

                /// If session user's ID = 2, then redirect to the Staff DashBoard
                if (Session["Role"].Equals(db.UserRoles.First(role => role.RoleID == 2).RoleID.ToString()))
                    return RedirectToAction("StaffDashBoard", "Staff");

                /// If session user's ID = 3, then redirect to the OMS User DashBoard
                if (Session["Role"].Equals(db.UserRoles.First(role => role.RoleID == 3).RoleID.ToString()))
                    return RedirectToAction("OMSDashBoard", "OMS");

                /// If session user's ID = 4, then redirect to the ORS User DashBoard
                if (Session["Role"].Equals(db.UserRoles.First(role => role.RoleID == 4).RoleID.ToString()))
                    return RedirectToAction("ORSDashBoard", "ORS");

                /// If session user's ID = 5, then redirect to the OSBB User DashBoard
                if (Session["Role"].Equals(db.UserRoles.First(role => role.RoleID == 5).RoleID.ToString()))
                    return RedirectToAction("OSBBDashBoard", "OSBB");
                
                /// Else redirect to the Standart User DashBoard because the only possible RoleID left is for "Звичайний користувач" which equals 6
                return RedirectToAction("StandartUserDashBoard", "StandartUser");
            }
        }
    }
}