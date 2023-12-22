using Smart_Heating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Smart_Heating.Controllers
{
    [StaffCheck]
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult StaffDashBoard()
        {
            /// If UserID is Null then redirects to the LoginPage view
            if (Session["UserID"] == null)
                return RedirectToAction("LoginPage", "Home");

            /// Else returns StaffDashBoard view that helps Staff user to perform all actions that he/she can do in this system
            return View();
        }


        /// Returns to the StaffViewMaintenance view list with maintenances by Staff's UserID
        public ActionResult StaffViewMaintenance(string sort_order, string current_filter, string search_str, int? page) 
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IdSort = String.IsNullOrEmpty(sort_order) ? "MaintID_desc" : "";
            ViewBag.StartdateSort = sort_order == "Startdate" ? "Startdate_desc" : "Startdate";
            ViewBag.EnddateSort = sort_order == "Enddate" ? "Enddate_desc" : "Enddate";
            ViewBag.StreetSort = sort_order == "Street" ? "Street_desc" : "Street";
            ViewBag.DistrictSort = sort_order == "District" ? "District_desc" : "District";
            ViewBag.EstablishmentSort = sort_order == "Establishment" ? "Establishment_desc" : "Establishment";


            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities()) 
            {
                int.TryParse(Session["UserID"].ToString(), out int id);
                var staff_maint_query = db.Maintenance_List_AdminView.Where(a => a.ID_Працівника == id) as IEnumerable<Maintenance_List_AdminView>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {

                    string sstr = search_str.ToLower();
                    staff_maint_query = staff_maint_query.Where(s => s.Тип_роботи.ToLower().Contains(search_str)
                                                                  || s.Статус.ToLower().Contains(search_str) 
                                                                  || s.Дата_початку_робіт.ToLower().Contains(search_str)
                                                                  || s.Дата_закінчення_робіт.ToLower().Contains(search_str)
                                                                  || s.Район.ToLower().Contains(search_str)
                                                                  || s.Назва_вулиці.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "MaintID_desc":
                        staff_maint_query = staff_maint_query.OrderByDescending(s => s.ID_Тех__роботи);
                        break;                   
                    case "Startdate":
                        staff_maint_query = staff_maint_query.OrderBy(s => s.Дата_початку_робіт);
                        break;
                    case "Startdate_desc":
                        staff_maint_query = staff_maint_query.OrderByDescending(s => s.Дата_початку_робіт);
                        break;
                    case "Enddate":
                        staff_maint_query = staff_maint_query.OrderBy(s => s.Дата_закінчення_робіт);
                        break;
                    case "Enddate_desc":
                        staff_maint_query = staff_maint_query.OrderByDescending(s => s.Дата_закінчення_робіт);
                        break;
                    case "Street":
                        staff_maint_query = staff_maint_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        staff_maint_query = staff_maint_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "District":
                        staff_maint_query = staff_maint_query.OrderBy(s => s.Район);
                        break;
                    case "District_desc":
                        staff_maint_query = staff_maint_query.OrderByDescending(s => s.Район);
                        break;
                    case "Establishment":
                        staff_maint_query = staff_maint_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establishment_desc":
                        staff_maint_query = staff_maint_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        staff_maint_query = staff_maint_query.OrderBy(s => s.ID_Тех__роботи);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(staff_maint_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }


        /// Returns to the StaffViewSensors view list with all sensors 
        public ActionResult StaffViewSensors() 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var staff_sensors_query = db.Sensor_List_View;

                return View(staff_sensors_query.ToList());
            }
        }


        /// Returns to the StaffViewIndicators view list with all indicators
        public ActionResult StaffViewIndicators() 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var staff_indicators_query = db.Indicators_List_View;

                return View(staff_indicators_query.ToList());
            }
        }



        public ActionResult ChangeMaintInfo(int? MaintID) 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var maint = db.Maintenances.Find(MaintID);
                
                if (maint != null)
                {
                    return View(maint);
                }
                return RedirectToAction("StaffViewMaintenance");
            }
        }

        [HttpPost]
        public ActionResult ChangeMaintInfo(Maintenance new_maint) 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities()) 
            {
                var maint = db.Maintenances.Find(new_maint.MaintID);
                if(maint != null) 
                {
                    maint.MaintType = new_maint.MaintType;
                    maint.MaintStatus = new_maint.MaintStatus;
                    maint.MaintStartDate = new_maint.MaintStartDate;
                    maint.MaintEndDate = new_maint.MaintEndDate;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ViewBag.Message = "Помилка під час зміни значень";
                        return View(maint.MaintID);
                    }
                }
                return RedirectToAction("StaffViewMaintenance");
            }                
        }
    }
}