using HRPlatform.Models;

namespace HRPlatform.Repositories
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetSkillsAsync();
        Task<Skill?> GetSkillAsync(int skillId);
        Task<Skill> AddSkillAsync(Skill skill);
        Task SaveSkillsDataAsync();
    }
}
