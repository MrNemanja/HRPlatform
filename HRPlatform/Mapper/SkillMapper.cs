using HRPlatform.DTOs;
using HRPlatform.Models;

namespace HRPlatform.Mapper
{
    public static class SkillMapper
    {
        public static SkillDTO ToDTO(Skill skill)
        {
            return new SkillDTO
            {
                Id = skill.Id,
                Name = skill.Name,
            };
        }

        public static Skill ToEntity(CreateSkillDTO createSkillDTO)
        {
            return new Skill
            {
                Name = createSkillDTO.Name
            };
        }
    }
}
