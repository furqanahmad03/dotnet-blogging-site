using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace X.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Title should not be greater than 255 characters.")]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [Required]
        public string Image { get; set; }

        public DateTime PostedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public string Category { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}