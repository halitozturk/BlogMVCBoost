using BlogMVCBoost.DTOs;
using BlogMVCBoost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVCBoost.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public ProfileController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> ProfilePage()
        {
            string name = User.Identity.Name;
            AppUser user = await _userManager.FindByNameAsync(name);

            if (user != null)
            {
                UserUpdateDTO userUpdateDTO = new UserUpdateDTO()
                {
                    Name=user.Name,
                    Surname=user.Surname,
                    Username = user.UserName,
                    Email = user.Email,
                };
                return View(userUpdateDTO);
            }
            else
                return RedirectToAction("ProfilePage");
        }

        [HttpPost]
        public async Task<IActionResult> ProfilePage(UserUpdateDTO userUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(userUpdateDTO.Username);
                user.UserName = userUpdateDTO.Username;
                user.Email = userUpdateDTO.Email;
                user.Name = userUpdateDTO.Name;
                user.Surname= userUpdateDTO.Surname;

                if (userUpdateDTO.Password != null)
                    user.PasswordHash = _passwordHasher.HashPassword(user, userUpdateDTO.Password);

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("ProfilePage");
                else
                    Errors(result);
            }

            return View(userUpdateDTO);
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                TempData["Error"] = $"{error.Code} - {error.Description}";
            }
        }

    }


}

