using Smart_Heating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace Smart_Heating.Controllers
{
    [StandartUserCheck]
    public class StandartUserController : Controller
    {
        // GET: StandartUser
        public ActionResult StandartUserDashBoard()
        {
            /// If UserID is Null then redirects to the LoginPage view
            if (Session["UserID"] == null)
                return RedirectToAction("LoginPage", "Home");

            /// Else returns StandartUserDashBoard view that helps Standart user to perform all actions that he/she can do in this system
            return View();
        }

        /// Returns to the StandartUserViewMaintenance view list with maintenances by User's AddressID
        public ActionResult StandartUserViewMaintenance(string sort_order, string current_filter, string search_str, int? page) 
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
            ViewBag.DistrictSort = sort_order == "District" ? "District_desc" : "District";
            ViewBag.EstablishmentSort = sort_order == "Establishment" ? "Establishment_desc" : "Establishment";


            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;


            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                int.TryParse(Session["Address"].ToString(), out int id);
                var st_user_maint_query = db.Maintenance_List_StandartView.Where(a => a.ID_адреси == id) as IEnumerable<Maintenance_List_StandartView>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {                    
                    string sstr = search_str.ToLower();
                    st_user_maint_query = st_user_maint_query.Where(s => s.Прізвище_працівника.ToLower().Contains(search_str) 
                                                                      || s.Ім_я_працівника.ToLower().Contains(search_str)
                                                                      || s.Тип_роботи.ToLower().Contains(search_str)
                                                                      || s.Статус.ToLower().Contains(search_str)
                                                                      || s.Дата_початку_робіт.ToLower().Contains(search_str)
                                                                      || s.Дата_закінчення_робіт.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "MaintID_desc":
                        st_user_maint_query = st_user_maint_query.OrderByDescending(s => s.ID_Тех__роботи);
                        break;
                    case "Surname":
                        st_user_maint_query = st_user_maint_query.OrderBy(s => s.Прізвище_працівника);
                        break;
                    case "Surname_desc":
                        st_user_maint_query = st_user_maint_query.OrderByDescending(s => s.Прізвище_працівника);
                        break;
                    case "Name":
                        st_user_maint_query = st_user_maint_query.OrderBy(s => s.Ім_я_працівника);
                        break;
                    case "Name_desc":
                        st_user_maint_query = st_user_maint_query.OrderByDescending(s => s.Ім_я_працівника);
                        break;
                    case "Secondname":
                        st_user_maint_query = st_user_maint_query.OrderBy(s => s.По_Батькові_працівника);
                        break;
                    case "Secondname_desc":
                        st_user_maint_query = st_user_maint_query.OrderByDescending(s => s.По_Батькові_працівника);
                        break;
                    case "Birth":
                        st_user_maint_query = st_user_maint_query.OrderBy(s => s.Дата_народження);
                        break;
                    case "Birth_desc":
                        st_user_maint_query = st_user_maint_query.OrderByDescending(s => s.Дата_народження);
                        break;
                    case "Startdate":
                        st_user_maint_query = st_user_maint_query.OrderBy(s => s.Дата_початку_робіт);
                        break;
                    case "Startdate_desc":
                        st_user_maint_query = st_user_maint_query.OrderByDescending(s => s.Дата_початку_робіт);
                        break;
                    case "Enddate":
                        st_user_maint_query = st_user_maint_query.OrderBy(s => s.Дата_закінчення_робіт);
                        break;
                    case "Enddate_desc":
                        st_user_maint_query = st_user_maint_query.OrderByDescending(s => s.Дата_закінчення_робіт);
                        break;
                    case "Street":
                        st_user_maint_query = st_user_maint_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        st_user_maint_query = st_user_maint_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "District":
                        st_user_maint_query = st_user_maint_query.OrderBy(s => s.Район);
                        break;
                    case "District_desc":
                        st_user_maint_query = st_user_maint_query.OrderByDescending(s => s.Район);
                        break;
                    case "Establishment":
                        st_user_maint_query = st_user_maint_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establishment_desc":
                        st_user_maint_query = st_user_maint_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        st_user_maint_query = st_user_maint_query.OrderBy(s => s.ID_Тех__роботи);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(st_user_maint_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        /// Returns to the StandartUserViewSensors view list with sensors by User's AddressID
        public ActionResult StandartUserViewSensors(string sort_order, string current_filter, string search_str, int? page) 
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IdSort = String.IsNullOrEmpty(sort_order) ? "SensorID_desc" : "";
            ViewBag.TypeSort = sort_order == "Type" ? "Type_desc" : "Type";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;


            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities()) 
            {
                int.TryParse(Session["Address"].ToString(), out int id);
                var st_user_sensors_query = db.Sensor_List_View.Where(a => a.ID_адреси == id) as IEnumerable<Sensor_List_View>; 

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    st_user_sensors_query = st_user_sensors_query.Where(s => s.Тип_датчика.ToLower().Contains(search_str)); 
                }

                switch (sort_order)      //page sorting switch
                {
                    case "SensorID_desc":
                        st_user_sensors_query = st_user_sensors_query.OrderByDescending(s => s.ID_датчика);
                        break;
                    case "Type":
                        st_user_sensors_query = st_user_sensors_query.OrderBy(s => s.Тип_датчика);
                        break;
                    case "Type_desc":
                        st_user_sensors_query = st_user_sensors_query.OrderByDescending(s => s.Тип_датчика);
                        break;                    
                    default:
                        st_user_sensors_query = st_user_sensors_query.OrderBy(s => s.ID_датчика);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(st_user_sensors_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        /// Returns to the StandartUserViewIndicators view list with indicators by User's AddressID
        public ActionResult StandartUserViewIndicators(string sort_order, string current_filter, string search_str, int? page) 
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.DateSort = String.IsNullOrEmpty(sort_order) ? "Date_desc" : "";
            ViewBag.TypeSort = sort_order == "Type" ? "Type_desc" : "Type";
            ViewBag.IndSort = sort_order == "Ind" ? "Ind_desc" : "Ind";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;


            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities()) 
            {
                int.TryParse(Session["Address"].ToString(), out int id);                
                var st_user_indicators_query = db.Indicators_List_View.Where(a => a.ID_адреси == id) as IEnumerable<Indicators_List_View>; 

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    st_user_indicators_query = st_user_indicators_query.Where(s => s.Тип_датчика.ToLower().Contains(search_str)
                                                                                || s.Показник.ToLower().Contains(search_str)
                                                                                || s.Дата_та_час.ToString().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "Date_desc":
                        st_user_indicators_query = st_user_indicators_query.OrderByDescending(s => s.Дата_та_час);
                        break;
                    case "Type":
                        st_user_indicators_query = st_user_indicators_query.OrderBy(s => s.Тип_датчика);
                        break;
                    case "Type_desc":
                        st_user_indicators_query = st_user_indicators_query.OrderByDescending(s => s.Тип_датчика);
                        break;
                    case "Ind":
                        st_user_indicators_query = st_user_indicators_query.OrderBy(s => s.Показник);
                        break;
                    case "Ind_desc":
                        st_user_indicators_query = st_user_indicators_query.OrderByDescending(s => s.Показник);
                        break;
                    default:
                        st_user_indicators_query = st_user_indicators_query.OrderBy(s => s.Дата_та_час);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(st_user_indicators_query.ToPagedList(pagenumber, pagesize));
            }
        }
    }
}