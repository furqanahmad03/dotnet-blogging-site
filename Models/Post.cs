using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace X.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Title should not be greater than 255 characters.")]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Category should not be greater than 100 characters.")]
        public string Category { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Type should not be greater than 50 characters.")]
        public string Type { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public User Author { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
