using BlogMVCBoost.Models;
using BlogMVCBoost.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BlogMVCBoost.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private static readonly string roleName = "User";
        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM p)
        {

            AppUser appUser = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Mail,
                UserName = p.Username
            };
            if (p.Password == p.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, p.Password);

                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var role = new AppRole() {Id= Guid.NewGuid().ToString(), Name = roleName };
                    await _roleManager.CreateAsync(role);
                }

                await _userManager.AddToRoleAsync(appUser, roleName);

                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(p);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM p)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, false, true);

                if (result.Succeeded)
                {
                    return RedirectToAction("ProfilePage", "Profile");
                }
                else
                {
                    return RedirectToAction("SignIn", "Login");
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }
        
            



            public void SendVerificationEmail(string toAddress, string verificationLink)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Your Name", "your-email@your-domain.com"));
                message.To.Add(new MailboxAddress("", toAddress));
                message.Subject = "Account Verification";
                message.Body = new TextPart("plain")
                {
                    Text = "Please click the following link to verify your account: " + verificationLink
                };



                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.your-email-provider.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("your-email@your-domain.com", "your-email-password");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
        
    }

}

