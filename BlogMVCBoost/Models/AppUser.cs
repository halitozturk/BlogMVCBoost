using Microsoft.AspNetCore.Identity;

namespace BlogMVCBoost.Models
{
    public class AppUser : IdentityUser<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        //Nav Props
        public virtual ICollection<Article> Articles { get; set; }
    }
}
