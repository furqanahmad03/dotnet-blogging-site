using System;
using System.ComponentModel.DataAnnotations;

namespace X.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Content should not be greater than 200 characters.")]
        public string Content { get; set; }

        [Required]
        public DateTime CommentedOn { get; set; }

        [Required]
        public string AuthorId { get; set; }
        public User Author { get; set; }

        public int? BlogId { get; set; }
        public Blog Blog { get; set; }

        public int? PostId { get; set; }
        public Post Post { get; set; }

        public string EntityType { get; set; }
    }
}
