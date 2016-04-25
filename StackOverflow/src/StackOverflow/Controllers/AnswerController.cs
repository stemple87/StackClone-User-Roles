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
    public class AnswerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public AnswerController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db
        )
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Answer(int id)
        {
            ViewBag.question = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Answer(Answer answer)
        {
            var theId = ViewBag.question;

            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());

            //var currentQuestion = await _userManager.FindByIdAsync(User.GetQuestionId());

            answer.User = currentUser;
            //answer.Question = QuestionId;
            _db.Answers.Add(answer);
            _db.SaveChanges();
            return RedirectToAction("Details", "Question", new { id = answer.QuestionId});
            //return View("Question", "Index");
        }

    }
}
