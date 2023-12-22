using Smart_Heating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smart_Heating.Controllers
{
    public class OMSController : Controller
    {
        // GET: OMS
        public ActionResult OMSDashBoard()
        {
            /// If UserID is Null then redirects to the LoginPage view
            if (Session["UserID"] == null)
                return RedirectToAction("LoginPage", "Home");

            /// Else returns OMSDashBoard view that helps OMS user to perform all actions that he/she can do in this system
            return View();
        }


        /// Returns to the OMSViewMaintenance view list with all maintenances 
        public ActionResult OMSViewMaintenance() 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var oms_maint_query = db.Maintenance_List_StandartView;

                return View(oms_maint_query.ToList());
            }
        }


        /// Returns to the OMSViewSensors view list with all sensors 
        public ActionResult OMSViewSensors() 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var oms_sensors_query = db.Sensor_List_View;

                return View(oms_sensors_query.ToList());
            }
        }


        /// Returns to the OMSViewIndicators view list with all indicators
        public ActionResult OMSViewIndicators() 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var oms_indicators_query = db.Indicators_List_View;

                return View(oms_indicators_query.ToList());
            }
        }

        /// Returns to the OMSViewUsers view list with all users in the city
        public ActionResult OMSViewUsers() 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities()) 
            {
                var oms_userlist_query = db.Users_List_View;

                return View(oms_userlist_query.ToList());
            }
        }
    }
}