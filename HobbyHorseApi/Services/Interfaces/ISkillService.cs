using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Services.Interfaces
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillRecommendation>> GetSkillRecomandations(string practiceStyle, string experience);
        Task<AssignedSkill> PostAssignedSkill(AssignedSkill assignedSkill);
        Task<AssignedSkill> PutAssignedSkill(AssignedSkill assignedSkill);
        Task<IEnumerable<Skill>> GetAllSkills();
        Task DeleteAssignedSkill(string assignedSkillId, string skateProfileId);
    }
}
