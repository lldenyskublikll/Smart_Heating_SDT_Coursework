using PagedList;
using Smart_Heating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smart_Heating.Controllers
{
    [OSBBCheck]
    public class OSBBController : Controller
    {
        // GET: OSBB
        public ActionResult OSBBDashBoard()
        {
            /// If UserID is Null then redirects to the LoginPage view
            if (Session["UserID"] == null)
                return RedirectToAction("LoginPage", "Home");

            /// Else returns OSBBDashBoard view that helps OSBB user to perform all actions that he/she can do in this system
            return View();
        }


        public ActionResult OSBBViewMaintenance(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IdSort = String.IsNullOrEmpty(sort_order) ? "MaintID_desc" : "";
            ViewBag.SurnameSort = sort_order == "Surname" ? "Surname_desc" : "Surname";
            ViewBag.NameSort = sort_order == "Name" ? "Name_desc" : "Name";
            ViewBag.SecondnameSort = sort_order == "Secondname" ? "Secondname_desc" : "Secondname";
            ViewBag.BirthdateSort = sort_order == "Birth" ? "Birth_desc" : "Birth";
            ViewBag.StartdateSort = sort_order == "Startdate" ? "Startdate_desc" : "Startdate";
            ViewBag.EnddateSort = sort_order == "Enddate" ? "Enddate_desc" : "Enddate";
            ViewBag.EstablishmentSort = sort_order == "Establishment" ? "Establishment_desc" : "Establishment";


            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                int.TryParse(Session["UserID"].ToString(), out int id);

                var streetID = db.Users_List_View.First(r => r.ID_Користувача == id).ID_вулиці;

                var housenumber = db.Users_List_View.First(r => r.ID_Користувача == id).C__Будинку;

                var osbb_maint_query = db.Maintenance_List_StandartView.Where(a => a.ID_вулиці.ToString() == streetID
                                                                                && a.C__Будинку == housenumber) as IEnumerable<Maintenance_List_StandartView>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    osbb_maint_query = osbb_maint_query.Where(s => s.Прізвище_працівника.ToLower().Contains(sstr)
                                                                || s.Ім_я_працівника.ToLower().Contains(sstr)
                                                                || s.Тип_роботи.ToLower().Contains(sstr)
                                                                || s.Статус.ToLower().Contains(sstr)
                                                                || s.Дата_початку_робіт.ToLower().Contains(sstr)
                                                                || s.Дата_закінчення_робіт.ToLower().Contains(sstr)
                                                                || s.C__Квартири.ToLower().Contains(sstr)
                                                                || s.C__Офісу.ToLower().Contains(sstr));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "MaintID_desc":
                        osbb_maint_query = osbb_maint_query.OrderByDescending(s => s.ID_Тех__роботи);
                        break;
                    case "Surname":
                        osbb_maint_query = osbb_maint_query.OrderBy(s => s.Прізвище_працівника);
                        break;
                    case "Surname_desc":
                        osbb_maint_query = osbb_maint_query.OrderByDescending(s => s.Прізвище_працівника);
                        break;
                    case "Name":
                        osbb_maint_query = osbb_maint_query.OrderBy(s => s.Ім_я_працівника);
                        break;
                    case "Name_desc":
                        osbb_maint_query = osbb_maint_query.OrderByDescending(s => s.Ім_я_працівника);
                        break;
                    case "Secondname":
                        osbb_maint_query = osbb_maint_query.OrderBy(s => s.По_Батькові_працівника);
                        break;
                    case "Secondname_desc":
                        osbb_maint_query = osbb_maint_query.OrderByDescending(s => s.По_Батькові_працівника);
                        break;
                    case "Birth":
                        osbb_maint_query = osbb_maint_query.OrderBy(s => s.Дата_народження);
                        break;
                    case "Birth_desc":
                        osbb_maint_query = osbb_maint_query.OrderByDescending(s => s.Дата_народження);
                        break;
                    case "Startdate":
                        osbb_maint_query = osbb_maint_query.OrderBy(s => s.Дата_початку_робіт);
                        break;
                    case "Startdate_desc":
                        osbb_maint_query = osbb_maint_query.OrderByDescending(s => s.Дата_початку_робіт);
                        break;
                    case "Enddate":
                        osbb_maint_query = osbb_maint_query.OrderBy(s => s.Дата_закінчення_робіт);
                        break;
                    case "Enddate_desc":
                        osbb_maint_query = osbb_maint_query.OrderByDescending(s => s.Дата_закінчення_робіт);
                        break;
                    case "Establishment":
                        osbb_maint_query = osbb_maint_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establishment_desc":
                        osbb_maint_query = osbb_maint_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        osbb_maint_query = osbb_maint_query.OrderBy(s => s.ID_Тех__роботи);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(osbb_maint_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        public ActionResult OSBBViewSensors(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IdSort = String.IsNullOrEmpty(sort_order) ? "SensorID_desc" : "";
            ViewBag.TypeSort = sort_order == "Type" ? "Type_desc" : "Type";
            ViewBag.EstablishmentSort = sort_order == "Establish" ? "Establish_desc" : "Establish";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                int.TryParse(Session["UserID"].ToString(), out int id);

                var streetID = db.Users_List_View.First(r => r.ID_Користувача == id).ID_вулиці;

                var housenumber = db.Users_List_View.First(r => r.ID_Користувача == id).C__Будинку;

                var osbb_sensors_query = db.Sensor_List_View.Where(a => a.ID_вулиці.ToString() == streetID
                                                                     && a.C__Будинку == housenumber) as IEnumerable<Sensor_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    osbb_sensors_query = osbb_sensors_query.Where(s => s.Тип_датчика.ToLower().Contains(sstr)
                                                                    || s.Назва_закладу.ToLower().Contains(sstr)
                                                                    || s.C__Квартири.ToLower().Contains(sstr)
                                                                    || s.C__Офісу.ToLower().Contains(sstr));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "SensorID_desc":
                        osbb_sensors_query = osbb_sensors_query.OrderByDescending(s => s.ID_датчика);
                        break;
                    case "Type":
                        osbb_sensors_query = osbb_sensors_query.OrderBy(s => s.Тип_датчика);
                        break;
                    case "Type_desc":
                        osbb_sensors_query = osbb_sensors_query.OrderByDescending(s => s.Тип_датчика);
                        break;
                    case "Establish":
                        osbb_sensors_query = osbb_sensors_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        osbb_sensors_query = osbb_sensors_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        osbb_sensors_query = osbb_sensors_query.OrderBy(s => s.ID_датчика);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(osbb_sensors_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }


        public ActionResult OSBBViewIndicators(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.DateSort = String.IsNullOrEmpty(sort_order) ? "Date_desc" : "";
            ViewBag.SensorSort = sort_order == "Sensor" ? "Sensor_desc" : "Sensor";
            ViewBag.TypeSort = sort_order == "Type" ? "Type_desc" : "Type";
            ViewBag.IndSort = sort_order == "Ind" ? "Ind_desc" : "Ind";
            ViewBag.EstablishmentSort = sort_order == "Establish" ? "Establish_desc" : "Establish";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                int.TryParse(Session["UserID"].ToString(), out int id);

                var streetID = db.Users_List_View.First(r => r.ID_Користувача == id).ID_вулиці;

                var housenumber = db.Users_List_View.First(r => r.ID_Користувача == id).C__Будинку;

                var osbb_indicators_query = db.Indicators_List_View.Where(a => a.ID_вулиці.ToString() == streetID
                                                                            && a.C__Будинку == housenumber) as IEnumerable<Indicators_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    osbb_indicators_query = osbb_indicators_query.Where(s => s.Дата_та_час.ToString().Contains(sstr)
                                                                            || s.ID_сенсору.ToString().Contains(sstr)
                                                                            || s.Тип_датчика.ToLower().Contains(sstr)
                                                                            || s.Показник.ToLower().Contains(sstr)
                                                                            || s.Назва_закладу.ToLower().Contains(sstr)
                                                                            || s.C__Квартири.ToLower().Contains(sstr)
                                                                            || s.C__Офісу.ToLower().Contains(sstr));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "Date_desc":
                        osbb_indicators_query = osbb_indicators_query.OrderByDescending(s => s.Дата_та_час);
                        break;
                    case "Sensor":
                        osbb_indicators_query = osbb_indicators_query.OrderBy(s => s.ID_сенсору);
                        break;
                    case "Sensor_desc":
                        osbb_indicators_query = osbb_indicators_query.OrderByDescending(s => s.ID_сенсору);
                        break;
                    case "Type":
                        osbb_indicators_query = osbb_indicators_query.OrderBy(s => s.Тип_датчика);
                        break;
                    case "Type_desc":
                        osbb_indicators_query = osbb_indicators_query.OrderByDescending(s => s.Тип_датчика);
                        break;
                    case "Ind":
                        osbb_indicators_query = osbb_indicators_query.OrderBy(s => s.Показник);
                        break;
                    case "Ind_desc":
                        osbb_indicators_query = osbb_indicators_query.OrderByDescending(s => s.Показник);
                        break;
                    case "Establish":
                        osbb_indicators_query = osbb_indicators_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        osbb_indicators_query = osbb_indicators_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        osbb_indicators_query = osbb_indicators_query.OrderBy(s => s.Дата_та_час);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(osbb_indicators_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        public ActionResult OSBBViewUsers(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IDSort = String.IsNullOrEmpty(sort_order) ? "ID_desc" : "";
            ViewBag.RoleSort = sort_order == "Role" ? "Role_desc" : "Role";
            ViewBag.SurnameSort = sort_order == "Surname" ? "Surname_desc" : "Surname";
            ViewBag.NameSort = sort_order == "Name" ? "Name_desc" : "Name";
            ViewBag.GenderSort = sort_order == "Gender" ? "Gender_desc" : "Gender";
            ViewBag.BirthdateSort = sort_order == "Birth" ? "Birth_desc" : "Birth";
            ViewBag.EstablishmentSort = sort_order == "Establish" ? "Establish_desc" : "Establish";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;


            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                int.TryParse(Session["UserID"].ToString(), out int id);

                var streetID = db.Users_List_View.First(r => r.ID_Користувача == id).ID_вулиці;

                var housenumber = db.Users_List_View.First(r => r.ID_Користувача == id).C__Будинку;

                var osbb_userlist_query = db.Users_List_View.Where(a => a.Роль_у_системі.ToString() != "Адміністатор"
                                                                    && a.ID_вулиці.ToString() == streetID
                                                                    && a.C__Будинку == housenumber) as IEnumerable<Users_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    osbb_userlist_query = osbb_userlist_query.Where(s => s.ID_Користувача.ToString().Contains(sstr)
                                                                      || s.Роль_у_системі.ToLower().Contains(sstr)
                                                                      || s.Прізвище.ToLower().Contains(sstr)
                                                                      || s.Ім_я.ToLower().Contains(sstr)
                                                                      || s.По_Батькові.ToLower().Contains(sstr)
                                                                      || s.Дата_народження.ToString().Contains(sstr)
                                                                      || s.C__Квартири.ToLower().Contains(sstr)
                                                                      || s.C__Офісу.ToLower().Contains(sstr)
                                                                      || s.Назва_закладу.ToLower().Contains(sstr));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "ID_desc":
                        osbb_userlist_query = osbb_userlist_query.OrderByDescending(s => s.ID_Користувача);
                        break;
                    case "Role":
                        osbb_userlist_query = osbb_userlist_query.OrderBy(s => s.Роль_у_системі);
                        break;
                    case "Role_desc":
                        osbb_userlist_query = osbb_userlist_query.OrderByDescending(s => s.Роль_у_системі);
                        break;
                    case "Surname":
                        osbb_userlist_query = osbb_userlist_query.OrderBy(s => s.Прізвище);
                        break;
                    case "Surname_desc":
                        osbb_userlist_query = osbb_userlist_query.OrderByDescending(s => s.Прізвище);
                        break;
                    case "Name":
                        osbb_userlist_query = osbb_userlist_query.OrderBy(s => s.Ім_я);
                        break;
                    case "Name_desc":
                        osbb_userlist_query = osbb_userlist_query.OrderByDescending(s => s.Ім_я);
                        break;
                    case "Gender":
                        osbb_userlist_query = osbb_userlist_query.OrderBy(s => s.Стать);
                        break;
                    case "Gender_desc":
                        osbb_userlist_query = osbb_userlist_query.OrderByDescending(s => s.Стать);
                        break;
                    case "Birth":
                        osbb_userlist_query = osbb_userlist_query.OrderBy(s => s.Дата_народження);
                        break;
                    case "Birth_desc":
                        osbb_userlist_query = osbb_userlist_query.OrderByDescending(s => s.Дата_народження);
                        break;
                    case "Establish":
                        osbb_userlist_query = osbb_userlist_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        osbb_userlist_query = osbb_userlist_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        osbb_userlist_query = osbb_userlist_query.OrderBy(s => s.ID_Користувача);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(osbb_userlist_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }
    }
}