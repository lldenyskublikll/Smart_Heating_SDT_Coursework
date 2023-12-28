using Smart_Heating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;

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
        public ActionResult AdminViewMaintenance(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IdSort = String.IsNullOrEmpty(sort_order) ? "MaintID_desc" : "";
            ViewBag.StaffIDSort = sort_order == "StaffID" ? "StaffID_desc" : "StaffID";
            ViewBag.LoginSort = sort_order == "Login" ? "Login_desc" : "Login";
            ViewBag.SurnameSort = sort_order == "Surname" ? "Surname_desc" : "Surname";
            ViewBag.NameSort = sort_order == "Name" ? "Name_desc" : "Name";
            ViewBag.SecondnameSort = sort_order == "Secondname" ? "Secondname_desc" : "Secondname";
            ViewBag.BirthdateSort = sort_order == "Birth" ? "Birth_desc" : "Birth";
            ViewBag.StartdateSort = sort_order == "Startdate" ? "Startdate_desc" : "Startdate";
            ViewBag.EnddateSort = sort_order == "Enddate" ? "Enddate_desc" : "Enddate";
            ViewBag.AddrIDSort = sort_order == "Address" ? "Address_desc" : "Address";
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
                var admin_maint_query = db.Maintenance_List_AdminView as IEnumerable<Maintenance_List_AdminView>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    admin_maint_query = admin_maint_query.Where(s => s.ID_Працівника.ToString().Contains(search_str)
                                                                  || s.Прізвище_працівника.ToLower().Contains(search_str)
                                                                  || s.Ім_я_працівника.ToLower().Contains(search_str)
                                                                  || s.Тип_роботи.ToLower().Contains(search_str)
                                                                  || s.Статус.ToLower().Contains(search_str)
                                                                  || s.Дата_початку_робіт.ToLower().Contains(search_str)
                                                                  || s.Дата_закінчення_робіт.ToLower().Contains(search_str)
                                                                  || s.ID_адреси.ToString().Contains(search_str)
                                                                  || s.Район.ToLower().Contains(search_str)
                                                                  || s.Назва_вулиці.ToLower().Contains(search_str)
                                                                  || s.Назва_закладу.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "MaintID_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.ID_Тех__роботи);
                        break;
                    case "StaffID":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.ID_Працівника);
                        break;
                    case "StaffID_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.ID_Працівника);
                        break;
                    case "Login":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.Логін);
                        break;
                    case "Login_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.Логін);
                        break;
                    case "Surname":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.Прізвище_працівника);
                        break;
                    case "Surname_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.Прізвище_працівника);
                        break;
                    case "Name":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.Ім_я_працівника);
                        break;
                    case "Name_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.Ім_я_працівника);
                        break;
                    case "Secondname":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.По_Батькові_працівника);
                        break;
                    case "Secondname_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.По_Батькові_працівника);
                        break;
                    case "Birth":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.Дата_народження);
                        break;
                    case "Birth_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.Дата_народження);
                        break;
                    case "Startdate":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.Дата_початку_робіт);
                        break;
                    case "Startdate_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.Дата_початку_робіт);
                        break;
                    case "Enddate":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.Дата_закінчення_робіт);
                        break;
                    case "Enddate_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.Дата_закінчення_робіт);
                        break;
                    case "Address":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.ID_адреси);
                        break;
                    case "Address_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.ID_адреси);
                        break;
                    case "Street":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "District":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.Район);
                        break;
                    case "District_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.Район);
                        break;
                    case "Establishment":
                        admin_maint_query = admin_maint_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establishment_desc":
                        admin_maint_query = admin_maint_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        admin_maint_query = admin_maint_query.OrderBy(s => s.ID_Тех__роботи);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(admin_maint_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        public ActionResult AdminViewUsers(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IDSort = String.IsNullOrEmpty(sort_order) ? "ID_desc" : "";
            ViewBag.LoginSort = sort_order == "Login" ? "Login_desc" : "Login";
            ViewBag.RoleSort = sort_order == "Role" ? "Role_desc" : "Role";
            ViewBag.SurnameSort = sort_order == "Surname" ? "Surname_desc" : "Surname";
            ViewBag.NameSort = sort_order == "Name" ? "Name_desc" : "Name";
            ViewBag.GenderSort = sort_order == "Gender" ? "Gender_desc" : "Gender";
            ViewBag.BirthdateSort = sort_order == "Birth" ? "Birth_desc" : "Birth";
            ViewBag.AddressIDSort = sort_order == "AddrID" ? "AddrID_desc" : "AddrID";
            ViewBag.StreetSort = sort_order == "Street" ? "Street_desc" : "Street";
            ViewBag.DistrictSort = sort_order == "District" ? "District_desc" : "District";
            ViewBag.BuildingSort = sort_order == "Building" ? "Building_desc" : "Building";
            ViewBag.EstablishmentSort = sort_order == "Establish" ? "Establish_desc" : "Establish";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var admin_userlist_query = db.Users_List_View as IEnumerable<Users_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    admin_userlist_query = admin_userlist_query.Where(s => s.ID_Користувача.ToString().Contains(search_str)
                                                                        || s.Роль_у_системі.ToLower().Contains(search_str)
                                                                        || s.Прізвище.ToLower().Contains(search_str)
                                                                        || s.Ім_я.ToLower().Contains(search_str)
                                                                        || s.По_Батькові.ToLower().Contains(search_str)
                                                                        || s.Дата_народження.ToString().Contains(search_str)
                                                                        || s.ID_адреси.ToString().Contains(search_str)
                                                                        || s.Назва_вулиці.ToLower().Contains(search_str)
                                                                        || s.Район.ToLower().Contains(search_str)
                                                                        || s.Назва_закладу.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "ID_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.ID_Користувача);
                        break;
                    case "Login":
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.Логін);
                        break;
                    case "Login_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.Логін);
                        break;
                    case "Role":
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.Роль_у_системі);
                        break;
                    case "Role_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.Роль_у_системі);
                        break;
                    case "Surname":
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.Прізвище);
                        break;
                    case "Surname_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.Прізвище);
                        break;
                    case "Name":
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.Ім_я);
                        break;
                    case "Name_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.Ім_я);
                        break;
                    case "Gender":
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.Стать);
                        break;
                    case "Gender_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.Стать);
                        break;
                    case "Birth":
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.Дата_народження);
                        break;
                    case "Birth_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.Дата_народження);
                        break;
                    case "AddrID":
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.ID_адреси);
                        break;
                    case "AddrID_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.ID_адреси);
                        break;
                    case "Street":
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "District":
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.Район);
                        break;
                    case "District_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.Район);
                        break;
                    case "Building":
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.Тип_будинку);
                        break;
                    case "Building_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.Тип_будинку);
                        break;
                    case "Establish":
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        admin_userlist_query = admin_userlist_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        admin_userlist_query = admin_userlist_query.OrderBy(s => s.ID_Користувача);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(admin_userlist_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        public ActionResult AdminViewSensors(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.IdSort = String.IsNullOrEmpty(sort_order) ? "SensorID_desc" : "";
            ViewBag.TypeSort = sort_order == "Type" ? "Type_desc" : "Type";
            ViewBag.AddressIDSort = sort_order == "AddrID" ? "AddrID_desc" : "AddrID";
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
                var admin_sensors_query = db.Sensor_List_View as IEnumerable<Sensor_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    admin_sensors_query = admin_sensors_query.Where(s => s.Тип_датчика.ToLower().Contains(search_str)
                                                                      || s.ID_адреси.ToString().Contains(search_str)
                                                                      || s.Назва_вулиці.ToLower().Contains(search_str)
                                                                      || s.Район.ToLower().Contains(search_str)
                                                                      || s.Назва_закладу.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "SensorID_desc":
                        admin_sensors_query = admin_sensors_query.OrderByDescending(s => s.ID_датчика);
                        break;
                    case "Type":
                        admin_sensors_query = admin_sensors_query.OrderBy(s => s.Тип_датчика);
                        break;
                    case "Type_desc":
                        admin_sensors_query = admin_sensors_query.OrderByDescending(s => s.Тип_датчика);
                        break;
                    case "AddrID":
                        admin_sensors_query = admin_sensors_query.OrderBy(s => s.ID_адреси);
                        break;
                    case "AddrID_desc":
                        admin_sensors_query = admin_sensors_query.OrderByDescending(s => s.ID_адреси);
                        break;
                    case "Street":
                        admin_sensors_query = admin_sensors_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        admin_sensors_query = admin_sensors_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "District":
                        admin_sensors_query = admin_sensors_query.OrderBy(s => s.Район);
                        break;
                    case "District_desc":
                        admin_sensors_query = admin_sensors_query.OrderByDescending(s => s.Район);
                        break;
                    case "Establish":
                        admin_sensors_query = admin_sensors_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        admin_sensors_query = admin_sensors_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        admin_sensors_query = admin_sensors_query.OrderBy(s => s.ID_датчика);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(admin_sensors_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        public ActionResult AdminViewIndicators(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.DateSort = String.IsNullOrEmpty(sort_order) ? "Date_desc" : "";
            ViewBag.SensorSort = sort_order == "Sensor" ? "Sensor_desc" : "Sensor";
            ViewBag.TypeSort = sort_order == "Type" ? "Type_desc" : "Type";
            ViewBag.IndSort = sort_order == "Ind" ? "Ind_desc" : "Ind";
            ViewBag.AddressIDSort = sort_order == "AddrID" ? "AddrID_desc" : "AddrID";
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
                var admin_indicators_query = db.Indicators_List_View as IEnumerable<Indicators_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    admin_indicators_query = admin_indicators_query.Where(s => s.Дата_та_час.ToString().Contains(search_str)
                                                                            || s.ID_сенсору.ToString().Contains(search_str)
                                                                            || s.Тип_датчика.ToLower().Contains(search_str)
                                                                            || s.Показник.ToLower().Contains(search_str)
                                                                            || s.ID_адреси.ToString().Contains(search_str)
                                                                            || s.Назва_вулиці.ToLower().Contains(search_str)
                                                                            || s.Район.ToLower().Contains(search_str)
                                                                            || s.Назва_закладу.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "Date_desc":
                        admin_indicators_query = admin_indicators_query.OrderByDescending(s => s.Дата_та_час);
                        break;
                    case "Sensor":
                        admin_indicators_query = admin_indicators_query.OrderBy(s => s.ID_сенсору);
                        break;
                    case "Sensor_desc":
                        admin_indicators_query = admin_indicators_query.OrderByDescending(s => s.ID_сенсору);
                        break;
                    case "Type":
                        admin_indicators_query = admin_indicators_query.OrderBy(s => s.Тип_датчика);
                        break;
                    case "Type_desc":
                        admin_indicators_query = admin_indicators_query.OrderByDescending(s => s.Тип_датчика);
                        break;
                    case "Ind":
                        admin_indicators_query = admin_indicators_query.OrderBy(s => s.Показник);
                        break;
                    case "Ind_desc":
                        admin_indicators_query = admin_indicators_query.OrderByDescending(s => s.Показник);
                        break;
                    case "AddrID":
                        admin_indicators_query = admin_indicators_query.OrderBy(s => s.ID_адреси);
                        break;
                    case "AddrID_desc":
                        admin_indicators_query = admin_indicators_query.OrderByDescending(s => s.ID_адреси);
                        break;
                    case "Street":
                        admin_indicators_query = admin_indicators_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        admin_indicators_query = admin_indicators_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "District":
                        admin_indicators_query = admin_indicators_query.OrderBy(s => s.Район);
                        break;
                    case "District_desc":
                        admin_indicators_query = admin_indicators_query.OrderByDescending(s => s.Район);
                        break;
                    case "Establish":
                        admin_indicators_query = admin_indicators_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        admin_indicators_query = admin_indicators_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        admin_indicators_query = admin_indicators_query.OrderBy(s => s.Дата_та_час);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(admin_indicators_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        public ActionResult AdminViewAddresses(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.AddressIDSort = String.IsNullOrEmpty(sort_order) ? "AddrID_desc" : "";
            ViewBag.StreetSort = sort_order == "Street" ? "Street_desc" : "Street";
            ViewBag.DistrictSort = sort_order == "District" ? "District_desc" : "District";
            ViewBag.BuildingSort = sort_order == "Building" ? "Building_desc" : "Building";
            ViewBag.EstablishmentSort = sort_order == "Establish" ? "Establish_desc" : "Establish";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var admin_addresslist_query = db.Address_List_View as IEnumerable<Address_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    admin_addresslist_query = admin_addresslist_query.Where(s => s.ID_Вулиці.ToString().Contains(search_str)
                                                                              || s.Назва_вулиці.ToLower().Contains(search_str)
                                                                              || s.Район.ToLower().Contains(search_str)
                                                                              || s.Назва_закладу.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "AddrID_desc":
                        admin_addresslist_query = admin_addresslist_query.OrderByDescending(s => s.ID_Вулиці);
                        break;
                    case "Street":
                        admin_addresslist_query = admin_addresslist_query.OrderBy(s => s.Назва_вулиці);
                        break;
                    case "Street_desc":
                        admin_addresslist_query = admin_addresslist_query.OrderByDescending(s => s.Назва_вулиці);
                        break;
                    case "District":
                        admin_addresslist_query = admin_addresslist_query.OrderBy(s => s.Район);
                        break;
                    case "District_desc":
                        admin_addresslist_query = admin_addresslist_query.OrderByDescending(s => s.Район);
                        break;
                    case "Building":
                        admin_addresslist_query = admin_addresslist_query.OrderBy(s => s.Тип_будівлі);
                        break;
                    case "Building_desc":
                        admin_addresslist_query = admin_addresslist_query.OrderByDescending(s => s.Тип_будівлі);
                        break;
                    case "Establish":
                        admin_addresslist_query = admin_addresslist_query.OrderBy(s => s.Назва_закладу);
                        break;
                    case "Establish_desc":
                        admin_addresslist_query = admin_addresslist_query.OrderByDescending(s => s.Назва_закладу);
                        break;
                    default:
                        admin_addresslist_query = admin_addresslist_query.OrderBy(s => s.ID_Вулиці);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(admin_addresslist_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        public ActionResult AdminViewStreets(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.StreetIDSort = String.IsNullOrEmpty(sort_order) ? "StreetID_desc" : "";
            ViewBag.StreetSort = sort_order == "Street" ? "Street_desc" : "Street";
            ViewBag.DistrictIDSort = sort_order == "DistrictID" ? "DistrictID_desc" : "DistrictID";
            ViewBag.DistrictSort = sort_order == "District" ? "District_desc" : "District";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var admin_streetlist_query = db.Streets_List_View as IEnumerable<Streets_List_View>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    admin_streetlist_query = admin_streetlist_query.Where(s => s.ID_вулиці.ToString().Contains(search_str)
                                                                            || s.Вулиця.ToLower().Contains(search_str)
                                                                            || s.ID_району.ToString().Contains(search_str)
                                                                            || s.Район.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "StreetID_desc":
                        admin_streetlist_query = admin_streetlist_query.OrderByDescending(s => s.ID_вулиці);
                        break;
                    case "Street":
                        admin_streetlist_query = admin_streetlist_query.OrderBy(s => s.Вулиця);
                        break;
                    case "Street_desc":
                        admin_streetlist_query = admin_streetlist_query.OrderByDescending(s => s.Вулиця);
                        break;
                    case "DistrictID":
                        admin_streetlist_query = admin_streetlist_query.OrderBy(s => s.ID_району);
                        break;
                    case "DistrictID_desc":
                        admin_streetlist_query = admin_streetlist_query.OrderByDescending(s => s.ID_району);
                        break;
                    case "District":
                        admin_streetlist_query = admin_streetlist_query.OrderBy(s => s.Район);
                        break;
                    case "District_desc":
                        admin_streetlist_query = admin_streetlist_query.OrderByDescending(s => s.Район);
                        break;
                    default:
                        admin_streetlist_query = admin_streetlist_query.OrderBy(s => s.ID_вулиці);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(admin_streetlist_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }

        public ActionResult AdminViewDistricts(string sort_order, string current_filter, string search_str, int? page)
        {
            ViewBag.CurrentSort = sort_order;

            ViewBag.DistrictIDSort = String.IsNullOrEmpty(sort_order) ? "DistrictID_desc" : "";
            ViewBag.DistrictSort = sort_order == "District" ? "District_desc" : "District";

            if (search_str != null)       //If searchbar is not null -> first page
                page = 1;
            else
                search_str = current_filter;

            ViewBag.CurrentFilter = search_str;

            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var admin_districtlist_query = db.Districts as IEnumerable<District>;

                if (!String.IsNullOrEmpty(search_str))        //searching bar 
                {
                    string sstr = search_str.ToLower();
                    admin_districtlist_query = admin_districtlist_query.Where(s => s.DistrictID.ToString().Contains(search_str)
                                                                                || s.DistrictName.ToLower().Contains(search_str));
                }

                switch (sort_order)      //page sorting switch
                {
                    case "DistrictID_desc":
                        admin_districtlist_query = admin_districtlist_query.OrderByDescending(s => s.DistrictID);
                        break;
                    case "District":
                        admin_districtlist_query = admin_districtlist_query.OrderBy(s => s.DistrictName);
                        break;
                    case "District_desc":
                        admin_districtlist_query = admin_districtlist_query.OrderByDescending(s => s.DistrictName);
                        break;
                    default:
                        admin_districtlist_query = admin_districtlist_query.OrderBy(s => s.DistrictID);
                        break;
                }

                int pagesize = 10;
                int pagenumber = (page ?? 1);

                return View(admin_districtlist_query.ToList().ToPagedList(pagenumber, pagesize));
            }
        }


        /// Maintenance edit methods
        public ActionResult AdminDeleteMaint(int? MaintID)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities()) 
            {
                try
                {
                    var maintenance = db.Maintenances.Find(MaintID);
                    db.Maintenances.Remove(maintenance);
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("AdminViewMaintenance");
                }

                return RedirectToAction("AdminViewMaintenance");
            }
        }
        [HttpGet]           
        public ActionResult AdminAddNewMaintenance()
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var address_query = from addr in db.Addresses select addr;
                var address_view = address_query
                    .Include(addr => addr.Street1)
                    .Include(addr => addr.Building_types)
                    .Include(addr => addr.Street1.District1) as IEnumerable<Address>;

                var address_list = address_view.Select(addrs => new
                {
                    MaintAddress = addrs.AddressID,
                    displayName = $"{addrs.Street1.District1.DistrictName} район, вулиця {addrs.Street1.StreetName} {addrs.House}, квартира {addrs.Flat}, офіс {addrs.Office}, тип будинку: {addrs.Building_types.BuildingTypeName}, {addrs.EstablishmentName}"
                }).ToList();

                SelectList select_addresses = new SelectList(address_list, "MaintAddress", "displayName");
                ViewData["Addresses"] = select_addresses;

                var engineer_query = db.Users.Where(e => e.UserRole == 2).ToList() as IEnumerable<User>;

                var engineer_view = engineer_query.Select(eng => new
                {
                    StaffUserID = eng.UserID,
                    displayName = $"{eng.Surname} {eng.PrsnName} {eng.SecondName} ({eng.UserLogin}, {eng.BirthDate}, {eng.PhoneNumber})"
                }).ToList();

                SelectList select_engineers = new SelectList(engineer_view, "StaffUserID", "displayName");
                ViewData["Engineers"] = select_engineers;

                return View();
            }
        }
        [HttpPost]
        public ActionResult AdminAddNewMaintenance(Maintenance maintenance) 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                db.Maintenances.Add(maintenance);
                db.SaveChanges();
                return RedirectToAction("AdminViewMaintenance");
            }
        }


        /// User edit methods
        public ActionResult AdminDeleteUser(int? UserID)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities()) 
            {
                try
                {
                    var user = db.Users.Find(UserID);
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                catch 
                {
                    return RedirectToAction("AdminViewUsers");
                }

                return RedirectToAction("AdminViewUsers");
            }
        }
        [HttpGet]
        public ActionResult AdminAddNewUser()
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var address_query = from addr in db.Addresses select addr;
                var address_view = address_query
                    .Include(addr => addr.Street1)
                    .Include(addr => addr.Building_types)
                    .Include(addr => addr.Street1.District1) as IEnumerable<Address>;

                var address_list = address_view.Select(addrs => new
                {
                    AddressInfo = addrs.AddressID,
                    displayName = $"{addrs.Street1.District1.DistrictName} район, вулиця {addrs.Street1.StreetName} {addrs.House}, квартира {addrs.Flat}, офіс {addrs.Office}, тип будинку: {addrs.Building_types.BuildingTypeName}, {addrs.EstablishmentName}"
                }).ToList();

                SelectList select_addresses = new SelectList(address_list, "AddressInfo", "displayName");
                ViewData["Addresses"] = select_addresses;

                var gendertype_query = db.Users.Select(item => item.Gender).Distinct().ToList();

                SelectList select_genders = new SelectList(gendertype_query, "Gender");
                ViewData["Genders"] = select_genders;

                SelectList select_user_roles = new SelectList(db.UserRoles.ToList(), "RoleID", "RoleName");
                ViewData["UserRoles"] = select_user_roles;

                return View();
            }
        }
        [HttpPost]
        public ActionResult AdminAddNewUser(User user) 
        {
            if (ModelState.IsValid)
            {
                using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
                {
                    try
                    {
                        if (user.UserRole == 1 || user.UserRole == 2)
                        {
                            user.AddressInfo = null;
                        }

                        db.Users.Add(user);
                        db.SaveChanges();
                        return RedirectToAction("AdminViewUsers");
                    }
                    catch
                    {
                        ViewBag.Message = "Дані введені невірно, або такий логін/пароль вже існує";
                    }                   
                }
            }
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var address_query = from addr in db.Addresses select addr;
                var address_view = address_query
                    .Include(addr => addr.Street1)
                    .Include(addr => addr.Building_types)
                    .Include(addr => addr.Street1.District1) as IEnumerable<Address>;

                var address_list = address_view.Select(addrs => new
                {
                    AddressInfo = addrs.AddressID,
                    displayName = $"{addrs.Street1.District1.DistrictName} район, вулиця {addrs.Street1.StreetName} {addrs.House}, квартира {addrs.Flat}, офіс {addrs.Office}, тип будинку: {addrs.Building_types.BuildingTypeName}, {addrs.EstablishmentName}"
                }).ToList();

                SelectList select_addresses = new SelectList(address_list, "AddressInfo", "displayName");
                ViewData["Addresses"] = select_addresses;

                var gendertype_query = db.Users.Select(item => item.Gender).Distinct().ToList();

                SelectList select_genders = new SelectList(gendertype_query, "Gender");
                ViewData["Genders"] = select_genders;

                SelectList select_user_roles = new SelectList(db.UserRoles.ToList(), "RoleID", "RoleName");
                ViewData["UserRoles"] = select_user_roles;
            }
            return View();
        }


        /// Sensor edit methods
        public ActionResult AdminDeleteSensor(int? SensorID)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                try
                {
                    var sensor = db.Sensors.Find(SensorID);
                    db.Sensors.Remove(sensor);
                    db.SaveChanges();
                }
                catch 
                {
                    return RedirectToAction("AdminViewSensors");
                }

                return RedirectToAction("AdminViewSensors");   
            }            
        }
        [HttpGet]
        public ActionResult AdminAddNewSensor()
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var address_query = from addr in db.Addresses select addr;
                var address_view = address_query
                    .Include(addr => addr.Street1)
                    .Include(addr => addr.Building_types)
                    .Include(addr => addr.Street1.District1) as IEnumerable<Address>;

                var address_list = address_view.Select(addrs => new 
                {
                    SensorAddress = addrs.AddressID,
                    displayName = $"{addrs.Street1.District1.DistrictName} район, вулиця {addrs.Street1.StreetName} {addrs.House}, квартира {addrs.Flat}, офіс {addrs.Office}, тип будинку: {addrs.Building_types.BuildingTypeName}"
                }).ToList();

                SelectList select_addresses = new SelectList(address_list, "SensorAddress", "displayName");
                ViewData["Addresses"] = select_addresses;

                SelectList select_sensortypes = new SelectList(db.SensorTypes.ToList(), "SensorTypeID", "SensorTypeName");
                ViewData["SensorTypes"] = select_sensortypes;

                return View();
            }
        }
        [HttpPost]
        public ActionResult AdminAddNewSensor(Sensor sensor) 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                db.Sensors.Add(sensor);
                db.SaveChanges();
                return RedirectToAction("AdminViewSensors");
            }
        }


        /// Indicator edit methods
        public ActionResult AdminDeleteIndicator(DateTime IndDate, int? Sensor)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities()) 
            {
                var indicatorID = db.Indicators.First(i => i.IndDate == IndDate && i.Sensor == Sensor).IndicatorID;

                try
                {
                    var indicator = db.Indicators.Find(indicatorID);
                    db.Indicators.Remove(indicator);
                    db.SaveChanges();   
                }
                catch 
                {
                    return RedirectToAction("AdminViewIndicators");   
                }

                return RedirectToAction("AdminViewIndicators");
            }
        }


        /// Address edit methods
        public ActionResult AdminChangeAddressInfo(int? AddressID)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var address = db.Addresses.Find(AddressID);

                if (address != null)
                {
                    var streets_query = from str in db.Streets select str;
                    var streets_view = streets_query
                        .Include(str => str.District1) as IEnumerable<Street>;

                    var streetslist = streets_view.Select(strt => new
                    {
                        Street = strt.StreetID,
                        displayName = $"Вулиця {strt.StreetName}, {strt.District1.DistrictName} район"
                    }).ToList();

                    SelectList select_streets = new SelectList(streetslist, "Street", "displayName");
                    ViewData["Streets"] = select_streets;

                    SelectList select_buildingtypes = new SelectList(db.Building_types.ToList(), "BuildingTypeID", "BuildingTypeName");
                    ViewData["BuildingTypes"] = select_buildingtypes;

                    return View(address);
                }
                return RedirectToAction("AdminViewAddresses");
            }
        }
        [HttpPost]
        public ActionResult AdminChangeAddressInfo(Address new_address) 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var address = db.Addresses.Find(new_address.AddressID);

                if (address != null)
                {
                    address.Street = new_address.Street;
                    address.House = new_address.House;
                    address.Flat = new_address.Flat;    
                    address.Office = new_address.Office;
                    address.BuildingType = new_address.BuildingType;    
                    address.EstablishmentName = new_address.EstablishmentName;
                    
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ViewBag.Message = "Помилка під час зміни значень";
                        return View(address.AddressID);
                    }
                }
                return RedirectToAction("AdminViewAddresses");
            }
        }
        public ActionResult AdminDeleteAddress(int? AddressID)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                try
                {
                    var address = db.Addresses.Find(AddressID);
                    db.Addresses.Remove(address);
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("AdminViewAddresses");
                }

                return RedirectToAction("AdminViewAddresses");
            }
        }
        [HttpGet]
        public ActionResult AdminAddNewAddress()
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var streets_query = from str in db.Streets select str;
                var streets_view = streets_query
                    .Include(str => str.District1) as IEnumerable<Street>;

                var streetslist = streets_view.Select(strt => new
                {
                    Street = strt.StreetID,
                    displayName = $"Вулиця {strt.StreetName}, {strt.District1.DistrictName} район"
                }).ToList();

                SelectList select_streets = new SelectList(streetslist, "Street", "displayName");
                ViewData["Streets"] = select_streets;

                SelectList select_buildingtypes = new SelectList(db.Building_types.ToList(), "BuildingTypeID", "BuildingTypeName");
                ViewData["BuildingTypes"] = select_buildingtypes;

                return View();
            }
        }
        [HttpPost]
        public ActionResult AdminAddNewAddress(Address address) 
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("AdminViewAddresses");
            }
        }


        /// Street edit methods
        public ActionResult AdminChangeStreetInfo(int? StreetID)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var street = db.Streets.Find(StreetID);

                if (street != null)
                {
                    SelectList select_districts = new SelectList(db.Districts.ToList(), "DistrictID", "DistrictName");
                    ViewData["Districts"] = select_districts;
                    return View(street);
                }
                return RedirectToAction("AdminViewStreets");
            }
        }
        [HttpPost]
        public ActionResult AdminChangeStreetInfo(Street new_street)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var street = db.Streets.Find(new_street.StreetID);

                if (street != null)
                {
                    var check = db.Streets.FirstOrDefault(a => a.StreetName == new_street.StreetName);

                    if (check != null)
                    {
                        ViewBag.Message = "Така вулиця вже існує";

                        SelectList select_districts = new SelectList(db.Districts.ToList(), "DistrictID", "DistrictName");
                        ViewData["Districts"] = select_districts;

                        return View(street);
                    }
                    
                    try
                    {
                        street.StreetName = new_street.StreetName;
                        street.District = new_street.District;
                        db.SaveChanges();
                    }
                    catch
                    {
                        ViewBag.Message = "Дані введено невірно, або такий район вже існує";

                        SelectList select_districts = new SelectList(db.Districts.ToList(), "DistrictID", "DistrictName");
                        ViewData["Districts"] = select_districts;

                        return View(street);
                    }
                }
                return RedirectToAction("AdminViewStreets");
            }
        }
        public ActionResult AdminDeleteStreet(int? StreetID)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                try
                {
                    var street = db.Streets.Find(StreetID);
                    db.Streets.Remove(street);
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("AdminViewStreets");
                }

                return RedirectToAction("AdminViewStreets");
            }
        }
        [HttpGet]
        public ActionResult AdminAddNewStreet()
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                SelectList select_districts = new SelectList(db.Districts.ToList(), "DistrictID", "DistrictName");
                ViewData["Districts"] = select_districts;
                return View();
            }            
        }
        [HttpPost]
        public ActionResult AdminAddNewStreet(Street street)
        {
            if (ModelState.IsValid)
            {
                using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
                {
                    try
                    {
                        db.Streets.Add(street);
                        db.SaveChanges();
                        return RedirectToAction("AdminViewStreets");
                    }
                    catch
                    {
                        ViewBag.Message = "Дані введено невірно, або така вулиця вже існує";
                    }
                }
            }
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                SelectList select_districts = new SelectList(db.Districts.ToList(), "DistrictID", "DistrictName");
                ViewData["Districts"] = select_districts;                
            }
            return View();
        }


        /// District edit methods
        public ActionResult AdminChangeDistrictInfo(int? DistrictID)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var district = db.Districts.Find(DistrictID);

                if (district != null)
                {
                    return View(district);
                }
                return RedirectToAction("AdminViewDistricts");
            }
        }
        [HttpPost]
        public ActionResult AdminChangeDistrictInfo(District new_district)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var district = db.Districts.Find(new_district.DistrictID);
                
                if (district != null)
                {
                    var check = db.Districts.FirstOrDefault(a => a.DistrictName == new_district.DistrictName);
                    
                    if (check != null)
                    {
                        ViewBag.Message = "Такий район вже існує";
                        return View(district);
                    }                    

                    try
                    {
                        district.DistrictName = new_district.DistrictName;
                        db.SaveChanges();
                    }
                    catch
                    {
                        ViewBag.Message = "Дані введено невірно, або такий район вже існує";
                        return View(district);
                    }
                }
                return RedirectToAction("AdminViewDistricts");
            }
        }        
        public ActionResult AdminDeleteDistrict(int? DistrictID)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                try
                {
                    var district = db.Districts.Find(DistrictID);
                    db.Districts.Remove(district);
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("AdminViewDistricts");
                }

                return RedirectToAction("AdminViewDistricts");
            }
        }
        public ActionResult AdminAddNewDistrict()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminAddNewDistrict(District district) 
        {
            if (ModelState.IsValid)
            {
                using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
                {
                    try
                    {
                        db.Districts.Add(district);
                        db.SaveChanges();
                        return RedirectToAction("AdminViewDistricts");
                    }
                    catch 
                    {
                        ViewBag.Message = "Дані введено невірно, або такий район вже існує";
                    }                   
                }
            }            
            return View();
        }
    }
}