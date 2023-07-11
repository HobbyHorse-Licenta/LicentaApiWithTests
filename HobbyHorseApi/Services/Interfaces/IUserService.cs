using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Services.Interfaces
{

    public interface IUserService
    {
        Task<User> PostUser(User user);
        Task<User> GetUser(string userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> PutUser(string userId, User user);

        Task DeleteUser(string userId);
        Task<User> GetUserWithBasicInfo(string userId);

    }
}
