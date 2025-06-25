using System;
namespace X.Models.Interfaces
{
	public interface IBlogRepository
	{
		Task<Blog> GetBlogById(int blogId);
		Task<List<Blog>> GetBlogsByCategory(string category);
		Task<List<Blog>> GetAllBlogs();
		Task<List<Blog>> GetMyBlogs(string userId);
		Task AddBlog(Blog blog);
		Task UpdateBlog(Blog blog);
		Task DeleteBlog(int blogId);
		Task PostCommentToBlog(Comment comment);
	}
}

