using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using X.Models.Interfaces;
using X.Models.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using X.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace X.Controllers
{
    public class AdminController : Controller
    {
        private IUserRepository _userRepository;
        private IBlogRepository _blogRepository;
        private IContactRepository _contactRepository;
        private readonly UserManager<User> _userManager;

        public AdminController(IUserRepository userRepository, IBlogRepository blogRepository, IContactRepository contactRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _blogRepository = blogRepository;
            _contactRepository = contactRepository;
            _userManager = userManager;
        }
        // GET: /<controller>/
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserById(userId);
            return View(user);
        }

        public async Task<IActionResult> LoadSection(string section)
        {
            switch (section)
            {
                case "blogs":
                    var blogs = await _blogRepository.GetAllBlogs();
                    return PartialView("_BlogsTable", blogs);
                case "posts":
                    return PartialView("_PostsTable");
                case "updates":
                    return PartialView("_UpdatesTable");
                case "queries":
                    var queries = await _contactRepository.GetAllQueries();
                    return PartialView("_QueriesTable", queries);
                default:
                    var users = await _userRepository.GetAllUsers();
                    return PartialView("_UserTable", users);
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuspendUser(string userId)
        {
            var adminId = _userManager.GetUserId(User);
            await _userRepository.SuspendUser(userId, adminId);
            return Json(new
            {
                success = true,
                newStatus = "SUSPENDED",
                statusClass = "bg-red-600"
            });
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnsuspendUser(string userId)
        {
            await _userRepository.UnsuspendUser(userId);
            return Json(new
            {
                success = true,
                newStatus = "ACTIVE",
                statusClass = "bg-green-600"
            });
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _userRepository.DeleteUser(userId);
            return Json(new
            {
                success = true,
                message = "User deleted successfully"
            });
        }
    }
}

