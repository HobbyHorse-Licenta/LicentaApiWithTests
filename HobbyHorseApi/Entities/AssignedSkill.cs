namespace HobbyHorseApi.Entities
{
    public class AssignedSkill
    {
        public string Id { get; set; }

        public string SkateProfileId { get; set; }
        public SkateProfile? SkateProfile { get; set; } = null;
        public Skill? Skill { get; set; } = null;
        public string SkillId { get; set; }
        public string MasteringLevel { get; set; }

    }
}
