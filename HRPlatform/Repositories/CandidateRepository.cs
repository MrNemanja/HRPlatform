using HRPlatform.Data;
using HRPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _context;

        public CandidateRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Candidate>> GetCandidatesAsync()
        {
            return await _context.Candidates.Include(c => c.Skills).ToListAsync();
        }
        public async Task<Candidate?> GetCandidateAsync(int candidateId)
        {
            return await _context.Candidates.Include(c => c.Skills).FirstOrDefaultAsync(c => c.Id == candidateId);
        }
        public async Task<Candidate> AddCandidateAsync(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            return candidate;
        }
        public async Task DeleteCandidate(Candidate candidate)
        {
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Skill>> GetSkillsAsync()
        {
            return await _context.Skills.ToListAsync();
        }
        public async Task<Skill?> GetSkillAsync(int skillId)
        {
            return await _context.Skills.FirstOrDefaultAsync(s => s.Id == skillId);
        }
        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            
            return skill;
        }
        public async Task SaveCandidatesDataAsync()
        { 
            await _context.SaveChangesAsync();
        }
    }
}
