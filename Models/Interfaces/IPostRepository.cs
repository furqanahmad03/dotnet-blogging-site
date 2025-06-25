using System.Collections.Generic;
using System.Threading.Tasks;
using X.Models;

namespace X.Models.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetPostById(int postId);
        Task<List<Comment>> GetPostComments(int postId);
        Task<List<Post>> GetAllPosts();
        Task AddPost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(int postId);
        Task PostCommentToPost(Comment comment);
    }
}