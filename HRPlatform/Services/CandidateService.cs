using HRPlatform.DTOs;
using HRPlatform.Mappers;
using HRPlatform.Models;
using HRPlatform.Repositories;

namespace HRPlatform.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ISkillRepository _skillRepository;

        public CandidateService(ICandidateRepository candidateRepository, ISkillRepository skillRepository)
        {
            _candidateRepository = candidateRepository;
            _skillRepository = skillRepository;
        }

        public async Task<CandidateDTO> AddCandidateAsync(CreateCandidateDTO candidateDTO)
        {
            Candidate existingCandidate = await _candidateRepository.GetCandidateByEmailAsync(candidateDTO.Email);
            if (existingCandidate != null) 
            {
                throw new InvalidOperationException("Candidate with this email already exists.");
            }

            existingCandidate = await _candidateRepository.GetCandidateByContactNumberAsync(candidateDTO.ContactNumber);
            if (existingCandidate != null)
            {
                throw new InvalidOperationException("Candidate with this contact number already exists.");
            }

            Candidate candidate = CandidateMapper.ToEntity(candidateDTO);

            Candidate createdCandidate = await _candidateRepository.AddCandidateAsync(candidate);

            return CandidateMapper.ToDTO(createdCandidate);
        }
        public async Task AddSkillToCandidateAsync(int candidateId, int skillId)
        {
            Candidate candidate = await _candidateRepository.GetCandidateAsync(candidateId);
            if (candidate == null)
            {
                throw new KeyNotFoundException("Candidate not found.");
            }

            Skill skill = await _skillRepository.GetSkillAsync(skillId);
            if (skill == null)
            {
                throw new KeyNotFoundException("Skill not found.");
            }

            if(candidate.Skills.Any(s => s.Id == skillId))
            {
                throw new InvalidOperationException("This skill is already assigned to this candidate.");
            }

            candidate.Skills.Add(skill);

            await _candidateRepository.SaveCandidatesDataAsync();

        }
        public async Task DeleteCandidateAsync(int candidateId)
        {
            Candidate candidate = await _candidateRepository.GetCandidateAsync(candidateId);
            if (candidate == null)
            {
                throw new KeyNotFoundException("Candidate not found.");
            }

            await _candidateRepository.DeleteCandidateAsync(candidate);
        }

        public async Task RemoveSkillFromCandidateAsync(int candidateId, int skillId)
        {
            Candidate candidate = await _candidateRepository.GetCandidateAsync(candidateId);
            if(candidate == null)
            {
                throw new KeyNotFoundException("Candidate not found.");
            }

            Skill skill = await _skillRepository.GetSkillAsync(skillId);
            if(skill == null)
            {
                throw new KeyNotFoundException("Skill not found.");
            }

            Skill existingSkill = candidate.Skills.FirstOrDefault(s => s.Id == skillId);
            if(existingSkill == null)
            {
                throw new InvalidOperationException("Candidate does not have this skill.");
            }

            candidate.Skills.Remove(existingSkill);

            await _candidateRepository.SaveCandidatesDataAsync();
        }

        public async Task<List<CandidateDTO>> SearchCandidatesAsync(string? name, List<string>? skills)
        {
            List<Candidate> filteredCandidates = await _candidateRepository.GetCandidatesAsync();

            if(!string.IsNullOrWhiteSpace(name))
            {
                filteredCandidates = filteredCandidates
                    .Where(c => c.FullName.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (skills != null && skills.Any())
            {
                filteredCandidates = filteredCandidates
                    .Where(c => 
                        c.Skills != null && 
                        skills.Any(s => 
                            c.Skills.Any(cs => 
                                cs.Name.Equals(s, StringComparison.OrdinalIgnoreCase))))
                    .ToList();
            }

            return CandidateMapper.ToDTOList(filteredCandidates);
        }
    }
}
