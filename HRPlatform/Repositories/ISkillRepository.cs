using HRPlatform.Models;

namespace HRPlatform.Repositories
{
    public interface ISkillRepository
    {
        Task<Skill?> GetSkillAsync(int skillId);
        Task<Skill?> GetSkillByNameAsync(string skillName);
        Task<Skill> AddSkillAsync(Skill skill);
    }
}
