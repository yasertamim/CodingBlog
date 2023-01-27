using System.ComponentModel.DataAnnotations.Schema;

namespace CodingBlog.Models
{
    public class Comment
    {
        public Comment() { }


        public Comment(string text, string author)
        {
            Text = text;
            Author = author;
        }
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CommentDate { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Author { get; set; } = string.Empty;

        
        public int PostId { get; set; }

        public Post? Post { get; set; }
    }
    

}

