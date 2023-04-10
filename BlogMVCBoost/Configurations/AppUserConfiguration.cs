using BlogMVCBoost.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMVCBoost.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Articles).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserID);
        }
    }
}
