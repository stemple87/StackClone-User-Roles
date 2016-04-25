using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using StackOverflow.Models;


namespace StackOverflow.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signIngManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signIngManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
