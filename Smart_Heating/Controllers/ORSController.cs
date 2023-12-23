using PagedList;
using Smart_Heating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smart_Heating.Controllers
{
    [ORSCheck]
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

        public ActionResult ORSViewMaintenance(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IdSort = String.IsNullOrEmpty(sort_order) ? "MaintID_desc" : "";
            ViewBag.SurnameSort = sort_order == "Surname" ? "Surname_desc" : "Surname";
            ViewBag.NameSort = sort_order == "Name" ? "Name_desc" : "Name";
            ViewBag.SecondnameSort = sort_order == "Secondname" ? "Secondname_desc" : "Secondname";
            ViewBag.BirthdateSort = sort_order == "Birth" ? "Birth_desc" : "Birth";
            ViewBag.StartdateSort = sort_order == "Startdate" ? "Startdate_desc" : "Startdate";
            ViewBag.EnddateSort = sort_order == "Enddate" ? "Enddate_desc" : "Enddate";
            ViewBag.StreetSort = sort_order == "Street" ? "Street_desc" : "Street";            
            ViewBag.EstablishmentSort = sort_order == "Establishment" ? "Establishment_desc" : "Establishment";


            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                int.TryParse(Session["UserID"].ToString(), out int id);

                var regionID = db.Users_List_View.First(r => r.ID_Користувача == id).ID_району;

                var ors_maint_query = db.Maintenance_List_StandartView.Where(a => a.ID_району.ToString() == regionID) as IEnumerable<Maintenance_List_StandartView>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    ors_maint_query = ors_maint_query.Where(s => s.Прізвище_працівника.ToLower().Contains(search_str)
                                                              || s.Ім_я_працівника.ToLower().Contains(search_str)
                                                              || s.Тип_роботи.ToLower().Contains(search_str)
                                                              || s.Статус.ToLower().Contains(search_str)
                                                              || s.Дата_початку_робіт.ToLower().Contains(search_str)
                                                              || s.Дата_закінчення_робіт.ToLower().Contains(search_str)
                                                              || s.Назва_вулиці.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "MaintID_desc":
                        ors_maint_query = ors_maint_query.OrderByDescending(s => s.ID_Тех__роботи);
                        break;
                    case "Surname":
                        ors_maint_query = ors_maint_query.OrderBy(s => s.Прізвище_працівника);
                        break;
                    case "Surname_desc":
                        ors_maint_query = ors_maint_query.OrderByDescending(s => s.Прізвище_працівника);
                        break;
                    case "Name":
                        ors_maint_query = ors_maint_query.OrderBy(s => s.Ім_я_працівника);
                        break;
                    case "Name_desc":
                        ors_maint_query = ors_maint_query.OrderByDescending(s => s.Ім_я_працівника);
                        break;
                    case "Secondname":
                        ors_maint_query = ors_maint_query.OrderBy(s => s.По_Батькові_працівника);
                        break;
                    case "Secondname_desc":
                        ors_maint_query = ors_maint_query.OrderByDescending(s => s.По_Батькові_працівника);
                        break;
                    case "Birth":
                        ors_maint_query = ors_maint_query.OrderBy(s => s.Дата_народження);
                        break;
                    case "Birth_desc":
                        ors_maint_query = ors_maint_query.OrderByDescending(s => s.Дата_народження);
                        break;
                    case "Startdate":
                        ors_maint_query = ors_maint_query.OrderBy(s => s.Дата_початку_робіт);
                        break;
                    case "Startdate_desc":
                        ors_maint_query = ors_maint_query.OrderByDescending(s => s.Дата_початку_робіт);
                        break;
                    case "Enddate":
                        ors_maint_query = ors_maint_query.OrderBy(s => s.Дата_закінчення_робіт);
                        break;
                    case "Enddate_desc":
                        ors_maint_query = ors_maint_query.OrderByDescending(s => s.Дата_закінчення_робіт);
                        break;
                    case "Street":
                        ors_maint_query = ors_maint_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        ors_maint_query = ors_maint_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "Establishment":
                        ors_maint_query = ors_maint_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establishment_desc":
                        ors_maint_query = ors_maint_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        ors_maint_query = ors_maint_query.OrderBy(s => s.ID_Тех__роботи);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(ors_maint_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        public ActionResult ORSViewSensors(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IdSort = String.IsNullOrEmpty(sort_order) ? "SensorID_desc" : "";
            ViewBag.TypeSort = sort_order == "Type" ? "Type_desc" : "Type";
            ViewBag.StreetSort = sort_order == "Street" ? "Street_desc" : "Street";           
            ViewBag.EstablishmentSort = sort_order == "Establish" ? "Establish_desc" : "Establish";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                int.TryParse(Session["UserID"].ToString(), out int id);

                var regionID = db.Users_List_View.First(r => r.ID_Користувача == id).ID_району;

                var ors_sensors_query = db.Sensor_List_View.Where(a => a.ID_району.ToString() == regionID) as IEnumerable<Sensor_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    ors_sensors_query = ors_sensors_query.Where(s => s.Тип_датчика.ToLower().Contains(search_str)
                                                                  || s.Назва_вулиці.ToLower().Contains(search_str)
                                                                  || s.Назва_закладу.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "SensorID_desc":
                        ors_sensors_query = ors_sensors_query.OrderByDescending(s => s.ID_датчика);
                        break;
                    case "Type":
                        ors_sensors_query = ors_sensors_query.OrderBy(s => s.Тип_датчика);
                        break;
                    case "Type_desc":
                        ors_sensors_query = ors_sensors_query.OrderByDescending(s => s.Тип_датчика);
                        break;
                    case "Street":
                        ors_sensors_query = ors_sensors_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        ors_sensors_query = ors_sensors_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "Establish":
                        ors_sensors_query = ors_sensors_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        ors_sensors_query = ors_sensors_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        ors_sensors_query = ors_sensors_query.OrderBy(s => s.ID_датчика);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(ors_sensors_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }


        public ActionResult ORSViewIndicators(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.DateSort = String.IsNullOrEmpty(sort_order) ? "Date_desc" : "";
            ViewBag.SensorSort = sort_order == "Sensor" ? "Sensor_desc" : "Sensor";
            ViewBag.TypeSort = sort_order == "Type" ? "Type_desc" : "Type";
            ViewBag.IndSort = sort_order == "Ind" ? "Ind_desc" : "Ind";
            ViewBag.StreetSort = sort_order == "Street" ? "Street_desc" : "Street";
            ViewBag.EstablishmentSort = sort_order == "Establish" ? "Establish_desc" : "Establish";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                int.TryParse(Session["UserID"].ToString(), out int id);

                var regionID = db.Users_List_View.First(r => r.ID_Користувача == id).ID_району;

                var ors_indicators_query = db.Indicators_List_View.Where(a => a.ID_району.ToString() == regionID) as IEnumerable<Indicators_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    ors_indicators_query = ors_indicators_query.Where(s => s.Дата_та_час.ToString().Contains(search_str)
                                                                            || s.ID_сенсору.ToString().Contains(search_str)
                                                                            || s.Тип_датчика.ToLower().Contains(search_str)
                                                                            || s.Показник.ToLower().Contains(search_str)
                                                                            || s.Назва_вулиці.ToLower().Contains(search_str)
                                                                            || s.Назва_закладу.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "Date_desc":
                        ors_indicators_query = ors_indicators_query.OrderByDescending(s => s.Дата_та_час);
                        break;
                    case "Sensor":
                        ors_indicators_query = ors_indicators_query.OrderBy(s => s.ID_сенсору);
                        break;
                    case "Sensor_desc":
                        ors_indicators_query = ors_indicators_query.OrderByDescending(s => s.ID_сенсору);
                        break;
                    case "Type":
                        ors_indicators_query = ors_indicators_query.OrderBy(s => s.Тип_датчика);
                        break;
                    case "Type_desc":
                        ors_indicators_query = ors_indicators_query.OrderByDescending(s => s.Тип_датчика);
                        break;
                    case "Ind":
                        ors_indicators_query = ors_indicators_query.OrderBy(s => s.Показник);
                        break;
                    case "Ind_desc":
                        ors_indicators_query = ors_indicators_query.OrderByDescending(s => s.Показник);
                        break;
                    case "Street":
                        ors_indicators_query = ors_indicators_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        ors_indicators_query = ors_indicators_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "Establish":
                        ors_indicators_query = ors_indicators_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        ors_indicators_query = ors_indicators_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        ors_indicators_query = ors_indicators_query.OrderBy(s => s.Дата_та_час);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(ors_indicators_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        public ActionResult ORSViewUsers(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IDSort = String.IsNullOrEmpty(sort_order) ? "ID_desc" : "";
            ViewBag.RoleSort = sort_order == "Role" ? "Role_desc" : "Role";
            ViewBag.SurnameSort = sort_order == "Surname" ? "Surname_desc" : "Surname";
            ViewBag.NameSort = sort_order == "Name" ? "Name_desc" : "Name";
            ViewBag.GenderSort = sort_order == "Gender" ? "Gender_desc" : "Gender";
            ViewBag.BirthdateSort = sort_order == "Birth" ? "Birth_desc" : "Birth";
            ViewBag.StreetSort = sort_order == "Street" ? "Street_desc" : "Street";
            ViewBag.BuildingSort = sort_order == "Building" ? "Building_desc" : "Building";
            ViewBag.EstablishmentSort = sort_order == "Establish" ? "Establish_desc" : "Establish";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;


            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                int.TryParse(Session["UserID"].ToString(), out int id);

                var regionID = db.Users_List_View.First(r => r.ID_Користувача == id).ID_району;

                var oms_userlist_query = db.Users_List_View.Where(a => a.Роль_у_системі.ToString() != "Адміністатор" 
                                                                    && a.ID_району.ToString() == regionID) as IEnumerable<Users_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    oms_userlist_query = oms_userlist_query.Where(s => s.ID_Користувача.ToString().Contains(search_str)
                                                                    || s.Роль_у_системі.ToLower().Contains(search_str)
                                                                    || s.Прізвище.ToLower().Contains(search_str)
                                                                    || s.Ім_я.ToLower().Contains(search_str)
                                                                    || s.По_Батькові.ToLower().Contains(search_str)
                                                                    || s.Дата_народження.ToString().Contains(search_str)
                                                                    || s.Назва_вулиці.ToLower().Contains(search_str)
                                                                    || s.Назва_закладу.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "ID_desc":
                        oms_userlist_query = oms_userlist_query.OrderByDescending(s => s.ID_Користувача);
                        break;
                    case "Role":
                        oms_userlist_query = oms_userlist_query.OrderBy(s => s.Роль_у_системі);
                        break;
                    case "Role_desc":
                        oms_userlist_query = oms_userlist_query.OrderByDescending(s => s.Роль_у_системі);
                        break;
                    case "Surname":
                        oms_userlist_query = oms_userlist_query.OrderBy(s => s.Прізвище);
                        break;
                    case "Surname_desc":
                        oms_userlist_query = oms_userlist_query.OrderByDescending(s => s.Прізвище);
                        break;
                    case "Name":
                        oms_userlist_query = oms_userlist_query.OrderBy(s => s.Ім_я);
                        break;
                    case "Name_desc":
                        oms_userlist_query = oms_userlist_query.OrderByDescending(s => s.Ім_я);
                        break;
                    case "Gender":
                        oms_userlist_query = oms_userlist_query.OrderBy(s => s.Стать);
                        break;
                    case "Gender_desc":
                        oms_userlist_query = oms_userlist_query.OrderByDescending(s => s.Стать);
                        break;
                    case "Birth":
                        oms_userlist_query = oms_userlist_query.OrderBy(s => s.Дата_народження);
                        break;
                    case "Birth_desc":
                        oms_userlist_query = oms_userlist_query.OrderByDescending(s => s.Дата_народження);
                        break;
                    case "Street":
                        oms_userlist_query = oms_userlist_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        oms_userlist_query = oms_userlist_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "Building":
                        oms_userlist_query = oms_userlist_query.OrderBy(s => s.Тип_будинку);
                        break;
                    case "Building_desc":
                        oms_userlist_query = oms_userlist_query.OrderByDescending(s => s.Тип_будинку);
                        break;
                    case "Establish":
                        oms_userlist_query = oms_userlist_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        oms_userlist_query = oms_userlist_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        oms_userlist_query = oms_userlist_query.OrderBy(s => s.ID_Користувача);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(oms_userlist_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }
    }
}