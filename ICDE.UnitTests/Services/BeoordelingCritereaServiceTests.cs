using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Dto.BeoordelingCriterea;
using Moq;

namespace ICDE.UnitTests.Services;

public class BeoordelingCritereaServiceTests
{
    private MockRepository mockRepository;

    private Mock<IBeoordelingCritereaRepository> mockBeoordelingCritereaRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IValidator<MaakBeoordelingCritereaDto>> mockValidatorMaakBeoordelingCritereaDto;
    private Mock<ILeeruitkomstRepository> mockLeeruitkomstRepository;
    private IValidator<UpdateBeoordelingCritereaDto> updateValidator = new UpdateBeoordelingCritereaValidation();


    public BeoordelingCritereaServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);
        mockLeeruitkomstRepository = this.mockRepository.Create<ILeeruitkomstRepository>();
        this.mockBeoordelingCritereaRepository = this.mockRepository.Create<IBeoordelingCritereaRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockValidatorMaakBeoordelingCritereaDto = this.mockRepository.Create<IValidator<MaakBeoordelingCritereaDto>>();
    }

    private BeoordelingCritereaService CreateService()
    {
        return new BeoordelingCritereaService(
            this.mockBeoordelingCritereaRepository.Object,
            this.mockMapper.Object,
            this.mockValidatorMaakBeoordelingCritereaDto.Object,
            updateValidator,
            mockLeeruitkomstRepository.Object);
    }

    [Fact]
    public async Task HaalOpMetEerdereVersies_ShouldReturnNull_WhenDbCritereaIsNull()
    {
        // Arrange
        var critereaGroupId = Guid.NewGuid();
        mockBeoordelingCritereaRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(critereaGroupId))
            .ReturnsAsync((BeoordelingCriterea?)null);

        var service = this.CreateService();

        // Act
        var result = await service.HaalOpMetEerdereVersies(critereaGroupId);

        // Assert
        Assert.Null(result);
        mockBeoordelingCritereaRepository.Verify(repo => repo.NieuwsteVoorGroepId(critereaGroupId), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<BeoordelingCritereaDto>(It.IsAny<BeoordelingCriterea>()), Times.Never);
    }

    [Fact]
    public async Task HaalOpMetEerdereVersies_ShouldReturnMappedDto_WhenDbCritereaExists()
    {
        // Arrange
        var critereaGroupId = Guid.NewGuid();
        var dbCriterea = new BeoordelingCriterea
        {
            GroupId = Guid.NewGuid(),
            VersieNummer = 1
        };
        var mappedCritereaDto = new BeoordelingCritereaDto();
        var earlierVersions = new List<BeoordelingCriterea>
        {
            new BeoordelingCriterea { GroupId = Guid.NewGuid(), VersieNummer = 0 }
        };
        var mappedEarlierVersions = new List<BeoordelingCritereaDto>
        {
            new BeoordelingCritereaDto()
        };

        mockBeoordelingCritereaRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(It.IsAny<Guid>()))
            .ReturnsAsync(dbCriterea);

        mockMapper
            .Setup(x => x.Map<List<BeoordelingCritereaDto>>(It.IsAny<List<BeoordelingCriterea>>()))
            .Returns(mappedEarlierVersions);

        mockMapper
            .Setup(mapper => mapper.Map<BeoordelingCritereaDto>(It.IsAny<BeoordelingCriterea?>()))
            .Returns(mappedCritereaDto);

        mockBeoordelingCritereaRepository
            .Setup(repo => repo.EerdereVersies(It.IsAny<Guid>(), It.IsAny<int>()))
            .ReturnsAsync(earlierVersions);

        var service = this.CreateService();

        // Act
        var result = await service.HaalOpMetEerdereVersies(critereaGroupId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mappedCritereaDto, result?.BeoordelingCriterea);
        Assert.Equal(mappedEarlierVersions, result?.EerdereVersies);

        mockRepository.VerifyAll();
    }

    [Fact]
    public async Task MaakKopie_ShouldCloneAndSaveCriterea_ReturnsNewGroupId()
    {
        // Arrange              

        var service = this.CreateService();

        var groupId = Guid.NewGuid();
        var versieNummer = 1;
        var originalCriterea = new BeoordelingCriterea
        {
            GroupId = groupId,
            Naam = "Original Criterea"
        };

        mockBeoordelingCritereaRepository
            .Setup(repo => repo.Versie(It.IsAny<Guid>(), It.IsAny<int>()))
            .ReturnsAsync(originalCriterea);

        mockBeoordelingCritereaRepository
            .Setup(repo => repo.Maak(It.IsAny<BeoordelingCriterea>()))
            .ReturnsAsync(new BeoordelingCriterea());

        // Act
        var result = await service.MaakKopie(groupId, versieNummer);

        // Assert
        Assert.NotEqual(groupId, result);

        mockRepository.VerifyAll();
    }

    [Fact]
    public async Task Update_ShouldThrowValidationException_WhenValidationFails()
    {
        // Arrange
        var service = this.CreateService();
        var request = new UpdateBeoordelingCritereaDto();

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => service.Update(request));
        mockBeoordelingCritereaRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Update_ShouldReturnFalse_WhenNoCriteriaFound()
    {
        // Arrange
        var service = this.CreateService();
        var request = new UpdateBeoordelingCritereaDto { GroupId = Guid.NewGuid(), Naam = "Naam1", Beschrijving = "Beschrijving" };

        mockBeoordelingCritereaRepository
            .Setup(r => r.NieuwsteVoorGroepId(request.GroupId))
            .ReturnsAsync((BeoordelingCriterea)null);

        // Act
        var result = await service.Update(request);

        // Assert
        Assert.False(result);
        mockRepository.VerifyAll();
    }

    [Fact]
    public async Task Update_ShouldReturnFalse_WhenUpdateFails()
    {
        // Arrange
        var service = this.CreateService();
        var request = new UpdateBeoordelingCritereaDto
        {
            GroupId = Guid.NewGuid(),
            Naam = "New Name",
            Beschrijving = "New Description"
        };

        var dbCriterea = new BeoordelingCriterea { Id = 1, GroupId = Guid.NewGuid(), };

        mockBeoordelingCritereaRepository
            .Setup(r => r.NieuwsteVoorGroepId(request.GroupId))
            .ReturnsAsync(dbCriterea);

        mockBeoordelingCritereaRepository
            .Setup(r => r.Update(dbCriterea))
            .ReturnsAsync(false);

        // Act
        var result = await service.Update(request);

        // Assert
        Assert.False(result);
        mockRepository.VerifyAll();
    }

    [Fact]
    public async Task Update_ShouldReturnTrue_WhenUpdateSucceeds()
    {
        // Arrange
        var service = this.CreateService();
        var request = new UpdateBeoordelingCritereaDto
        {
            GroupId = Guid.NewGuid(),
            Naam = "Updated Name",
            Beschrijving = "Updated Description"
        };

        var dbCriterea = new BeoordelingCriterea
        {
            Id = 1,
            GroupId = Guid.NewGuid(),
            Naam = "Old Name",
            Beschrijving = "Old Description",
            Leeruitkomsten = new List<Leeruitkomst>()
        };

        var updatedCriterea = new BeoordelingCriterea
        {
            Id = 1,
            GroupId = Guid.NewGuid(),
            Naam = "Updated Name",
            Beschrijving = "Updated Description",
            Leeruitkomsten = new List<Leeruitkomst>()
        };

        mockBeoordelingCritereaRepository
            .SetupSequence(r => r.NieuwsteVoorGroepId(request.GroupId))
            .ReturnsAsync(dbCriterea)
            .ReturnsAsync(updatedCriterea);

        mockBeoordelingCritereaRepository
            .Setup(r => r.Update(It.IsAny<BeoordelingCriterea>()))
            .ReturnsAsync(true);

        // Act
        var result = await service.Update(request);

        // Assert
        Assert.True(result);
        mockRepository.VerifyAll();
    }
}
