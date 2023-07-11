using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllSchedules();
        Task<Schedule> PostSchedule(Schedule schedule);

        Task DeleteSchedule(string scheduleId);

        Task<Schedule> PutSchedule(Schedule schedule, string scheduleId);
        Task<Schedule> GetSchedule(string scheduleId);
        Task<IEnumerable<Schedule>> GetScheduleForSkateProfile(string skateProfileId);



    }
}
