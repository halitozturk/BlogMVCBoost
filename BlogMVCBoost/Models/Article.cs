using System.Reflection;

namespace BlogMVCBoost.Models
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string ArticleName { get; set; }
        public string ArticleBody { get; set; }
        public string ArticleContent { get; set; }
        public string? ArticleImage { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool Status { get; set; } = true;

        //Nav Props
        public string AppUserID { get; set; }
        public AppUser AppUser { get; set; }
    }
}
