using System.ComponentModel.DataAnnotations;

namespace CodingBlog.Models
{
    public class Post
    {
        public Post() { }

        public Post(string title, string content, Technology.Tech language)
        {
            Title = title;
            Content = content;
            Language = language;
        
        }

        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(5000)]
        public string Content { get; set; } = string.Empty;
       

        public Technology.Tech Language { get; set; } = Technology.Tech.Java;

        public DateTime Published { get; set; } = DateTime.Now;

        public string Author { get; set; } = string.Empty;

        // navigation property

        public ApplicationUser? User { get; set; }
        public int UserId { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
