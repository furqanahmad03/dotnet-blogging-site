using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using X.Models;
using Microsoft.AspNetCore.Identity;
using X.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace X.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IBlogRepository _blogRepository;

    public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, IBlogRepository blogRepository)
    {
        _logger = logger;
        _userManager = userManager;
        _blogRepository = blogRepository;
    }

    public async Task<IActionResult> Index()
    {
        var blogs = await _blogRepository.GetAllBlogs();
        return View(blogs);
    }

    //public IActionResult Privacy()
    //{
    //    return View();
    //}

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

