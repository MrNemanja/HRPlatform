using HRPlatform.Models;
using System.Runtime.CompilerServices;

namespace HRPlatform.Repositories
{
    public interface ICandidateRepository
    {
        Task<List<Candidate>> GetCandidatesAsync();
        Task<Candidate?> GetCandidateAsync(int candidateId);
        Task<Candidate?> GetCandidateByEmailAsync(string email);
        Task<Candidate?> GetCandidateByContactNumberAsync(string contactNumber);
        Task<Candidate> AddCandidateAsync(Candidate candidate);
        Task DeleteCandidateAsync(Candidate candidate);
        Task SaveCandidatesDataAsync();

    }
}
