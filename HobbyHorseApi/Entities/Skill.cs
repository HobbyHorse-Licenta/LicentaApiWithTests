namespace HobbyHorseApi.Entities
{
    public class Skill
    {
        //public Skill()
        //{
        //    AssignedSkills = new List<AssignedSkill>();
        //    SkillRecommendations = new List<SkillRecommendation>();
        //}
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<SkillRecommendation>? SkillRecommendations { get; set; } = new List<SkillRecommendation>();   
        public ICollection<AssignedSkill>? AssignedSkills { get; set; } = new List<AssignedSkill>();

    }
}
