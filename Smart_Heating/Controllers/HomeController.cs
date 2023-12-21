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
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPage(User objUser)
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                //var obj = db.Users.Where(a => a.UserLogin.Equals(objUser.UserLogin) && a.UserPassword.Equals(objUser.UserPassword));

                var obj = db.Users.Where(a => a.UserLogin.Equals(objUser.UserLogin.ToString()) && a.UserPassword.Equals(objUser.UserPassword.ToString()));

                /*var obj = db.Database.SqlQuery<User>("SELECT * FROM Users WHERE UserLogin = @UserLogin AND UserPassword = @UserPassword", 
                                                     new SqlParameter("UserLogin", objUser.UserLogin),
                                                     new SqlParameter("UserPassword", objUser.UserPassword)).First();*/
                
                if (!obj.Any())
                {
                    ViewBag.Message = "Credentials is not valid";
                    return View();
                }
                Session["UserID"] = obj.First().UserID.ToString();
                Session["FirstName"] = obj.First().PrsnName.ToString();
                Session["Role"] = obj.First().UserRole.ToString();
                return RedirectToAction("UserDashBoard", "Application");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {

                var adrquery = from adr in db.Addresses select adr;
                var adrview = adrquery
                    .Include(adr => adr.Street1)
                    .Include(adr => adr.Street1.District1) as IEnumerable<Address>;

                
                var addressList = adrview.Select(addr => new {
                    AddressInfo = addr.AddressID,
                    displayName = $"{addr.Street1.StreetName}, {addr.Street1.District1.DistrictName}"
                }).ToList();

                SelectList selectaddress = new SelectList(addressList, "AddressInfo", "displayName");

                ViewData["addressList"] = selectaddress;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User objUser)
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
                        return RedirectToAction("UserDashBoard", "Application");
                    }
                    catch
                    {
                        ViewBag.Message = "Credentials is not valid";
                    }
                }
            }
            using (SMART_HEATINGEntities db = new SMART_HEATINGEntities())
            {
                var addresslist = db.Addresses.OrderBy(item => item.AddressID).ToList();
                ViewData["addresslist"] = addresslist;
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return View("LoginPage");
        }

    }



    /// <summary>
    /// Attribute for Admin Role verification on view
    /// </summary>
    public class AdminCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "1")
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

    /// <summary>
    /// Attribute for Staff Role verification on view
    /// </summary>
    public class StaffCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "2")
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

    /// <summary>
    /// Attribute for OMS Role verification on view
    /// </summary>
    public class OMSCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "3")
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

    /// <summary>
    /// Attribute for ORS Role verification on view
    /// </summary>
    public class ORSCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "4")
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

    /// <summary>
    /// Attribute for OSBB Role verification on view
    /// </summary>
    public class OSBBCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "5")
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

    /// <summary>
    /// Attribute for Standart user Role verification on view
    /// </summary>
    public class StandartUserCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = filterContext.HttpContext.Session["Role"];

            if (role == null || role.ToString() != "6")
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