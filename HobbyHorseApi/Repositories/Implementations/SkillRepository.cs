using HobbyHorseApi.Entities;
using HobbyHorseApi.Entities.DBContext;
using HobbyHorseApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HobbyHorseApi.Repositories.Implementations
{
    public class SkillRepository : ISkillRepository
    {
        private readonly HobbyHorseContext _context;

        public SkillRepository(HobbyHorseContext context)
        {
            _context = context;
        }

        public async Task DeleteAssignedSkill(string assignedSkillId, string skateProfileId)
        {
            try
            {
                var skateProfile = await _context.SkateProfiles.Include(skateProfile => skateProfile.AssignedSkills).FirstOrDefaultAsync(skateProfile => skateProfile.Id == skateProfileId);
                if (skateProfile == null)
                {
                    throw new Exception("No skate profile with this id found");
                }

                var assignedSkillToRemove = skateProfile.AssignedSkills.FirstOrDefault(skill => skill.Id == assignedSkillId);
                if(assignedSkillToRemove == null)
                {
                    throw new Exception("No such skill associated with the indicated skate profile");
                }

                skateProfile.AssignedSkills.Remove(assignedSkillToRemove);
                await _context.SaveChangesAsync();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Skill>> GetAllSkills()
        {
            try
            {
               return await _context.Skills.ToListAsync();
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
                var recommandations = await _context.SkillRecommendations.Where((skill) => skill.SkatePracticeStyle == practiceStyle && skill.SkateExperience == experience).Include(skillRecomm => skillRecomm.Skill).ToListAsync();
                return recommandations;
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
                if (assignedSkill.SkateProfileId != null)
                {
                    var skateProfile = await _context.SkateProfiles.FindAsync(assignedSkill.SkateProfileId);
                    if(skateProfile == null)
                        throw new Exception("AssignedSkill doesn't belong to any SkateProfile");
                }
                else throw new Exception("AssignedSkill doesn't belong to any SkateProfile");

                await _context.AssignedSkills.AddAsync(assignedSkill);
                await _context.SaveChangesAsync();
                var postedAssignedSkill =  await _context.AssignedSkills.Include(assignedSkill => assignedSkill.Skill).SingleOrDefaultAsync(skill => skill.Id == assignedSkill.Id);

                return postedAssignedSkill;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AssignedSkill> PutAssignedSkill(AssignedSkill assignedSkill)
        {
            try
            {
                if (assignedSkill.SkateProfileId != null)
                {
                    var skateProfile = await _context.SkateProfiles.FindAsync(assignedSkill.SkateProfileId);
                    if (skateProfile == null)
                        throw new Exception("AssignedSkill doesn't belong to any SkateProfile");

                    var skill = await _context.AssignedSkills.FindAsync(assignedSkill.Id);
                    if (skill == null)
                    {
                        throw new Exception($"Skill with id {assignedSkill.Id} was not found");
                    }
                    _context.Entry(skill).CurrentValues.SetValues(assignedSkill);
                    await _context.SaveChangesAsync();
                    return assignedSkill;

                }
                else throw new Exception("AssignedSkill doesn't belong to any SkateProfile");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
