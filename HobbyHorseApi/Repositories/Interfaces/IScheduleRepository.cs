using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetAllSchedules();
        Task<Schedule> PostSchedule(Schedule schedule);
        Task DeleteSchedule(string scheduleId);

        Task<Schedule> PutSchedule(Schedule schedule, string scheduleId);
        Task<Schedule> GetSchedule(string scheduleId);
        Task<IEnumerable<Schedule>> GetScheduleForSkateProfile(string skateProfileId);


    }
}
