using BlogMVCBoost.Context;
using BlogMVCBoost.Models;
using BlogMVCBoost.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogMVCBoost.Controllers
{
    public class HomeController : Controller
    {
        private readonly BoostBlogDbContext _db;
        public HomeController(BoostBlogDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ArticleUserVM ArticleUserVM = new ArticleUserVM()
            {
                Articles = _db.Articles.ToList(),
                Users = _db.Users.ToList(),
            };

            return View(ArticleUserVM);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}