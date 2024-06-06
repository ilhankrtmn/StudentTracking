using StudentTracking.Data.EntityFramework.Entities;

namespace StudentTracking.Business.Interfaces
{
    public interface IAdminService
    {
        Task<List<User>> GetUserListAsync();
        Task<User> GetUserAsync(int userId);
        Task SaveorUpdateUser(User user);
        Task<bool> DeleteUser(int userId);
    }
}
