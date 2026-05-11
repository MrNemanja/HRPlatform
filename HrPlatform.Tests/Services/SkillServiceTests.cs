using HRPlatform.DTOs;
using HRPlatform.Models;
using HRPlatform.Repositories;
using HRPlatform.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HRPlatform.Tests.Services
{
    public class SkillServiceTests
    {
        private readonly Mock<ISkillRepository> _skillRepoMock;
        private readonly SkillService _skillService;

        public SkillServiceTests()
        {
            _skillRepoMock = new Mock<ISkillRepository>();

            _skillService = new SkillService(_skillRepoMock.Object);
        }

        [Fact]
        public async Task AddSkill_ShouldReturnSkill_WhenValidData()
        {
            //Arrange
            CreateSkillDTO createSkillDTO = new CreateSkillDTO
            {
                Name = "Test skill name"
            };

            Skill skill = new Skill { Name = createSkillDTO.Name };

            _skillRepoMock
                .Setup(x => x.GetSkillByNameAsync(createSkillDTO.Name))
                .ReturnsAsync((Skill)null);

            _skillRepoMock
                .Setup(x => x.AddSkillAsync(It.IsAny<Skill>()))
                .ReturnsAsync(skill);

            // Act
            SkillDTO result = await _skillService.AddSkillAsync(createSkillDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(createSkillDTO.Name, result.Name);
        }

        [Fact]
        public async Task AddSkill_ShouldThrowException_WhenSkillNameAlreadyExists()
        {
            //Arrange
            CreateSkillDTO createSkillDTO = new CreateSkillDTO
            {
                Name = "Test skill name"
            };

            Skill existringSkill = new Skill { Name = createSkillDTO.Name };

            _skillRepoMock
                .Setup(x => x.GetSkillByNameAsync(createSkillDTO.Name))
                .ReturnsAsync(existringSkill);

            //Act + Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _skillService.AddSkillAsync(createSkillDTO)
            );

            Assert.Equal("Skill with this name already exists.", exception.Message);
        }
    }
}
