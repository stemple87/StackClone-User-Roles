using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StackOverflow.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http.Internal;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Mvc.Rendering;

namespace StackOverflow.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RolesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var roles = _db.Roles.ToList();
            return View(roles);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string RoleName)
        {
            try
            {
                _db.Roles.Add(new IdentityRole()
                {
                    Name = RoleName 
                });
                _db.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                return View();
            }
        }

        //Delete
        public IActionResult Delete(string RoleName)
        {
            var thisRole = _db.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
              _db.Roles.Remove(thisRole);
              _db.SaveChanges();
              return RedirectToAction("Index");
        }
        
        public IActionResult Edit(string roleName)
        {
            var thisRole = _db.Roles
                .Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();

            return View(thisRole);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditRole(string roleName)
        {
            try
            {
                var currentRole = _db.Roles.FirstOrDefault(m =>m.Name == roleName);
                currentRole.Name = Request.Form["Name"];
                _db.Roles.Update(currentRole);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult ManageUserRoles()
        {
            var list = _db.Roles.OrderBy(r => r.Name)
                .ToList()
                .Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            ViewBag.Roles = list;

            return View();
        }
    }
}
