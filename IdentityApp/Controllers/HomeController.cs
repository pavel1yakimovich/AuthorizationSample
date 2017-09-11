using IdentityApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace IdentityApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                IQueryable<ApplicationUser> usersWithRoles = ctx.Users.Include(u => u.Roles);

                ViewData["users"] = usersWithRoles.ToList();
                ViewData["roles"] = ctx.Roles.ToList();
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult About()
        {
            ViewBag.Message = "For admin only";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = GetInfo();

            return View();
        }

        public string GetInfo()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;
            var gender = identity.Claims.Where(c => c.Type == ClaimTypes.Gender).Select(c => c.Value).SingleOrDefault();
            var age = identity.Claims.Where(c => c.Type == "age").Select(c => c.Value).SingleOrDefault();
            return "Email: " + email + " Gender:" + gender + " Age:" + age;
        }
    }
}