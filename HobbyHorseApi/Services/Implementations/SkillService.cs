using HobbyHorseApi.Entities;
using HobbyHorseApi.Repositories.Interfaces;
using HobbyHorseApi.Services.Interfaces;

namespace HobbyHorseApi.Services.Implementations
{
    public class SkillService : ISkillService
    {
        public readonly ISkillRepository _repo;
        public SkillService(ISkillRepository repo)
        {
            _repo = repo;
        }

        public async Task DeleteAssignedSkill(string assignedSkillId, string skateProfileId)
        {
            try
            {
                await _repo.DeleteAssignedSkill(assignedSkillId, skateProfileId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async  Task<IEnumerable<Skill>> GetAllSkills()
        {
            try
            {
                return await _repo.GetAllSkills();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<SkillRecommendation>> GetSkillRecomandations(string practiceStyle, string experience)
        {
            try
            {
                return await _repo.GetSkillRecomandations(practiceStyle, experience);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<AssignedSkill> PostAssignedSkill(AssignedSkill assignedSkill)
        {
            try
            {
                return await _repo.PostAssignedSkill(assignedSkill);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<AssignedSkill> PutAssignedSkill(AssignedSkill assignedSkill)
        {
            try
            {
                return await _repo.PutAssignedSkill(assignedSkill);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }


}
