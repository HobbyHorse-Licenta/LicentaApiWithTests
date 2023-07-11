using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task<Location> GetLocationByName(string name);
    }
}
