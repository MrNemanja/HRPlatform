using HRPlatform.Data;
using HRPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Repositories
{
    public class SkillRepository: ISkillRepository
    {
        private readonly AppDbContext _context;
        public SkillRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Skill?> GetSkillAsync(int skillId)
        {
            return await _context.Skills.FirstOrDefaultAsync(s => s.Id == skillId);
        }
        public async Task<Skill?> GetSkillByNameAsync(string skillName)
        {
            return await _context.Skills.FirstOrDefaultAsync(s => s.Name == skillName);
        }
        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return skill;
        }
    }
}
