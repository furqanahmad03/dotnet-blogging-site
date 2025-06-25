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
using X.Models.Repositories;
using X.Repositories;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace X.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private IBlogRepository _blogRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<BlogHub> _hubContext;

        public BlogsController(IBlogRepository blogRepository, UserManager<User> userManager, IHubContext<BlogHub> hubContext)
        {
            _hubContext = hubContext;
            _blogRepository = blogRepository;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogRepository.GetAllBlogs();
            return View(blogs);
        }

        public async Task<IActionResult> Blog(int id)
        {
            var blog = await _blogRepository.GetBlogById(id);
            return View(blog);
        }


        [HttpPost("AddBlog")]
        public async Task<IActionResult> AddBlog(Blog blog)
        {
            if (blog.ImageFile.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "Image size must be less than 2MB");
                var model = new DashboardViewModel
                {
                    blog = blog,
                    blogs = await _blogRepository.GetAllBlogs()
                };
                return RedirectToAction("Dashboard", "Profile");
            }
            if (blog.ImageFile == null || blog.ImageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Please upload an image");
                var model = new DashboardViewModel
                {
                    blog = blog,
                    blogs = await _blogRepository.GetAllBlogs()
                };
                return RedirectToAction("Dashboard", "Profile");
            }

            using (var memoryStream = new MemoryStream())
            {
                await blog.ImageFile.CopyToAsync(memoryStream);
                blog.Image = Convert.ToBase64String(memoryStream.ToArray());
            }

            blog.AuthorId = _userManager.GetUserId(User);

            await _blogRepository.AddBlog(blog);

            await _hubContext.Clients.All.SendAsync("ReceiveNewBlog", blog);

            return RedirectToAction("Dashboard", "Profile");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _blogRepository.DeleteBlog(id);
            return Json(new
            {
                success = true,
                message = "Blog deleted successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(int blogId, string comment, string userId)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                return Json(new { success = false, message = "⚠️ Comment is empty." });
            }

            var newComment = new Comment
            {
                BlogId = blogId,
                Content = comment,
                AuthorId = userId,
                CommentedOn = DateTime.UtcNow,
                EntityType = "Blog"
            };

            try
            {
                await _blogRepository.PostCommentToBlog(newComment);
                return Json(new { success = true, message = "✅ Comment submitted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "❌" + ex.Message });
            }
        }


    }
}

