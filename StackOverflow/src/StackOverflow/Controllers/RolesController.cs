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

    }
}
