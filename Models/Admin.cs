using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace X.Models
{
	public class Admin : IdentityUser 
	{
        [Required]
        [StringLength(100, ErrorMessage = "Full name can't be longer than 100 characters.")]
        public string FullName { get; set; }
    }
}

