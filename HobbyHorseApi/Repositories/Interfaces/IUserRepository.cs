using HobbyHorseApi.Entities;

namespace HobbyHorseApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> PostUser(User user);
        Task<User> GetUser(string userId);
        Task<IEnumerable<User>> GetAllUsers();
        
        Task<User> PutUser(string userId, User user);
        Task DeleteUser(string userId);
        Task<User> GetUserWithBasicInfo(string userId);


    }
}
