using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Dto.Opdracht;
using Moq;

namespace ICDE.UnitTests.Services;

public class OpdrachtServiceTests
{
    private MockRepository mockRepository;

    private Mock<IOpdrachtRepository> mockOpdrachtRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IBeoordelingCritereaRepository> mockBeoordelingCritereaRepository;
    private Mock<IValidator<MaakOpdrachtDto>> mockValidatorMaakOpdrachtDto;
    private IValidator<UpdateOpdrachtDto> mockValidatorUpdateOpdrachtDto;

    public OpdrachtServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockOpdrachtRepository = this.mockRepository.Create<IOpdrachtRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockBeoordelingCritereaRepository = this.mockRepository.Create<IBeoordelingCritereaRepository>();
        this.mockValidatorMaakOpdrachtDto = this.mockRepository.Create<IValidator<MaakOpdrachtDto>>();
        this.mockValidatorUpdateOpdrachtDto = new UpdateOpdrachtValidation();
    }

    private OpdrachtService CreateService()
    {
        return new OpdrachtService(
            this.mockOpdrachtRepository.Object,
            this.mockMapper.Object,
            this.mockBeoordelingCritereaRepository.Object,
            this.mockValidatorMaakOpdrachtDto.Object,
            this.mockValidatorUpdateOpdrachtDto);
    }

    [Fact]
    public async Task HaalAlleDataOp_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid opdrachtGroupId = Guid.NewGuid();

        var dbOpdracht = new Opdracht()
        {
            GroupId = opdrachtGroupId,
        };

        mockOpdrachtRepository
            .Setup(x => x.GetFullDataByGroupId(It.Is<Guid>(x => x == opdrachtGroupId)))
            .ReturnsAsync(dbOpdracht);

        mockOpdrachtRepository
            .Setup(x => x.EerdereVersies(It.Is<Guid>(x => x == opdrachtGroupId), It.IsAny<int>()))
            .ReturnsAsync(new List<Opdracht>());

        mockMapper
            .Setup(x => x.Map<List<BeoordelingCritereaDto>>(It.IsAny<object>()))
            .Returns(new List<BeoordelingCritereaDto>());

        mockMapper
            .Setup(x => x.Map<List<OpdrachtDto>>(It.IsAny<object>()))
            .Returns(new List<OpdrachtDto>());

        mockMapper
            .Setup(x => x.Map<OpdrachtDto>(It.IsAny<object>()))
            .Returns(new OpdrachtDto(false));

        // Act
        var result = await service.HaalAlleDataOp(
            opdrachtGroupId);

        // Assert
        Assert.NotNull(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task VoegCritereaToe_NoOpdrachten_ReturnsFalse()
    {
        // Arrange
        var opdrachtGroupId = Guid.NewGuid();
        var critereaGroupId = Guid.NewGuid();

        mockOpdrachtRepository
            .Setup(repo => repo.Lijst(It.IsAny<Expression<Func<Opdracht, bool>>>()))
            .ReturnsAsync(new List<Opdracht>());

        var service = CreateService();

        // Act
        var result = await service.VoegCritereaToe(opdrachtGroupId, critereaGroupId);

        // Assert
        Assert.False(result);
        mockOpdrachtRepository.Verify(repo => repo.Lijst(It.IsAny<Expression<Func<Opdracht, bool>>>()), Times.Once);
        mockBeoordelingCritereaRepository.Verify(repo => repo.Lijst(It.IsAny<Expression<Func<BeoordelingCriterea, bool>>>()), Times.Never);
    }

    [Fact]
    public async Task VoegCritereaToe_NoCriterea_ReturnsFalse()
    {
        // Arrange
        var opdrachtGroupId = Guid.NewGuid();
        var critereaGroupId = Guid.NewGuid();

        mockOpdrachtRepository
            .Setup(repo => repo.Lijst(It.IsAny<Expression<Func<Opdracht, bool>>>()))
            .ReturnsAsync(new List<Opdracht> { new Opdracht { GroupId = opdrachtGroupId } });

        mockBeoordelingCritereaRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(It.IsAny<Guid>()))
            .ReturnsAsync((BeoordelingCriterea?)null);

        var service = CreateService();

        // Act
        var result = await service.VoegCritereaToe(opdrachtGroupId, critereaGroupId);

        // Assert
        Assert.False(result);
        mockOpdrachtRepository.Verify(repo => repo.Lijst(It.IsAny<Expression<Func<Opdracht, bool>>>()), Times.Once);
        mockBeoordelingCritereaRepository.Verify(repo => repo.NieuwsteVoorGroepId(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task VoegCritereaToe_ValidData_UpdatesAndReturnsTrue()
    {
        // Arrange
        var opdrachtGroupId = Guid.NewGuid();
        var critereaGroupId = Guid.NewGuid();

        var opdrachten = new List<Opdracht>
        {
            new Opdracht
            {
                GroupId = opdrachtGroupId,
                BeoordelingCritereas = new List<BeoordelingCriterea>(),
                RelationshipChanged = false
            }
        };

        mockOpdrachtRepository
            .Setup(repo => repo.Lijst(It.IsAny<Expression<Func<Opdracht, bool>>>()))
            .ReturnsAsync(opdrachten);

        mockBeoordelingCritereaRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(It.IsAny<Guid>()))
            .ReturnsAsync(new BeoordelingCriterea { GroupId = critereaGroupId });

        mockOpdrachtRepository
            .Setup(repo => repo.Update(It.IsAny<Opdracht>()))
            .ReturnsAsync(true);

        var service = CreateService();

        // Act
        var result = await service.VoegCritereaToe(opdrachtGroupId, critereaGroupId);

        // Assert
        Assert.True(result);
        mockOpdrachtRepository.Verify(repo => repo.Lijst(It.IsAny<Expression<Func<Opdracht, bool>>>()), Times.Once);
        mockBeoordelingCritereaRepository.Verify(repo => repo.NieuwsteVoorGroepId(It.IsAny<Guid>()), Times.Once);
        mockOpdrachtRepository.Verify(repo => repo.Update(It.IsAny<Opdracht>()), Times.Exactly(opdrachten.Count));
    }


    [Fact]
    public async Task HaalStudentOpdrachtDataOp_OpdrachtIsNull_ReturnsNull()
    {
        // Arrange
        var opdrachtGroupId = Guid.NewGuid();
        mockOpdrachtRepository
            .Setup(repo => repo.GetFullDataByGroupId(opdrachtGroupId))
            .ReturnsAsync((Opdracht?)null);

        var service = CreateService();

        // Act
        var result = await service.HaalStudentOpdrachtDataOp(opdrachtGroupId);

        // Assert
        Assert.Null(result);
        mockOpdrachtRepository.Verify(repo => repo.GetFullDataByGroupId(opdrachtGroupId), Times.Once);
    }

    [Fact]
    public async Task HaalStudentOpdrachtDataOp_OpdrachtIsNotNull_ReturnsMappedStudentOpdrachtDto()
    {
        // Arrange
        var opdrachtGroupId = Guid.NewGuid();
        var opdracht = new Opdracht
        {
            GroupId = Guid.NewGuid(),
            BeoordelingCritereas = new List<BeoordelingCriterea>
        {
            new BeoordelingCriterea { GroupId = Guid.NewGuid(), Naam = "Criteria1" },
            new BeoordelingCriterea { GroupId = Guid.NewGuid(), Naam = "Criteria2" }
        }
        };

        var opdrachtDto = new OpdrachtDto(true) { GroupId = opdracht.GroupId };
        var beoordelingCritereasDto = new List<BeoordelingCritereaDto>
        {
            new BeoordelingCritereaDto { GroupId = opdracht.BeoordelingCritereas[0].GroupId, Naam = "Criteria1" },
            new BeoordelingCritereaDto { GroupId = opdracht.BeoordelingCritereas[1].GroupId, Naam = "Criteria2" }
        };

        mockOpdrachtRepository
            .Setup(repo => repo.GetFullDataByGroupId(opdrachtGroupId))
            .ReturnsAsync(opdracht);

        mockMapper
            .Setup(mapper => mapper.Map<OpdrachtDto>(opdracht))
            .Returns(opdrachtDto);

        mockMapper
            .Setup(mapper => mapper.Map<List<BeoordelingCritereaDto>>(opdracht.BeoordelingCritereas))
            .Returns(beoordelingCritereasDto);

        var service = CreateService();

        // Act
        var result = await service.HaalStudentOpdrachtDataOp(opdrachtGroupId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(opdracht.Id, result!.OpdrachtId);
        Assert.Equal(opdrachtDto, result.Opdracht);
        Assert.Equal(beoordelingCritereasDto, result.BeoordelingCritereas);

        mockOpdrachtRepository.Verify(repo => repo.GetFullDataByGroupId(opdrachtGroupId), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<OpdrachtDto>(opdracht), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<List<BeoordelingCritereaDto>>(opdracht.BeoordelingCritereas), Times.Once);
    }

    [Fact]
    public async Task Update_HappyPath_ReturnsTrue()
    {
        // Arrange
        var service = this.CreateService();

        var request = new UpdateOpdrachtDto
        {
            GroupId = Guid.NewGuid(),
            Naam = "Updated Name",
            Beschrijving = "Updated Description",
            IsToets = true
        };
        var existingOpdracht = new Opdracht
        {
            GroupId = request.GroupId,
            Naam = "Original Name",
            Beschrijving = "Original Description",
            Type = OpdrachtType.Casus,
            BeoordelingCritereas = new List<BeoordelingCriterea>()
        };
        this.mockOpdrachtRepository
            .Setup(repo => repo.GetFullDataByGroupId(request.GroupId))
            .ReturnsAsync(existingOpdracht);
        this.mockOpdrachtRepository
            .Setup(repo => repo.Update(It.IsAny<Opdracht>()))
            .ReturnsAsync(true);

        // Act
        var result = await service.Update(request);

        // Assert
        Assert.True(result);
        this.mockOpdrachtRepository.Verify(repo => repo.GetFullDataByGroupId(request.GroupId), Times.Exactly(2));
        this.mockOpdrachtRepository.Verify(repo => repo.Update(It.IsAny<Opdracht>()), Times.Exactly(2));
        this.mockRepository.VerifyAll();
    }
}
