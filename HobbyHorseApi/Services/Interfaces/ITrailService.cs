using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Services.Interfaces
{
    public interface ITrailService
    {
        Task<IEnumerable<ParkTrail>> GetAllParkTrails();
    }
}
