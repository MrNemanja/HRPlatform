using HRPlatform.DTOs;

namespace HRPlatform.Services
{
    public interface ICandidateSkillService
    {
        Task<CandidateDTO> AddCandidateAsync(CreateCandidateDTO candidateDTO);
        Task DeleteCandidateAsync (int candidateId);
        Task AddSkillToCandidateAsync(int candidateId, int skillId);
        Task RemoveSkillFromCandidateAsync(int candidateId, int skillId);
        Task<List<CandidateDTO>> SearchCandidatesAsync(string? fullName, List<string>? skills);

    }
}
