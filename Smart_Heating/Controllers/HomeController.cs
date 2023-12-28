using Smart_Heating.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Smart_Heating.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LoginPage() 
        {
            return View();  /// Returns the LoginPage view for user validation
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPage(User objUser)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                /// Searching for user with the same Login an Password as the entered credentials
                var obj = db.Users.Where(a => a.UserLogin.Equals(objUser.UserLogin.ToString()) && a.UserPassword.Equals(objUser.UserPassword.ToString()));

                /*var obj = db.Database.SqlQuery<User>("SELECT * FROM Users WHERE UserLogin = @UserLogin AND UserPassword = @UserPassword", 
                                                     new SqlParameter("UserLogin", objUser.UserLogin),
                                                     new SqlParameter("UserPassword", objUser.UserPassword)).First();*/
                
                /// If there's any saved user with the same credentials -- return to LoginPage view
                if (!obj.Any())
                {
                    ViewBag.Message = "Credentials is not valid";
                    return View();
                }
                /// Else -- save some user's data as session parameters:
                Session["UserID"] = obj.First().UserID.ToString();                     /// ID of this user
                Session["FirstName"] = obj.First().PrsnName.ToString();                /// Name of this user
                Session["Role"] = obj.First().UserRole.ToString();                     /// System role of this user
                Session["Address"] = obj.First().AddressInfo.ToString();               /// Information of the user's address (id of the address)
                Session["RoleName"] = obj.First().UserRole1.RoleName.ToString();

                /// And then redirect to the Application controller where will be user's system role check
                return RedirectToAction("UserDashBoard", "Application");
            }
        }

        [HttpGet]
        public ActionResult Register()
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

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User objUser) /// Saving information about new user
        {
            if (ModelState.IsValid)
            {
                using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
                {
                    try
                    {
                        db.Users.Add(objUser);
                        db.SaveChanges();
                        Session["UserID"] = objUser.UserID.ToString();
                        Session["FirstName"] = objUser.UserLogin.ToString();
                        Session["Role"] = objUser.UserRole.ToString();

                        int.TryParse(Session["Role"].ToString(), out int id);
                        Session["RoleName"] = db.UserRoles.First(r => r.RoleID == id).RoleName.ToString();

                        Session["Address"] = objUser.AddressInfo.ToString();

                        return RedirectToAction("UserDashBoard", "Application");
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
            }
            return View();
        }

        /// Logs out of user's "cabinet" and shows LoginPage view
        public ActionResult Logout()
        {
            Session.Clear();
            return View("LoginPage");
        }
    }



    /// Attribute for Admin Role verification on Admin view
    public class AdminCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "1")  /// if role is not Administrator then redirect on System Role check Actionresult
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "controller", "Application" },
                        { "action", "UserDashBoard" }
                    }
                );
            }

            base.OnActionExecuting(filterContext);
        }
    }

    
    /// Attribute for Staff Role verification on Staff view/
    public class StaffCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "2")  /// if role is not Staff then redirect on System Role check Actionresult
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "controller", "Application" },
                        { "action", "UserDashBoard" }
                    }
                );
            }

            base.OnActionExecuting(filterContext);
        }
    }

    
    /// Attribute for OMS Role verification on OMS view
    public class OMSCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "3")  /// if role is not OMS User then redirect on System Role check Actionresult
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "controller", "Application" },
                        { "action", "UserDashBoard" }
                    }
                );
            }

            base.OnActionExecuting(filterContext);
        }
    }

    
    /// Attribute for ORS Role verification on ORS view
    public class ORSCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "4")  /// if role is not ORS User then redirect on System Role check Actionresult
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "controller", "Application" },
                        { "action", "UserDashBoard" }
                    }
                );
            }

            base.OnActionExecuting(filterContext);
        }
    }

    
    /// Attribute for OSBB Role verification on OBB view
    public class OSBBCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "5")  /// if role is not OSBB User then redirect on System Role check Actionresult
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "controller", "Application" },
                        { "action", "UserDashBoard" }
                    }
                );
            }

            base.OnActionExecuting(filterContext);
        }
    }
    
    
    /// Attribute for Standart user Role verification on Standart User view
    public class StandartUserCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "6")  /// if role is not Standart User then redirect on System Role check Actionresult
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "controller", "Application" },
                        { "action", "UserDashBoard" }
                    }
                );
            }

            base.OnActionExecuting(filterContext);
        }
    }
}