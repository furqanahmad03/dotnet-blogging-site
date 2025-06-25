using System;
using System.ComponentModel.DataAnnotations;

namespace X.Models
{
    public class Contact
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime AskedOn { get; set; } = DateTime.UtcNow;
    }
}
