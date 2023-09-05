using HobbyHorseApi.Entities;
using HobbyHorseApi.Entities.DBContext;
using HobbyHorseApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HobbyHorseApi.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly HobbyHorseContext _context;

        public UserRepository(HobbyHorseContext context)
        {
            _context = context;
        }

        public async Task DeleteUser(string userId)
        {
            try
            {
                var userToDelete = await _context.Users.FindAsync(userId);

                if (userToDelete != null)
                {
                    _context.Users.Remove(userToDelete);
                    await _context.SaveChangesAsync();
                }
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
                return await _context.Users.ToListAsync();
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
                var user = await _context.Users
                .Include(user => user.SkateProfiles).ThenInclude(skateProfile => skateProfile.AssignedSkills).ThenInclude(assignedSkill => assignedSkill.Skill)
                .Include(user => user.SkateProfiles).ThenInclude(skateProfile => skateProfile.Schedules).ThenInclude(schedule => schedule.Days)
                .Include(user => user.SkateProfiles).ThenInclude(skateProfile => skateProfile.Schedules).ThenInclude(schedule => schedule.Zones)
                .Include(user => user.SkateProfiles).ThenInclude(skateProfile => skateProfile.Schedules).ThenInclude(schedule => schedule.Zones).ThenInclude(zone => zone.Location)
                .SingleOrDefaultAsync(user => user.Id == userId);
                if (user == null)
                {
                    throw new Exception($"User with id {userId} not found");
                }
                return user;
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
                var user = await _context.Users
                .SingleOrDefaultAsync(user => user.Id == userId);
                if (user == null)
                {
                    throw new Exception($"User with id {userId} not found");
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<User> PostUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
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
                Console.WriteLine("In UserRepo in PutUser");
                var oldUser = await _context.Users.FindAsync(userId);
                if(oldUser == null)
                {
                    throw new Exception($"User with id {userId} was not found");
                }
                _context.Entry(oldUser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
