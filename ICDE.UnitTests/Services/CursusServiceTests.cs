using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Dto.Cursus;
using Moq;

namespace ICDE.UnitTests.Services;

public class CursusServiceTests
{
    private MockRepository mockRepository;

    private Mock<ICursusRepository> mockCursusRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IPlanningRepository> mockPlanningRepository;
    private IValidator<UpdateCursusDto> mockValidatorUpdateCursusDto;
    private Mock<IValidator<MaakCursusDto>> mockValidatorMaakCursusDto;
    private Mock<ILeeruitkomstRepository> mockLeeruitkomstRepository;

    public CursusServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockCursusRepository = this.mockRepository.Create<ICursusRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockPlanningRepository = this.mockRepository.Create<IPlanningRepository>();
        this.mockValidatorUpdateCursusDto = new UpdateCursusValidation();
        this.mockValidatorMaakCursusDto = this.mockRepository.Create<IValidator<MaakCursusDto>>();
        this.mockLeeruitkomstRepository = this.mockRepository.Create<ILeeruitkomstRepository>();
    }

    private CursusService CreateService()
    {
        return new CursusService(
            this.mockCursusRepository.Object,
            this.mockMapper.Object,
            this.mockPlanningRepository.Object,
            this.mockValidatorUpdateCursusDto,
            this.mockValidatorMaakCursusDto.Object,
            this.mockLeeruitkomstRepository.Object);
    }

    [Fact]
    public async Task HaalAlleDataOp_CursusExists_ReturnsMappedDto()
    {
        // Arrange
        var service = this.CreateService();
        Guid cursusGroupId = Guid.NewGuid();

        var mockCursus = new Cursus
        {
            GroupId = Guid.NewGuid(),
            Naam = "Sample Cursus"
        };

        var expectedDto = new CursusMetPlanningDto
        {
            GroupId = mockCursus.GroupId,
            Naam = mockCursus.Naam
        };

        mockCursusRepository
            .Setup(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId))
            .ReturnsAsync(mockCursus);

        mockMapper
            .Setup(mapper => mapper.Map<CursusMetPlanningDto>(mockCursus))
            .Returns(expectedDto);

        // Act
        var result = await service.HaalAlleDataOp(cursusGroupId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedDto.Id, result.Id);
        Assert.Equal(expectedDto.Naam, result.Naam);
        this.mockCursusRepository.Verify(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId), Times.Once);
        this.mockMapper.Verify(mapper => mapper.Map<CursusMetPlanningDto>(mockCursus), Times.Once);
    }

    [Fact]
    public async Task HaalAlleDataOp_CursusDoesNotExist_ReturnsNull()
    {
        // Arrange
        var service = this.CreateService();
        Guid cursusGroupId = Guid.NewGuid();

        mockCursusRepository
            .Setup(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId))
            .ReturnsAsync((Cursus)null);

        // Act
        var result = await service.HaalAlleDataOp(cursusGroupId);

        // Assert
        Assert.Null(result);
        this.mockCursusRepository.Verify(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<CursusMetPlanningDto>(It.IsAny<Cursus>()), Times.Never);
    }

    [Fact]
    public async Task VoegPlanningToeAanCursus_CursusNotFound_ReturnsFalse()
    {
        // Arrange
        var service = CreateService();
        var cursusGroupId = Guid.NewGuid();
        var planningId = 1;

        mockCursusRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(cursusGroupId))
            .ReturnsAsync((Cursus)null);

        // Act
        var result = await service.VoegPlanningToeAanCursus(cursusGroupId, planningId);

        // Assert
        Assert.False(result);
        mockCursusRepository.Verify(repo => repo.NieuwsteVoorGroepId(cursusGroupId), Times.Once);
        mockPlanningRepository.Verify(repo => repo.CreateCloneOf(It.IsAny<int>()), Times.Never);
        mockCursusRepository.Verify(repo => repo.Update(It.IsAny<Cursus>()), Times.Never);
    }

    [Fact]
    public async Task VoegPlanningToeAanCursus_PlanningNotFound_ReturnsFalse()
    {
        // Arrange
        var service = CreateService();
        var cursusGroupId = Guid.NewGuid();
        var planningId = 1;
        var cursus = new Cursus();

        mockCursusRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(cursusGroupId))
            .ReturnsAsync(cursus);

        mockPlanningRepository
            .Setup(repo => repo.CreateCloneOf(planningId))
            .ReturnsAsync((Planning)null);

        // Act
        var result = await service.VoegPlanningToeAanCursus(cursusGroupId, planningId);

        // Assert
        Assert.False(result);
        mockCursusRepository.Verify(repo => repo.NieuwsteVoorGroepId(cursusGroupId), Times.Once);
        mockPlanningRepository.Verify(repo => repo.CreateCloneOf(planningId), Times.Once);
        mockCursusRepository.Verify(repo => repo.Update(It.IsAny<Cursus>()), Times.Never);
    }

    [Fact]
    public async Task VoegPlanningToeAanCursus_UpdateSuccessful_ReturnsTrue()
    {
        // Arrange
        var service = CreateService();
        var cursusGroupId = Guid.NewGuid();
        var planningId = 1;
        var cursus = new Cursus();
        var planning = new Planning();

        mockCursusRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(cursusGroupId))
            .ReturnsAsync(cursus);

        mockPlanningRepository
            .Setup(repo => repo.CreateCloneOf(planningId))
            .ReturnsAsync(planning);

        mockCursusRepository
            .Setup(repo => repo.Update(It.IsAny<Cursus>()))
            .ReturnsAsync(true);

        // Act
        var result = await service.VoegPlanningToeAanCursus(cursusGroupId, planningId);

        // Assert
        Assert.True(result);
        Assert.True(cursus.RelationshipChanged);
        Assert.Equal(planning, cursus.Planning);

        mockCursusRepository.Verify(repo => repo.NieuwsteVoorGroepId(cursusGroupId), Times.Once);
        mockPlanningRepository.Verify(repo => repo.CreateCloneOf(planningId), Times.Once);
        mockCursusRepository.Verify(repo => repo.Update(cursus), Times.Once);
    }

    [Fact]
    public async Task MaakKopie_WhenCursusIsNull_ReturnsEmptyGuid()
    {
        // Arrange
        var service = CreateService();
        var groupId = Guid.NewGuid();
        var versieNummer = 1;

        mockCursusRepository
            .Setup(repo => repo.Versie(groupId, versieNummer))
            .ReturnsAsync((Cursus)null);

        // Act
        var result = await service.MaakKopie(groupId, versieNummer);

        // Assert
        Assert.Equal(Guid.Empty, result);
        mockCursusRepository.Verify(repo => repo.Versie(groupId, versieNummer), Times.Once);
        mockCursusRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task MaakKopie_WhenCursusExists_ReturnsNewGroupId()
    {
        // Arrange
        var service = CreateService();
        var groupId = Guid.NewGuid();
        var versieNummer = 1;

        var originalCursus = new Cursus
        {
            GroupId = groupId
        };
        var clonedCursus = new Cursus
        {
            GroupId = Guid.NewGuid()
        };

        mockCursusRepository
            .Setup(repo => repo.Versie(groupId, versieNummer))
            .ReturnsAsync(originalCursus);

        mockCursusRepository
            .Setup(repo => repo.Maak(It.IsAny<Cursus>()))
            .ReturnsAsync(new Cursus());

        // Act
        var result = await service.MaakKopie(groupId, versieNummer);

        // Assert
        Assert.NotEqual(originalCursus.GroupId, result);
        mockRepository.VerifyAll();
    }

    [Fact]
    public async Task Update_UpdateFails_ReturnsFalse()
    {
        // Arrange
        var service = this.CreateService();
        var request = new UpdateCursusDto
        {
            GroupId = Guid.NewGuid(),
            Naam = "Updated Name",
            Beschrijving = "Updated Description"
        };

        var dbCursus = new Cursus { Naam = "Old Name", Beschrijving = "Old Description", Planning = null };

        mockCursusRepository
            .Setup(r => r.GetFullObjectTreeByGroupId(request.GroupId))
            .ReturnsAsync(dbCursus);

        mockCursusRepository
            .Setup(r => r.Update(It.IsAny<Cursus>()))
            .ReturnsAsync(false);

        // Act
        var result = await service.Update(request);

        // Assert
        Assert.False(result);
        mockCursusRepository.Verify(r => r.GetFullObjectTreeByGroupId(request.GroupId), Times.Once);
        mockCursusRepository.Verify(r => r.Update(It.IsAny<Cursus>()), Times.Once);
    }

    [Fact]
    public async Task KoppelLuk_ShouldReturnFalse_WhenCursusIsNull()
    {
        // Arrange
        var service = CreateService();
        var cursusGroupId = Guid.NewGuid();
        var lukGroupId = Guid.NewGuid();

        mockCursusRepository
            .Setup(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId))
            .ReturnsAsync((Cursus)null);

        // Act
        var result = await service.KoppelLuk(cursusGroupId, lukGroupId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task KoppelLuk_ShouldReturnFalse_WhenLeeruitkomstIsNull()
    {
        // Arrange
        var service = CreateService();
        var cursusGroupId = Guid.NewGuid();
        var lukGroupId = Guid.NewGuid();

        var cursus = new Cursus { Leeruitkomsten = new List<Leeruitkomst>() };

        mockCursusRepository
            .Setup(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId))
            .ReturnsAsync(cursus);

        mockLeeruitkomstRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(lukGroupId))
            .ReturnsAsync((Leeruitkomst)null);

        // Act
        var result = await service.KoppelLuk(cursusGroupId, lukGroupId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task KoppelLuk_ShouldReturnTrue_WhenLeeruitkomstAlreadyExists()
    {
        // Arrange
        var service = CreateService();
        var cursusGroupId = Guid.NewGuid();
        var lukGroupId = Guid.NewGuid();

        var luk = new Leeruitkomst();
        var cursus = new Cursus { Leeruitkomsten = new List<Leeruitkomst> { luk } };

        mockCursusRepository
            .Setup(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId))
            .ReturnsAsync(cursus);

        mockLeeruitkomstRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(lukGroupId))
            .ReturnsAsync(luk);

        // Act
        var result = await service.KoppelLuk(cursusGroupId, lukGroupId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task KoppelLuk_ShouldAddLeeruitkomstAndReturnTrue_WhenLeeruitkomstIsNew()
    {
        // Arrange
        var service = CreateService();
        var cursusGroupId = Guid.NewGuid();
        var lukGroupId = Guid.NewGuid();

        var luk = new Leeruitkomst();
        var cursus = new Cursus { Leeruitkomsten = new List<Leeruitkomst>() };

        mockCursusRepository
            .Setup(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId))
            .ReturnsAsync(cursus);

        mockLeeruitkomstRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(lukGroupId))
            .ReturnsAsync(luk);

        mockCursusRepository
            .Setup(repo => repo.Update(cursus))
            .ReturnsAsync(true);

        // Act
        var result = await service.KoppelLuk(cursusGroupId, lukGroupId);

        // Assert
        Assert.True(result);
        Assert.Contains(luk, cursus.Leeruitkomsten);
        Assert.True(cursus.RelationshipChanged);
        mockCursusRepository.Verify(repo => repo.Update(cursus), Times.Once);
    }

    [Fact]
    public async Task OntkoppelLuk_ShouldReturnFalse_WhenCursusIsNull()
    {
        // Arrange
        var service = CreateService();
        Guid cursusGroupId = Guid.NewGuid();
        Guid lukGroupId = Guid.NewGuid();

        mockCursusRepository
            .Setup(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId))
            .ReturnsAsync((Cursus)null);

        // Act
        var result = await service.OntkoppelLuk(cursusGroupId, lukGroupId);

        // Assert
        Assert.False(result);
        mockCursusRepository.Verify(repo => repo.Update(It.IsAny<Cursus>()), Times.Never);
    }

    [Fact]
    public async Task OntkoppelLuk_ShouldReturnFalse_WhenLukIsNull()
    {
        // Arrange
        var service = CreateService();
        Guid cursusGroupId = Guid.NewGuid();
        Guid lukGroupId = Guid.NewGuid();

        var cursus = new Cursus { Leeruitkomsten = new List<Leeruitkomst>() };

        mockCursusRepository
            .Setup(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId))
            .ReturnsAsync(cursus);

        mockLeeruitkomstRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(lukGroupId))
            .ReturnsAsync((Leeruitkomst)null);

        // Act
        var result = await service.OntkoppelLuk(cursusGroupId, lukGroupId);

        // Assert
        Assert.False(result);
        mockCursusRepository.Verify(repo => repo.Update(It.IsAny<Cursus>()), Times.Never);
    }

    [Fact]
    public async Task OntkoppelLuk_ShouldReturnTrue_WhenLukIsNotInCursus()
    {
        // Arrange
        var service = CreateService();
        Guid cursusGroupId = Guid.NewGuid();
        Guid lukGroupId = Guid.NewGuid();

        var cursus = new Cursus
        {
            Leeruitkomsten = new List<Leeruitkomst>
            {
                new Leeruitkomst { GroupId = Guid.NewGuid() }
            }
        };

        var luk = new Leeruitkomst { GroupId = lukGroupId };

        mockCursusRepository
            .Setup(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId))
            .ReturnsAsync(cursus);

        mockLeeruitkomstRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(lukGroupId))
            .ReturnsAsync(luk);

        // Act
        var result = await service.OntkoppelLuk(cursusGroupId, lukGroupId);

        // Assert
        Assert.True(result);
        mockCursusRepository.Verify(repo => repo.Update(It.IsAny<Cursus>()), Times.Never);
    }

    [Fact]
    public async Task OntkoppelLuk_ShouldRemoveLukAndReturnTrue_WhenLukExistsInCursus()
    {
        // Arrange
        var service = CreateService();
        Guid cursusGroupId = Guid.NewGuid();
        Guid lukGroupId = Guid.NewGuid();

        var luk = new Leeruitkomst { GroupId = lukGroupId };
        var cursus = new Cursus
        {
            Leeruitkomsten = new List<Leeruitkomst> { luk }
        };

        mockCursusRepository
            .Setup(repo => repo.GetFullObjectTreeByGroupId(cursusGroupId))
            .ReturnsAsync(cursus);

        mockLeeruitkomstRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(lukGroupId))
            .ReturnsAsync(luk);

        mockCursusRepository
            .Setup(repo => repo.Update(It.IsAny<Cursus>()))
            .ReturnsAsync(true);

        // Act
        var result = await service.OntkoppelLuk(cursusGroupId, lukGroupId);

        // Assert
        Assert.True(result);
        Assert.Empty(cursus.Leeruitkomsten);
        Assert.True(cursus.RelationshipChanged);
        mockCursusRepository.Verify(repo => repo.Update(It.Is<Cursus>(c => c.RelationshipChanged)), Times.Once);
    }
}
