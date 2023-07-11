using HobbyHorseApi.Entities;

namespace HobbyHorseApi.Repositories.Interfaces
{
    public interface ISkillRepository
    {
        Task<IEnumerable<SkillRecommendation>> GetSkillRecomandations(string practiceStyle, string experience);
        Task<AssignedSkill> PostAssignedSkill(AssignedSkill assignedSkill);
        Task<AssignedSkill> PutAssignedSkill(AssignedSkill assignedSkill);
        Task<IEnumerable<Skill>> GetAllSkills();
        Task DeleteAssignedSkill(string assignedSkillId, string skateProfileId);

    }
}
