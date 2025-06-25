using Microsoft.EntityFrameworkCore;
using X.Data;
using X.Models;
using X.Models.Interfaces;

namespace X.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Blog> GetBlogById(int blogId)
        {
            var blog = await _context.Blogs
            .Include(b => b.Author)
            .Include(b => b.Comments)
                .ThenInclude(c => c.Author)
            .FirstOrDefaultAsync(b => b.BlogId == blogId);

            if (blog == null)
            {
                throw new KeyNotFoundException("Blog not found.");
            }

            return blog;
        }

        public async Task<List<Blog>> GetBlogsByCategory(string category)
        {
            var blogs = await _context.Blogs
                .Include(b => b.Author)
                .Include(b => b.Comments)
                    .ThenInclude(c => c.Author)
                .Where(b => b.Category.ToLower() == category.ToLower())
                .ToListAsync();

            if (blogs == null)
            {
                throw new KeyNotFoundException("No blogs found for this category.");
            }

            return blogs.Count == 0 ? new List<Blog>() : blogs;
        }

        public async Task<List<Blog>> GetAllBlogs()
        {
            return await _context.Blogs
                .Include(b => b.Author)
                .Include(b => b.Comments)
                .OrderByDescending(b => b.BlogId)
                .ToListAsync();
        }

        public async Task<List<Blog>> GetMyBlogs(string userId)
        {
            return await _context.Blogs
                .Include(b => b.Author)
                .Include(c => c.Comments)
                    .ThenInclude(c => c.Author)
                .Where(b => b.AuthorId == userId)
                .ToListAsync();
        }

        public async Task AddBlog(Blog blog)
        {
            if (blog == null)
            {
                throw new ArgumentNullException(nameof(blog), "Blog cannot be null.");
            }

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBlog(Blog blog)
        {
            if (blog == null)
            {
                throw new ArgumentNullException(nameof(blog), "Blog cannot be null.");
            }

            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBlog(int blogId)
        {
            var blog = await _context.Blogs
                        .Include(b => b.Comments)
                        .FirstOrDefaultAsync(b => b.BlogId == blogId);

            if (blog == null)
            {
                throw new KeyNotFoundException("Blog not found.");
            }

            if (blog.Comments != null && blog.Comments.Any())
            {
                _context.Comments.RemoveRange(blog.Comments);
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }

        public async Task PostCommentToBlog(Comment comment)
        {
            Console.WriteLine("pahly");
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment), "Comment cannot be null.");
            }
            Console.WriteLine("baad");

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }
    }
}
