using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using X.Models;
using X.Models.Interfaces;
using X.Models.Repositories;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace X.Controllers
{
    public class ProfileController : Controller
    {
        private IBlogRepository _blogRepository;
        private readonly UserManager<User> _userManager;

        public ProfileController(IBlogRepository blogRepository, UserManager<User> userManager)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            User user = new User();
            return View(user);
        }

        public async Task<IActionResult> Dashboard()
        {
            string userId = _userManager.GetUserId(User);
            var blogs = await _blogRepository.GetMyBlogs(userId);
            var model = new DashboardViewModel
            {
                blogs = blogs
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(string fullName, string phoneNumber, string aboutMe)
        {
            if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3)
            {
                ModelState.AddModelError("FullName", "Full name must be at least 3 characters long.");
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Length < 10)
            {
                ModelState.AddModelError("PhoneNumber", "Phone number must be at least 10 digits long.");
            }

            if (!string.IsNullOrWhiteSpace(aboutMe) && aboutMe.Length > 500)
            {
                ModelState.AddModelError("AboutMe", "About me section cannot exceed 500 characters.");
            }

            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            currentUser.FullName = fullName;
            currentUser.Phone = phoneNumber;
            currentUser.About = aboutMe;

            var result = await _userManager.UpdateAsync(currentUser);
            if (result.Succeeded)
            {
                return View("Index", currentUser);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Index");
        }

    }


}

