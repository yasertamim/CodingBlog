using CodingBlog.Data;
using CodingBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;
using System.Xml.Linq;

namespace CodingBlog.Controllers

{
    
    public class PostController : Controller
    {
        // instantiate an instance of ApplicationDbContext class and UserManager
        // in order to inject them in the constructor of the controller
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        

        public PostController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _db = db;


        }


       
        [HttpGet]
        // get the content of selected post
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _db.Posts == null)
            {
                return NotFound();
            }
            // serach for the requested post in the database using the post id
            var post = await _db.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            // search for all comments related to the post
            List<Comment> comments = await _db.Comments.Where(b => b.PostId.Equals(id)).ToListAsync();

            // create an object of PostViewModel 
            // in order to pass it to the view
            // the of this model is to be able to use two
            // models in the same view
            var postViewModel = new PostViewModel
            {
                Post = post,
                Comments = comments
            };

            if (post == null)
            {
                return NotFound();
            }

            return View(postViewModel);
        }

        [Authorize]
        [HttpPost]
        // Post the comment
        public async Task<IActionResult> Details(string text, int postid)
        {
            // get the current loggrd in user
            var user = await _userManager.GetUserAsync(User);

            var author = user?.UserName;

            if (postid == null || _db.Posts == null)
            {
                return NotFound();
            }

            var comment = new Comment(text, author);
            
            comment.PostId = postid;
           
        
            _db.Add(comment);
            await _db.SaveChangesAsync();
         

            return RedirectToAction("Details");
        }



        [HttpGet]
        [Authorize]
        //get the view to delete the selected post
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Posts == null)
            {
                return NotFound();
            }

            var post = await _db.Posts
                .FirstOrDefaultAsync(m => m.Id == id);


            return View(post);
        }

        [HttpPost]
        [Authorize]
        // delete the selected post
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _db.Posts == null)
            {
                return NotFound();
            }

            var post = await _db.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
            return RedirectToAction("MyPosts");
        }


        // Get  Edit a post

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Posts == null)
            {
                return NotFound();
            }

            var post = await _db.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        [HttpPost]
        [Authorize]
        // update the selected post
        public async Task<IActionResult> Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);


                
                post.User = user;
                post.Author = user.UserName;


                _db.Update(post);
                    await _db.SaveChangesAsync();
              
                return RedirectToAction("MyPosts");
            }
            return View(post);
        }







        [HttpGet]
        [Authorize]
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        // create new post
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                // get user id
                var user = await _userManager.GetUserAsync(User);


                post.Published = DateTime.Now;
                post.User = user;
                post.Author = user.UserName;
            
              
                _db.Add(post);
                await _db.SaveChangesAsync();
                return RedirectToAction("MyPosts");
            }
            return View(post);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyPosts()
        {
            // to check if the user is logged in
            bool isAuthenticated = User.Identity.IsAuthenticated;
            // get user id
            var user = await _userManager.GetUserAsync(User);
            if (isAuthenticated)
            {
                // in the query you should add null checking to user id
                var query = await _db.Posts.Where(b => b.User.Id.
                          Equals(user.Id) && b.User.Id != null).ToListAsync();
                if (query.Count > 0)
                {
                    return View(query);
                }

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

           
        }



    }
}
