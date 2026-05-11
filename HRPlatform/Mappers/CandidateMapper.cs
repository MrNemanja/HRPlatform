using HRPlatform.DTOs;
using HRPlatform.Models;

namespace HRPlatform.Mappers
{
    public static class CandidateMapper
    {
        public static CandidateDTO ToDTO(Candidate candidate) {

            return new CandidateDTO
            {
                Id = candidate.Id,
                FullName = candidate.FullName,
                DateOfBirth = candidate.DateOfBirth,
                ContactNumber = candidate.ContactNumber,
                Email = candidate.Email,
                Skills = candidate.Skills?.Select(s => s.Name).ToList() ?? new List<string>()
            };
        }

        public static Candidate ToEntity(CreateCandidateDTO candidateDTO)
        {
            return new Candidate
            {
                FullName = candidateDTO.FullName,
                DateOfBirth = candidateDTO.DateOfBirth,
                ContactNumber = candidateDTO.ContactNumber,
                Email = candidateDTO.Email,
            };
        }
        public static List<CandidateDTO> ToDTOList(List<Candidate> candidates)
        {
            return candidates.Select(ToDTO).ToList();
        }
    }
}
