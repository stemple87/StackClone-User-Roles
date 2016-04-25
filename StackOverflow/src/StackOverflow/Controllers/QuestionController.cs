using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using StackOverflow.Models;
using StackOverflow.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;

namespace StackOverflow.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db
        )
        {
            _userManager = userManager;
            _db = db;
        }
    
        public IActionResult Index()
        {
            var allQuestions = _db.Questions.ToList();
            return View(allQuestions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            question.User = currentUser;
            _db.Questions.Add(question);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var questionDetail = _db.Questions.Include(x => x.Answers).ToList()
                .FirstOrDefault(x => x.QuestionId == id);
                
            return View(questionDetail); 
        }

        //public IActionResult Answer(int id)
        //{
        //    ViewBag.question = id;

        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Answer(Answer answer)
        //{
        //    var QuestionId = QuestionId

        //    var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
        //    answer.User = currentUser;
        //    answer.Question = QuestionId;
        //    _db.Answers.Add(answer);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}
