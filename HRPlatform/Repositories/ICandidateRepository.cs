using HRPlatform.Models;
using System.Runtime.CompilerServices;

namespace HRPlatform.Repositories
{
    public interface ICandidateRepository
    {
        Task<List<Candidate>> GetCandidatesAsync();
        Task<Candidate?> GetCandidateAsync(int candidateId);
        Task<Candidate> AddCandidateAsync(Candidate candidate);
        Task DeleteCandidate(Candidate candidate);
        Task SaveCandidatesDataAsync();

    }
}
