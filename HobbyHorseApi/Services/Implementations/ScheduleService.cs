using HobbyHorseApi.Entities;
using HobbyHorseApi.Repositories.Interfaces;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Services.Implementations
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepo;

        public ScheduleService(IScheduleRepository scheduleRepo)
        {
            _scheduleRepo = scheduleRepo;
        }

        public async Task DeleteSchedule(string scheduleId)
        {
            try
            {
                await _scheduleRepo.DeleteSchedule(scheduleId);
                return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedules()
        {
            try
            {
                return await _scheduleRepo.GetAllSchedules();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Schedule> GetSchedule(string scheduleId)
        {
            try
            {
                return await _scheduleRepo.GetSchedule(scheduleId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Schedule>> GetScheduleForSkateProfile(string skateProfileId)
        {
            try
            {
                return await _scheduleRepo.GetScheduleForSkateProfile(skateProfileId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Schedule> PostSchedule(Schedule schedule)
        {
            try
            {
                return await _scheduleRepo.PostSchedule(schedule);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Schedule> PutSchedule(Schedule schedule, string scheduleId)
        {
            try
            {
                return await _scheduleRepo.PutSchedule(schedule, scheduleId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
