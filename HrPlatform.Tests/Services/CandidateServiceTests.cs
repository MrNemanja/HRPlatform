using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using HRPlatform.Repositories;
using HRPlatform.Services;
using HRPlatform.DTOs;
using HRPlatform.Models;

namespace HRPlatform.Tests.Services
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _candidateRepoMock;
        private readonly Mock<ISkillRepository> _skillRepoMock;
        private readonly CandidateService _candidateService;

        public CandidateServiceTests()
        {
            _candidateRepoMock = new Mock<ICandidateRepository>();
            _skillRepoMock = new Mock<ISkillRepository>();

            _candidateService = new CandidateService(_candidateRepoMock.Object, _skillRepoMock.Object);
        }

        private List<Candidate> GetCandidates()
        {
            return new List<Candidate>
        {
            new Candidate
            {
                FullName = "Nemanja Nikolic",
                Skills = new List<Skill>
                {
                    new Skill { Name = "C#" },
                    new Skill { Name = "SQL" }
                }
            },
            new Candidate
            {
                FullName = "Marko Markovic",
                Skills = new List<Skill>
                {
                    new Skill { Name = "Java" }
                }
            }
        };
        }

        [Fact]
        public async Task AddCandidate_ShouldReturnCandidate_WhenValidData()
        {
            // Arrange
            CreateCandidateDTO createCandidateDTO = new CreateCandidateDTO
            {
                FullName = "Test User",
                Email = "test@test.com",
                ContactNumber = "123",
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            Candidate candidate = new Candidate
            {
                FullName = createCandidateDTO.FullName,
                Email = createCandidateDTO.Email,
                ContactNumber = createCandidateDTO.ContactNumber,
                DateOfBirth = createCandidateDTO.DateOfBirth
            };

            _candidateRepoMock
                .Setup(x => x.GetCandidateByEmailAsync(createCandidateDTO.Email))
                .ReturnsAsync((Candidate)null);

            _candidateRepoMock
                .Setup(x => x.GetCandidateByContactNumberAsync(createCandidateDTO.ContactNumber))
                .ReturnsAsync((Candidate)null);

            _candidateRepoMock
                .Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(candidate);

            // Act
            CandidateDTO result = await _candidateService.AddCandidateAsync(createCandidateDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(createCandidateDTO.FullName, result.FullName);
            Assert.Equal(createCandidateDTO.Email, result.Email);
        }

        [Fact]
        public async Task AddCandidate_ShouldThrowException_WhenEmailAlreadyExists()
        {
            //Arrange
            CreateCandidateDTO createCandidateDTO = new CreateCandidateDTO
            {
                FullName = "Test User",
                Email = "test@test.com",
                ContactNumber = "123",
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            Candidate existingCandidate = new Candidate
            {
                Email = createCandidateDTO.Email
            };

            _candidateRepoMock
                .Setup(x => x.GetCandidateByEmailAsync(createCandidateDTO.Email))
                .ReturnsAsync(existingCandidate);

            //Act + Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _candidateService.AddCandidateAsync(createCandidateDTO)
            );

            Assert.Equal("Candidate with this email already exists.", exception.Message);

        }

        [Fact]
        public async Task AddCandidate_ShouldThrowException_WhenPhoneNumberAlreadyExists()
        {
            // Arrange
            CreateCandidateDTO createCandidateDTO = new CreateCandidateDTO
            {
                FullName = "Test User",
                Email = "test@test.com",
                ContactNumber = "123",
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            Candidate existingCandidate = new Candidate
            {
                ContactNumber = createCandidateDTO.ContactNumber
            };

            _candidateRepoMock
                .Setup(x => x.GetCandidateByEmailAsync(createCandidateDTO.Email))
                .ReturnsAsync((Candidate)null);

            _candidateRepoMock
                .Setup(x => x.GetCandidateByContactNumberAsync(createCandidateDTO.ContactNumber))
                .ReturnsAsync(existingCandidate);

            // Act + Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _candidateService.AddCandidateAsync(createCandidateDTO));

            Assert.Equal("Candidate with this contact number already exists.", exception.Message);
        }

        [Fact]
        public async Task DeleteCandidate_ShouldDelete_WhenCandidateExists()
        {
            // Arrange
            Candidate candidate = new Candidate { Id = 1 };

            _candidateRepoMock
                .Setup(x => x.GetCandidateAsync(1))
                .ReturnsAsync(candidate);

            _candidateRepoMock
                .Setup(x => x.DeleteCandidateAsync(candidate))
                .Returns(Task.CompletedTask);

            // Act
            await _candidateService.DeleteCandidateAsync(1);

            // Assert
            _candidateRepoMock.Verify(x => x.DeleteCandidateAsync(candidate), Times.Once);
        }

        [Fact]
        public async Task DeleteCandidate_ShouldThrowException_WhenCandidateNotFound()
        {
            //Arrange
            _candidateRepoMock
                .Setup(x => x.GetCandidateAsync(It.IsAny<int>()))
                .ReturnsAsync((Candidate)null);

            //Act + Assert
            KeyNotFoundException exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _candidateService.DeleteCandidateAsync(1));

            Assert.Equal("Candidate not found.", exception.Message);
        }

        [Fact]
        public async Task AddSkillToCandidate_ShouldAddSkill_WhenValid()
        {
            //Arrange
            var candidate = new Candidate
            {
                Id = 1,
                Skills = new List<Skill>()
            };

            var skill = new Skill { Id = 1, Name = "C#" };

            _candidateRepoMock.Setup(x => x.GetCandidateAsync(1)).ReturnsAsync(candidate);
            _skillRepoMock.Setup(x => x.GetSkillAsync(1)).ReturnsAsync(skill);
            _candidateRepoMock.Setup(x => x.SaveCandidatesDataAsync()).Returns(Task.CompletedTask);

            //Act
            await _candidateService.AddSkillToCandidateAsync(1, 1);

            //Assert
            Assert.Single(candidate.Skills);
        }

        [Fact]
        public async Task AddSkillToCandidate_ShouldThrowException_WhenSkillAlreadyExists()
        {
            //Arrange
            var skill = new Skill { Id = 1 };

            var candidate = new Candidate
            {
                Id = 1,
                Skills = new List<Skill> { skill }
            };

            _candidateRepoMock.Setup(x => x.GetCandidateAsync(1)).ReturnsAsync(candidate);
            _skillRepoMock.Setup(x => x.GetSkillAsync(1)).ReturnsAsync(skill);

            //Act + Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _candidateService.AddSkillToCandidateAsync(1, 1));

            Assert.Equal("This skill is already assigned to this candidate.", exception.Message);
        }

        [Fact]
        public async Task AddSkillToCandidate_ShouldThrowException_WhenCandidateNotFound()
        {
            //Arrange
            _candidateRepoMock
                .Setup(x => x.GetCandidateAsync(It.IsAny<int>()))
                .ReturnsAsync((Candidate)null);

            //Act + Assert
            KeyNotFoundException exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _candidateService.AddSkillToCandidateAsync(1, 1));

            Assert.Equal("Candidate not found.", exception.Message);
        }

        [Fact]
        public async Task AddSkillToCandidate_ShouldThrowException_WhenSkillNotFound()
        {
            //Arrange
            Candidate candidate = new Candidate
            { Id = 1, Skills = new List<Skill>() };

            _candidateRepoMock
                .Setup(x => x.GetCandidateAsync(1))
                .ReturnsAsync(candidate);

            _skillRepoMock
                .Setup(x => x.GetSkillAsync(1))
                .ReturnsAsync((Skill)null);

            //Act + Assert
            KeyNotFoundException exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _candidateService.AddSkillToCandidateAsync(1, 1));

            Assert.Equal("Skill not found.", exception.Message);
        }

        [Fact]
        public async Task RemoveSkillFromCandidate_ShouldRemoveSkill_WhenExists()
        {
            //Arrange
            var skill = new Skill { Id = 1 };

            var candidate = new Candidate
            {
                Id = 1,
                Skills = new List<Skill> { skill }
            };

            _candidateRepoMock.Setup(x => x.GetCandidateAsync(1)).ReturnsAsync(candidate);
            _skillRepoMock.Setup(x => x.GetSkillAsync(1)).ReturnsAsync(skill);
            _candidateRepoMock.Setup(x => x.SaveCandidatesDataAsync()).Returns(Task.CompletedTask);

            //Act
            await _candidateService.RemoveSkillFromCandidateAsync(1, 1);

            //Assert
            Assert.Empty(candidate.Skills);
        }

        [Fact]
        public async Task RemoveSkillFromCandidate_ShouldThrowException_WhenCandidateNotFound()
        {
            //Arrange
            _candidateRepoMock
                .Setup(x => x.GetCandidateAsync(It.IsAny<int>()))
                .ReturnsAsync((Candidate)null);

            //Act + Assert
            KeyNotFoundException exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _candidateService.RemoveSkillFromCandidateAsync(1, 1));

            Assert.Equal("Candidate not found.", exception.Message);
        }

        [Fact]
        public async Task RemoveSkillFromCandidate_ShouldThrowException_WhenSkillNotFound()
        {
            //Arrange
            var candidate = new Candidate { Id = 1, Skills = new List<Skill>() };

            _candidateRepoMock.Setup(x => x.GetCandidateAsync(1)).ReturnsAsync(candidate);
            _skillRepoMock.Setup(x => x.GetSkillAsync(1)).ReturnsAsync((Skill)null);

            //Act + Assert
            KeyNotFoundException exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _candidateService.RemoveSkillFromCandidateAsync(1, 1));

            Assert.Equal("Skill not found.", exception.Message);
        }

        [Fact]
        public async Task RemoveSkillFromCandidate_ShouldThrowException_WhenSkillNotAssigned()
        {
            //Arrange
            var candidate = new Candidate
            {
                Id = 1,
                Skills = new List<Skill>()
            };

            var skill = new Skill { Id = 1 };

            _candidateRepoMock.Setup(x => x.GetCandidateAsync(1)).ReturnsAsync(candidate);
            _skillRepoMock.Setup(x => x.GetSkillAsync(1)).ReturnsAsync(skill);

            //Act + Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _candidateService.RemoveSkillFromCandidateAsync(1, 1));

            Assert.Equal("Candidate does not have this skill.", exception.Message);
        }

        [Fact]
        public async Task Search_ShouldReturnAll_WhenNoFilters()
        {
            // Arrange
            _candidateRepoMock
                .Setup(x => x.GetCandidatesAsync())
                .ReturnsAsync(GetCandidates());

            // Act
            List<CandidateDTO> result = await _candidateService.SearchCandidatesAsync(null, null);

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Search_ShouldReturnCandidates_WhenNameMatches()
        {
            //Arrange
            _candidateRepoMock
                .Setup(x => x.GetCandidatesAsync())
                .ReturnsAsync(GetCandidates());
            
            //Act
            List<CandidateDTO> result = await _candidateService.SearchCandidatesAsync("Nemanja", null);

            //Assert
            Assert.Single(result);
            Assert.Equal("Nemanja Nikolic", result.First().FullName);
        }

        [Fact]
        public async Task Search_ShouldReturnCandidates_WhenSkillMatches()
        {
            //Arrange
            _candidateRepoMock
                .Setup(x => x.GetCandidatesAsync())
                .ReturnsAsync(GetCandidates());

            //Act
            List<CandidateDTO> result = await _candidateService.SearchCandidatesAsync(null, new List<string> { "C#" });

            //Assert
            Assert.Single(result);
            Assert.Equal("Nemanja Nikolic", result.First().FullName);
        }

        [Fact]
        public async Task Search_ShouldReturnCandidates_WhenNameAndSkillMatch()
        {
            //Arrange
            _candidateRepoMock
                .Setup(x => x.GetCandidatesAsync())
                .ReturnsAsync(GetCandidates());

            //Act
            List<CandidateDTO> result = await _candidateService.SearchCandidatesAsync("Nemanja", new List<string> { "C#" });

            //Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task Search_ShouldReturnEmpty_WhenNoMatch()
        {
            //Arrange
            _candidateRepoMock
                .Setup(x => x.GetCandidatesAsync())
                .ReturnsAsync(GetCandidates());

            //Act
            List<CandidateDTO> result = await _candidateService.SearchCandidatesAsync("Petar Petrovic", new List<string> { "Python" });

            //Assert
            Assert.Empty(result);
        }


    }
}
