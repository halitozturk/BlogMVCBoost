using BlogMVCBoost.Models;

namespace BlogMVCBoost.ViewModels
{
    public class ArticleUserVM
    {
        public List<Article> Articles { get; set; }
        public Article Article { get; set; }
        public List<AppUser> Users { get; set; }
        public AppUser User { get; set; }
    }
}
