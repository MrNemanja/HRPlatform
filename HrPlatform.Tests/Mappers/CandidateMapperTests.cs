using HRPlatform.Mappers;
using HRPlatform.Models;
using HRPlatform.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Tests.Mappers
{
    public class CandidateMapperTests
    {
        [Fact]
        public void ToDTO_ShouldMapAllFields_Correctly()
        {
            // Arrange
            Candidate candidate = new Candidate
            {
                Id = 1,
                FullName = "Nemanja Nikolic",
                DateOfBirth = new DateTime(2000, 1, 1),
                ContactNumber = "123",
                Email = "test@test.com",
                Skills = new List<Skill>
                {
                    new Skill { Name = "C#" },
                    new Skill { Name = "SQL" }
                }
            };

            // Act
            CandidateDTO result = CandidateMapper.ToDTO(candidate);

            // Assert
            Assert.Equal(candidate.Id, result.Id);
            Assert.Equal(candidate.FullName, result.FullName);
            Assert.Equal(candidate.Email, result.Email);
            Assert.Equal(candidate.ContactNumber, result.ContactNumber);
            Assert.Equal(2, result.Skills.Count);
            Assert.Contains("C#", result.Skills);
            Assert.Contains("SQL", result.Skills);
        }

        [Fact]
        public void ToDTO_ShouldHandleNullSkills()
        {
            // Arrange
            Candidate candidate = new Candidate
            {
                Id = 1,
                FullName = "Test",
                Skills = null
            };

            // Act
            CandidateDTO result = CandidateMapper.ToDTO(candidate);

            // Assert
            Assert.NotNull(result.Skills);
            Assert.Empty(result.Skills);
        }

        [Fact]
        public void ToEntity_ShouldMapCreateDTO_Correctly()
        {
            // Arrange
            CreateCandidateDTO createCandidateDTO = new CreateCandidateDTO
            {
                FullName = "Test User",
                Email = "test@test.com",
                ContactNumber = "123",
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            // Act
            Candidate result = CandidateMapper.ToEntity(createCandidateDTO);

            // Assert
            Assert.Equal(createCandidateDTO.FullName, result.FullName);
            Assert.Equal(createCandidateDTO.Email, result.Email);
            Assert.Equal(createCandidateDTO.ContactNumber, result.ContactNumber);
            Assert.Equal(createCandidateDTO.DateOfBirth, result.DateOfBirth);
        }

        [Fact]
        public void ToDTOList_ShouldMapAllCandidates()
        {
            // Arrange
            List<Candidate> candidates = new List<Candidate>
            { 
                new Candidate { Id = 1, FullName = "A", Skills = new List<Skill>() },
                new Candidate { Id = 2, FullName = "B", Skills = new List<Skill>() }
            };

            // Act
            List<CandidateDTO> result = CandidateMapper.ToDTOList(candidates);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("A", result[0].FullName);
            Assert.Equal("B", result[1].FullName);
        }
    }
}
