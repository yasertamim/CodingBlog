using CodingBlog.Data;
using CodingBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodingBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;

        }
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{

        //    return _db.Posts != null ?
        //                (IActionResult)View(await _db.Posts.ToListAsync()) :
        //                Problem("Entity set 'ApplicationDbContext.Posts'  is null.");





        //}
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _db.Posts != null ?
                       (IActionResult)View(await _db.Posts.ToListAsync()) :
                       Problem("Entity set 'ApplicationDbContext.Posts'  is null.");

            }
            var find =  _db.Posts.Where(b => b.Title.ToLower().Contains(search.ToLower()));
            if (find != null)
            {
                return View(await find.ToListAsync());
            }
            return _db.Posts != null ?
                      (IActionResult)View(await _db.Posts.ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.Posts'  is null.");


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}