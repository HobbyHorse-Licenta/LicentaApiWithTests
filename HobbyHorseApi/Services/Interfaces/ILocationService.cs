using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Services.Interfaces
{
    public interface ILocationService
    {
        Task<Location> GetLocationByName(string name);
    }
}
