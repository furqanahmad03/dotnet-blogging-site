using System;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using X.Data;
using X.Models.Interfaces;

namespace X.Models.Repositories
{
	public class PostRepository : IPostRepository
	{
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.PostId == postId);

            if (post == null)
            {
                throw new KeyNotFoundException("Post not found.");
            }

            return post;
        }

        public async Task<List<Comment>> GetPostComments(int postId)
        {
            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.PostId == postId);

            if (post == null)
            {
                throw new KeyNotFoundException("Post not found.");
            }

            return post.Comments.ToList();
        }

        public async Task<List<Post>> GetAllPosts() {


            return await _context.Posts
                    .Include(p => p.Author)
                    .ToListAsync();
        }

        public async Task AddPost(Post post) {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post), "Post cannot be null.");
            }

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePost(Post post) {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post), "Post cannot be null.");
            }

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePost(int postId) {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                throw new KeyNotFoundException("Post not found.");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task PostCommentToPost(Comment comment) {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment), "Comment cannot be null.");
            }

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }
    }
}

