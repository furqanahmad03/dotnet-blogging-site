using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.Data;
using X.Models;
using X.Models.Interfaces;

namespace X.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<User> GetUserById(string userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users
                .Where(u => _context.UserClaims.Any(c => c.UserId == u.Id && c.ClaimType == "UserType" && c.ClaimValue == "User"))
                .ToListAsync();

            return users;
        }

        public async Task AddUser(User user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }

        public async Task UpdateUser(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUser(string userId)
        {
            var userBlogs = await _context.Blogs
                .Where(b => b.AuthorId == userId)
                .ToListAsync();

            var userBlogIds = userBlogs.Select(b => b.BlogId).ToList();

            var commentsOnUserBlogs = await _context.Comments
                .Where(c => c.BlogId.HasValue && userBlogIds.Contains(c.BlogId.Value))
                .ToListAsync();


            var userComments = await _context.Comments
                .Where(c => c.AuthorId == userId)
                .ToListAsync();


            _context.Comments.RemoveRange(commentsOnUserBlogs);
            _context.Comments.RemoveRange(userComments);
            _context.Blogs.RemoveRange(userBlogs);

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _context.SaveChangesAsync();
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task SuspendUser(string userId, string adminId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.Status = Status.SUSPENDED;
                user.SuspendedBy = adminId;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task UnsuspendUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.Status = Status.ACTIVE;
                user.SuspendedBy = null;
                await _userManager.UpdateAsync(user);
            }
        }

    }
}