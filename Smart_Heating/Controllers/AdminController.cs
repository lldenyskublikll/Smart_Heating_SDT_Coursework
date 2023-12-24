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












        public ActionResult AdminChangeMaintInfo() 
        {
            return View();
        }
        public ActionResult AdminDeleteMaint()
        {
            return View();
        }
        public ActionResult AdminAddNewMaintenance()
        {
            return View();
        }


        public ActionResult AdminChangeUserInfo()
        {
            return View();
        }
        public ActionResult AdminDeleteUser()
        {
            return View();
        }
        public ActionResult AdminAddNewUser()
        {
            return View();
        }


        public ActionResult AdminDeleteSensor()
        {
            return View();
        }
        public ActionResult AdminAddNewSensor()
        {
            return View();
        }


        public ActionResult AdminDeleteIndicator()
        {
            return View();
        }


        public ActionResult AdminChangeAddressInfo()
        {
            return View();
        }
        public ActionResult AdminDeleteAddress()
        {
            return View();
        }
        public ActionResult AdminAddNewAddress()
        {
            return View();
        }


        public ActionResult AdminChangeStreetInfo()
        {
            return View();
        }
        public ActionResult AdminDeleteStreet()
        {
            return View();
        }
        public ActionResult AdminAddNewStreet()
        {
            return View();
        }


        public ActionResult AdminChangeDistrictInfo()
        {
            return View();
        }
        public ActionResult AdminDeleteDistrict()
        {
            return View();
        }
        public ActionResult AdminAddNewDistrict()
        {
            return View();
        }
    }
}