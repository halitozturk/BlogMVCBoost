using BlogMVCBoost.Context;
using BlogMVCBoost.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<BoostBlogDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BoostBlogDbContext>();

builder.Services.AddIdentity<AppUser, AppRole>()
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<BoostBlogDbContext>()
                .AddEntityFrameworkStores<BoostBlogDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
