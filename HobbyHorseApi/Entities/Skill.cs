namespace HobbyHorseApi.Entities
{
    public class Skill
    {
       
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<SkillRecommendation>? SkillRecommendations { get; set; } = new List<SkillRecommendation>();   
        public ICollection<AssignedSkill>? AssignedSkills { get; set; } = new List<AssignedSkill>();

    }
}
