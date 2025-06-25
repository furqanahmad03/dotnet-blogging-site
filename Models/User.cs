using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace X.Models
{
    public enum Status
    {
        ACTIVE,
        SUSPENDED
    }

    public class User : IdentityUser
    {
        [PersonalData]
        [Required]
        [StringLength(100, ErrorMessage = "Full name can't be longer than 100 characters.")]
        public string FullName { get; set; }

        [StringLength(500, ErrorMessage = "Picture string can't be longer than 500 characters.")]
        public string? Picture { get; set; }

        [Phone]
        [StringLength(15, ErrorMessage = "Phone number can't be longer than 15 characters.")]
        public string? Phone { get; set; }

        [StringLength(500, ErrorMessage = "About section can't be longer than 500 characters.")]
        public string? About { get; set; }

        public DateTime JoinedDate { get; set; }

        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        public string? SuspendedBy { get; set; }
        public Admin? SuspendedByUser { get; set; }

        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}