using BlogMVCBoost.Context;
using BlogMVCBoost.Models;
using BlogMVCBoost.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BlogMVCBoost.Controllers
{
    public class ArticleController : Controller
    {
        BoostBlogDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public ArticleController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher, BoostBlogDbContext db)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _db = db;
        }
        public IActionResult Write()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Write(ArticleVM a)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            Article article = new Article()
            {
                AppUserID = user.Id,
                ArticleName = a.ArticleName,
                ArticleBody = a.ArticleBody,
                ArticleImage = a.ArticleImage
            };
            _db.Articles.Add(article);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> MyArticleList()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(_db.Articles.ToList().Where(u => u.AppUserID == user.Id));
        }

        public IActionResult UpdateArticle(int id)
        {
            var article = _db.Articles.FirstOrDefault(x => x.ArticleID == id);
            ArticleVM articleVM = new ArticleVM()
            {
                ArticleBody=article.ArticleBody,
                ArticleImage=article.ArticleImage,
                ArticleName = article.ArticleName,
            };
            if (article == null) { return NotFound(); }
            return View(articleVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateArticle(int id, ArticleVM p)
        {
            var article = _db.Articles.Find(id);
            article.ArticleName = p.ArticleName;
            article.ArticleBody = p.ArticleBody;
            article.ArticleImage = p.ArticleImage;
            article.UpdatedTime = DateTime.Parse(DateTime.Now.ToString("dd-MMM-yyyy"));
            _db.Articles.Update(article);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = _db.Articles.Find(id);
            _db.Remove(article);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ReadArticle(int id)
        {
            ArticleUserVM articleUserVM = new ArticleUserVM()
            {
                Article = _db.Articles.Find(id)
            };            
            return View(articleUserVM);
        }
    }
}
