using HobbyHorseApi.Entities;
using HobbyHorseApi.Repositories.Interfaces;
using HobbyHorseApi.Services.Interfaces;

namespace HobbyHorseApi.Services.Implementations
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task DeleteUser(string userId)
        {
            try
            {
                await _repo.DeleteUser(userId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _repo.GetAllUsers();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<User> GetUser(string userId)
        {
            try
            {
                return await _repo.GetUser(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<User> GetUserWithBasicInfo(string userId)
        {
            try
            {
                return await _repo.GetUserWithBasicInfo(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async  Task<User> PostUser(User user)
        {
            try
            {
                return await _repo.PostUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<User> PutUser(string userId, User user)
        {
            try
            {
                return await _repo.PutUser(userId, user);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }


}
