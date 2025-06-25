using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using X.Hubs;
using X.Models;
using X.Models.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace X.Controllers
{
    [Authorize]
    public class FilterController : Controller
    {
        private IBlogRepository _blogRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<BlogHub> _hubContext;

        public FilterController(IBlogRepository blogRepository, UserManager<User> userManager, IHubContext<BlogHub> hubContext)
        {
            _hubContext = hubContext;
            _blogRepository = blogRepository;
            _userManager = userManager;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return RedirectToAction("Index", "Home");
            }
            var filteredBlogs = await _blogRepository.GetBlogsByCategory(category);
            ViewBag.category = category;
            return View(filteredBlogs);
        }
    }
}

