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
        public async Task<Candidate?> GetCandidateByEmailAsync(string email)
        {
            return await _context.Candidates.Include(c => c.Skills).FirstOrDefaultAsync(c => c.Email == email);
        }
        public async Task<Candidate?> GetCandidateByContactNumberAsync(string contactNumber)
        {
            return await _context.Candidates.
                Include(c => c.Skills).
                FirstOrDefaultAsync(c => c.ContactNumber == contactNumber);
        }
        public async Task<Candidate> AddCandidateAsync(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            return candidate;
        }
        public async Task DeleteCandidateAsync(Candidate candidate)
        {
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }
        public async Task SaveCandidatesDataAsync()
        { 
            await _context.SaveChangesAsync();
        }

    }
}
