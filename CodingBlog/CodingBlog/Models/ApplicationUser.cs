using Microsoft.AspNetCore.Identity;

namespace CodingBlog.Models
{
    public class ApplicationUser : IdentityUser
    {

        public List<Post> Posts { get; set; }
    }
}
