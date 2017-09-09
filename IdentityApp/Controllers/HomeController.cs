using IdentityApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}