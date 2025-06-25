using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace X.Models
{
	public class Update
	{
		[Key]
		public int UpdateId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Title cant't be longer than 50 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Description can't be longer than 100 characters.")]
        public string Description { get; set; }

        public int UpdateBy { get; set; }
        public Admin UpdateByAdmin { get; set; }
    }
}

