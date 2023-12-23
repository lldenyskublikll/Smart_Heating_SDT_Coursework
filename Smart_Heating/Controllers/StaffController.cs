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
        public ActionResult StaffViewSensors(string sort_order, string current_filter, string search_str, int? page) 
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IdSort = String.IsNullOrEmpty(sort_order) ? "SensorID_desc" : "";
            ViewBag.TypeSort = sort_order == "Type" ? "Type_desc" : "Type";
            ViewBag.StreetSort = sort_order == "Street" ? "Street_desc" : "Street";
            ViewBag.DistrictSort = sort_order == "District" ? "District_desc" : "District";
            ViewBag.EstablishmentSort = sort_order == "Establish" ? "Establish_desc" : "Establish";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;


            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var staff_sensors_query = db.Sensor_List_View as IEnumerable<Sensor_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    staff_sensors_query = staff_sensors_query.Where(s => s.Тип_датчика.ToLower().Contains(search_str)
                                                                      || s.Назва_вулиці.ToLower().Contains(search_str)
                                                                      || s.Район.ToLower().Contains(search_str)
                                                                      || s.Назва_закладу.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "SensorID_desc":
                        staff_sensors_query = staff_sensors_query.OrderByDescending(s => s.ID_датчика);
                        break;
                    case "Type":
                        staff_sensors_query = staff_sensors_query.OrderBy(s => s.Тип_датчика);
                        break;
                    case "Type_desc":
                        staff_sensors_query = staff_sensors_query.OrderByDescending(s => s.Тип_датчика);
                        break;
                    case "Street":
                        staff_sensors_query = staff_sensors_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        staff_sensors_query = staff_sensors_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "District":
                        staff_sensors_query = staff_sensors_query.OrderBy(s => s.Район);
                        break;
                    case "District_desc":
                        staff_sensors_query = staff_sensors_query.OrderByDescending(s => s.Район);
                        break;
                    case "Establish":
                        staff_sensors_query = staff_sensors_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        staff_sensors_query = staff_sensors_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        staff_sensors_query = staff_sensors_query.OrderBy(s => s.ID_датчика);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(staff_sensors_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }


        /// Returns to the StaffViewIndicators view list with all indicators
        public ActionResult StaffViewIndicators(string sort_order, string current_filter, string search_str, int? page) 
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.DateSort = String.IsNullOrEmpty(sort_order) ? "Date_desc" : "";
            ViewBag.SensorSort = sort_order == "Sensor" ? "Sensor_desc" : "Sensor";
            ViewBag.TypeSort = sort_order == "Type" ? "Type_desc" : "Type";
            ViewBag.IndSort = sort_order == "Ind" ? "Ind_desc" : "Ind";
            ViewBag.StreetSort = sort_order == "Street" ? "Street_desc" : "Street";
            ViewBag.DistrictSort = sort_order == "District" ? "District_desc" : "District";
            ViewBag.EstablishmentSort = sort_order == "Establish" ? "Establish_desc" : "Establish";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;


            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var staff_indicators_query = db.Indicators_List_View as IEnumerable<Indicators_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    staff_indicators_query = staff_indicators_query.Where(s => s.Дата_та_час.ToString().Contains(search_str)
                                                                            || s.ID_сенсору.ToString().Contains(search_str)
                                                                            || s.Тип_датчика.ToLower().Contains(search_str)
                                                                            || s.Показник.ToLower().Contains(search_str)
                                                                            || s.Назва_вулиці.ToLower().Contains(search_str)
                                                                            || s.Район.ToLower().Contains(search_str)
                                                                            || s.Назва_закладу.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {                    
                    case "Date_desc":
                        staff_indicators_query = staff_indicators_query.OrderByDescending(s => s.Дата_та_час);
                        break;
                    case "Sensor":
                        staff_indicators_query = staff_indicators_query.OrderBy(s => s.ID_сенсору);
                        break;
                    case "Sensor_desc":
                        staff_indicators_query = staff_indicators_query.OrderByDescending(s => s.ID_сенсору);
                        break;
                    case "Type":
                        staff_indicators_query = staff_indicators_query.OrderBy(s => s.Тип_датчика);
                        break;
                    case "Type_desc":
                        staff_indicators_query = staff_indicators_query.OrderByDescending(s => s.Тип_датчика);
                        break;
                    case "Ind":
                        staff_indicators_query = staff_indicators_query.OrderBy(s => s.Показник);
                        break;
                    case "Ind_desc":
                        staff_indicators_query = staff_indicators_query.OrderByDescending(s => s.Показник);
                        break;
                    case "Street":
                        staff_indicators_query = staff_indicators_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        staff_indicators_query = staff_indicators_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "District":
                        staff_indicators_query = staff_indicators_query.OrderBy(s => s.Район);
                        break;
                    case "District_desc":
                        staff_indicators_query = staff_indicators_query.OrderByDescending(s => s.Район);
                        break;
                    case "Establish":
                        staff_indicators_query = staff_indicators_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        staff_indicators_query = staff_indicators_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        staff_indicators_query = staff_indicators_query.OrderBy(s => s.Дата_та_час);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(staff_indicators_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }


        /// Change maintenance AR method that checks if there is a maintenance with this ID
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


        /// Change maintenance AR method that changes tha maintenance info if previous method found maintenance with tis ID
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