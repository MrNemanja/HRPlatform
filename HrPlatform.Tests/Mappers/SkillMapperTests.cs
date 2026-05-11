using HRPlatform.DTOs;
using HRPlatform.Mappers;
using HRPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Tests.Mappers
{
    public class SkillMapperTests
    {
        [Fact]
        public void Skill_ToDTO_ShouldMapCorrectly()
        {
            // Arrange
            Skill skill = new Skill
            {
                Id = 1,
                Name = "C#"
            };

            // Act
            SkillDTO result = SkillMapper.ToDTO(skill);

            // Assert
            Assert.Equal(skill.Id, result.Id);
            Assert.Equal(skill.Name, result.Name);
        }

        [Fact]
        public void Skill_ToEntity_ShouldMapCorrectly()
        {
            // Arrange
            CreateSkillDTO createSkillDTO = new CreateSkillDTO
            {
                Name = "Java"
            };

            // Act
            Skill result = SkillMapper.ToEntity(createSkillDTO);

            // Assert
            Assert.Equal(createSkillDTO.Name, result.Name);
        }
    }
}
