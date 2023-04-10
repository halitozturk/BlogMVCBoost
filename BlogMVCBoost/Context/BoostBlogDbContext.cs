using BlogMVCBoost.Configurations;
using BlogMVCBoost.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogMVCBoost.Context
{
    public class BoostBlogDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=DESKTOP-S2C7UGO;Database=BoostBlogDb;User ID=sa;Password=arkadas1");
            optionsBuilder.UseSqlServer("Server=DESKTOP-81CS8R3;Database=BoostBlogDb;User ID=sa;Password=3157261Ho");
            base.OnConfiguring(optionsBuilder);
        }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ArticleConfiguration());
            builder.ApplyConfiguration(new AppUserConfiguration());
            base.OnModelCreating(builder);
        }
        public DbSet<Article> Articles { get; set; }
    }
}
