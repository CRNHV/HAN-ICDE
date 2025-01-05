using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Dto.Opleiding;
using Moq;

namespace ICDE.UnitTests.Services;

public class OpleidingServiceTests
{
    private MockRepository mockRepository;

    private Mock<IOpleidingRepository> mockOpleidingRepository;
    private Mock<IVakRepository> mockVakRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IValidator<MaakOpleidingDto>> mockValidatorMaakOpleidingDto;
    private IValidator<UpdateOpleidingDto> mockValidatorUpdateOpleidingDto;

    public OpleidingServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockOpleidingRepository = this.mockRepository.Create<IOpleidingRepository>();
        this.mockVakRepository = this.mockRepository.Create<IVakRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockValidatorMaakOpleidingDto = this.mockRepository.Create<IValidator<MaakOpleidingDto>>();
        this.mockValidatorUpdateOpleidingDto = new UpdateOpleidingValidation();
    }

    private OpleidingService CreateService()
    {
        return new OpleidingService(
            this.mockOpleidingRepository.Object,
            this.mockVakRepository.Object,
            this.mockMapper.Object,
            this.mockValidatorMaakOpleidingDto.Object,
            this.mockValidatorUpdateOpleidingDto);
    }

    [Fact]
    public async Task KoppelVakAanOpleiding_WhenOpleidingAndVakExist_ShouldReturnTrue()
    {
        // Arrange
        var service = this.CreateService();

        var opleidingGroupId = Guid.NewGuid();
        var vakGroupId = Guid.NewGuid();

        var mockOpleiding = new Opleiding
        {
            GroupId = Guid.NewGuid(),
            Vakken = new List<Vak>(),
            RelationshipChanged = false
        };

        var mockVak = new Vak
        {
            GroupId = Guid.NewGuid()
        };

        this.mockOpleidingRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(opleidingGroupId))
            .ReturnsAsync(mockOpleiding);

        this.mockVakRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(vakGroupId))
            .ReturnsAsync(mockVak);

        this.mockOpleidingRepository
            .Setup(repo => repo.Update(mockOpleiding))
            .ReturnsAsync(true);

        // Act
        var result = await service.KoppelVakAanOpleiding(opleidingGroupId, vakGroupId);

        // Assert
        Assert.True(result);
        Assert.Contains(mockVak, mockOpleiding.Vakken);
        Assert.True(mockOpleiding.RelationshipChanged);

        this.mockOpleidingRepository.Verify(repo => repo.NieuwsteVoorGroepId(opleidingGroupId), Times.Once);
        this.mockVakRepository.Verify(repo => repo.NieuwsteVoorGroepId(vakGroupId), Times.Once);
        this.mockOpleidingRepository.Verify(repo => repo.Update(mockOpleiding), Times.Once);

        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task ZoekOpleidingMetEerdereVersies_HappyPath_ReturnsExpectedResult()
    {
        // Arrange
        var service = this.CreateService();
        Guid groupId = Guid.NewGuid();

        // Mocked data
        var nieuwsteOpleiding = new Opleiding // Example model, replace with your actual entity
        {
            GroupId = groupId,
            Naam = "Opleiding 1"
        };

        var eerdereVersies = new List<Opleiding>
    {
        new Opleiding { GroupId = groupId, Naam = "Opleiding 2" },
        new Opleiding { GroupId = groupId, Naam = "Opleiding 3" }
    };

        var mappedOpleidingDto = new OpleidingMetVakkenDto // Example DTO, replace with your actual DTO
        {
            Naam = nieuwsteOpleiding.Naam
        };

        var mappedEerdereVersies = eerdereVersies.Select(x => new OpleidingDto
        {
            Naam = x.Naam
        }).ToList();

        // Set up mocks
        this.mockOpleidingRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(groupId))
            .ReturnsAsync(nieuwsteOpleiding);

        this.mockOpleidingRepository
            .Setup(repo => repo.Lijst(It.IsAny<Expression<Func<Opleiding, bool>>>()))
            .ReturnsAsync(eerdereVersies);

        this.mockMapper
            .Setup(mapper => mapper.Map<OpleidingMetVakkenDto>(nieuwsteOpleiding))
            .Returns(mappedOpleidingDto);

        this.mockMapper
            .Setup(mapper => mapper.Map<List<OpleidingDto>>(eerdereVersies))
            .Returns(mappedEerdereVersies);

        // Act
        var result = await service.ZoekOpleidingMetEerdereVersies(groupId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mappedOpleidingDto, result.OpleidingDto);
        Assert.Equal(mappedEerdereVersies, result.EerdereVersies);

        this.mockOpleidingRepository.Verify(repo => repo.NieuwsteVoorGroepId(groupId), Times.Once);
        this.mockOpleidingRepository.Verify(repo => repo.Lijst(It.IsAny<Expression<Func<Opleiding, bool>>>()), Times.Once);
        this.mockMapper.Verify(mapper => mapper.Map<OpleidingMetVakkenDto>(nieuwsteOpleiding), Times.Once);
        this.mockMapper.Verify(mapper => mapper.Map<List<OpleidingDto>>(eerdereVersies), Times.Once);
    }

    [Fact]
    public async Task Update_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var service = this.CreateService();

        var validRequest = new UpdateOpleidingDto
        {
            GroupId = Guid.NewGuid(),
            Naam = "Updated Opleiding",
            Beschrijving = "Updated Beschrijving"
        };

        var existingOpleiding = new Opleiding
        {
            GroupId = validRequest.GroupId,
            Naam = "Existing Opleiding",
            Beschrijving = "Existing Beschrijving",
            Vakken = new List<Vak>()
        };

        var updatedOpleiding = new Opleiding
        {
            GroupId = validRequest.GroupId,
            Naam = "Updated Opleiding",
            Beschrijving = "Updated Beschrijving",
            Vakken = new List<Vak>(),
            RelationshipChanged = true
        };

        // Mock repository methods
        mockOpleidingRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(validRequest.GroupId))
            .ReturnsAsync(existingOpleiding); // Simulate retrieving the existing opleiding

        mockOpleidingRepository
            .Setup(repo => repo.Update(It.IsAny<Opleiding>()))
            .ReturnsAsync(true) // Simulate successful update
            .Verifiable();

        // Act
        var result = await service.Update(validRequest);

        // Assert
        Assert.True(result, "The update should return true for a valid request.");
        mockOpleidingRepository.Verify(repo => repo.NieuwsteVoorGroepId(validRequest.GroupId), Times.Exactly(2));
        mockOpleidingRepository.Verify(repo => repo.Update(It.IsAny<Opleiding>()), Times.Exactly(2));
    }
}
