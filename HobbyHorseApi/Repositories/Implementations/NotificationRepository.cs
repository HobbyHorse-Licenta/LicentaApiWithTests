using Google.Protobuf.WellKnownTypes;
using HobbyHorseApi.Entities;
using HobbyHorseApi.Entities.DBContext;
using HobbyHorseApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace HobbyHorseApi.Repositories.Implementations
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly HobbyHorseContext _context;

        public NotificationRepository(HobbyHorseContext context)
        {
            _context = context;
        }

        public async Task<SkateProfile> GetSkateProfilePlusNotifToken(string skateProfileId)
        {
            return await _context.SkateProfiles.Include(skateprofile => skateprofile.User)
                .Where(skateprofile => String.Equals(skateprofile.Id, skateProfileId)).FirstOrDefaultAsync();
        }
      
    }
}
