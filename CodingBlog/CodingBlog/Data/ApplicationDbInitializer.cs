using CodingBlog.Models;
using Microsoft.AspNetCore.Identity;

namespace CodingBlog.Data
{
    // this class is a service used to create database 
    // and add some data in order to test the application
    public class ApplicationDbInitializer
    {
        public static void Initializer(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            // create admin role
            var admin = new IdentityRole("admin");
            rm.CreateAsync(admin).Wait();
            // create new user
            var adminUser = new ApplicationUser { Email = "yaser@gmail.com", UserName = "yaser@gmail.com", EmailConfirmed = true };
            um.CreateAsync(adminUser, "Password.1").Wait();

            var user = new ApplicationUser { Email = "yaser@uia.no", UserName = "yaser@uia.no", EmailConfirmed = true };
            um.CreateAsync(user, "Password.1").Wait();

            // add role to the user
            um.AddToRoleAsync(adminUser, "admin").Wait();

            var comments = new[]
            {
                new Comment("this is awsome work", "yaser"),
                new Comment("this is awsome work", "aouss"),
                new Comment("this is awsome work","miral"),
                new Comment("this is awsome work","hani"),
                new Comment("this is awsome work","amer"),
                new Comment("this is awsome work","naser"),
            };


            var posts = new[]
            {
                new Post("Bubble sort", "here tou can learn. how to implement bubble sort algorithm in python Bubble sort\", \"here tou can learn how to implement bubble sort algorithm in python",  Technology.Tech.Python),
                new Post("quick sort", "here tou can learn how to implement quick sort algorithm in python", Technology.Tech.Java),
                new Post("binary search", "here tou can learn how to implement binary search algorithm in python", Technology.Tech.C),
                 new Post("Bubble sort", "here tou can learn how to implement bubble sort algorithm in python Bubble sort\", \"here tou can learn how to implement bubble sort algorithm in python",  Technology.Tech.Python),
                new Post("quick sort", "here tou can learn how to implement quick sort algorithm in python", Technology.Tech.Java),
                new Post("binary search", "here tou can learn how to implement binary search algorithm in python", Technology.Tech.C),


            };


            var posts2 = new[]
         {
                new Post("Bubble sort", "here tou can learn. how to implement bubble sort algorithm in python Bubble sort\", \"here tou can learn how to implement bubble sort algorithm in python",  Technology.Tech.Python),
                new Post("quick sort", "here tou can learn how to implement quick sort algorithm in python", Technology.Tech.Java),
                new Post("binary search", "here tou can learn how to implement binary search algorithm in python", Technology.Tech.C),
                 new Post("Bubble sort", "here tou can learn how to implement bubble sort algorithm in python Bubble sort\", \"here tou can learn how to implement bubble sort algorithm in python",  Technology.Tech.Python),
                new Post("quick sort", "here tou can learn how to implement quick sort algorithm in python", Technology.Tech.Java),
                new Post("binary search", "here tou can learn how to implement binary search algorithm in python", Technology.Tech.C),


            };

            foreach(var post in posts)
            {
                post.User = adminUser;
                post.Author = adminUser.UserName;
                post.Comments.AddRange(comments);
                db.SaveChanges();
            }

            foreach (var post in posts2)
            {
                post.User = user;
                post.Author = user.UserName;
                post.Comments.AddRange(comments);
                db.SaveChanges();

            }


            db.AddRange(posts);
            db.SaveChanges();
            db.AddRange(posts2);
            db.SaveChanges();
        }
    }
}
