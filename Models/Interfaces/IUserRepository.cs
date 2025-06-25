using System.Collections.Generic;
using System.Threading.Tasks;
using X.Models;

namespace X.Models.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string userId);
        Task<List<User>> GetAllUsers();
        Task AddUser(User user, string password);
        Task UpdateUser(User user);
        Task DeleteUser(string userId);
        Task SuspendUser(string userId, string adminId);
        Task UnsuspendUser(string userId);
    }
}