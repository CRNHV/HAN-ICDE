using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Dto.Planning;
using Moq;

namespace ICDE.UnitTests.Services;

public class PlanningServiceTests
{
    private MockRepository mockRepository;
    private Mock<ICursusRepository> mockCursusRepository;
    private Mock<IPlanningRepository> mockPlanningRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IOpdrachtRepository> mockOpdrachtRepository;
    private Mock<ILesRepository> mockLesRepository;
    private Mock<IValidator<MaakPlanningDto>> mockValidatorMaakPlanningDto;
    private IValidator<UpdatePlanningDto> mockValidatorUpdatePlanningDto;

    public PlanningServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);
        this.mockCursusRepository = this.mockRepository.Create<ICursusRepository>();
        this.mockPlanningRepository = this.mockRepository.Create<IPlanningRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockOpdrachtRepository = this.mockRepository.Create<IOpdrachtRepository>();
        this.mockLesRepository = this.mockRepository.Create<ILesRepository>();
        this.mockValidatorMaakPlanningDto = this.mockRepository.Create<IValidator<MaakPlanningDto>>();
        this.mockValidatorUpdatePlanningDto = new UpdatePlanningValidation();
    }

    private PlanningService CreateService()
    {
        return new PlanningService(
            this.mockPlanningRepository.Object,
            this.mockMapper.Object,
            this.mockCursusRepository.Object,
            this.mockOpdrachtRepository.Object,
            this.mockLesRepository.Object,
            this.mockValidatorMaakPlanningDto.Object,
            this.mockValidatorUpdatePlanningDto
            );
    }

    [Fact]
    public async Task VoegOpdrachtToe_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int planningId = 0;
        Guid groupId = Guid.NewGuid();

        var mockPlanning = new Planning
        {
            Id = planningId,
            Name = "test planning",
            PlanningItems = new List<PlanningItem>()
        };

        var mockPlanningItem = new PlanningItem
        {
            Id = planningId,
            PlanningId = planningId,
            Planning = mockPlanning
        };
        mockPlanning.PlanningItems.Add(mockPlanningItem);

        var mockOpdracht = new Opdracht
        {
            GroupId = groupId
        };

        this.mockPlanningRepository
            .Setup(repo => repo.VoorId(planningId))
            .ReturnsAsync(mockPlanning);

        this.mockOpdrachtRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(groupId))
            .ReturnsAsync(mockOpdracht);

        this.mockPlanningRepository
            .Setup(repo => repo.Update(It.IsAny<Planning>()))
            .ReturnsAsync(true);

        this.mockMapper
            .Setup(mapper => mapper.Map<PlanningZonderItemsDto>(It.IsAny<Planning>()))
            .Returns(new PlanningZonderItemsDto());

        // Act
        var result = await service.VoegOpdrachtToe(planningId, groupId);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(mockPlanning.PlanningItems, item => item.Opdracht == mockOpdracht);

        this.mockPlanningRepository.Verify(repo => repo.VoorId(planningId), Times.Once);
        this.mockOpdrachtRepository.Verify(repo => repo.NieuwsteVoorGroepId(groupId), Times.Once);
        this.mockPlanningRepository.Verify(repo => repo.Update(It.IsAny<Planning>()), Times.Once);
        this.mockMapper.Verify(mapper => mapper.Map<PlanningZonderItemsDto>(mockPlanning), Times.Once);

    }

    [Fact]
    public async Task VoegLesToe_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int planningId = 0;
        Guid groupId = Guid.NewGuid();

        var mockPlanning = new Planning
        {
            Id = planningId,
            Name = "test planning",
            PlanningItems = new List<PlanningItem>()
        };

        var mockPlanningItem = new PlanningItem
        {
            Id = planningId,
            PlanningId = planningId,
            Planning = mockPlanning
        };
        mockPlanning.PlanningItems.Add(mockPlanningItem);

        var mockLes = new Les
        {
            GroupId = groupId
        };

        this.mockPlanningRepository
            .Setup(repo => repo.VoorId(planningId))
            .ReturnsAsync(mockPlanning);

        this.mockLesRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(groupId))
            .ReturnsAsync(mockLes);

        this.mockPlanningRepository
            .Setup(repo => repo.Update(It.IsAny<Planning>()))
            .ReturnsAsync(true);

        this.mockMapper
            .Setup(mapper => mapper.Map<PlanningZonderItemsDto>(It.IsAny<Planning>()))
            .Returns(new PlanningZonderItemsDto());

        // Act
        var result = await service.VoegLesToe(planningId, groupId);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(mockPlanning.PlanningItems, item => item.Les == mockLes);

        this.mockPlanningRepository.Verify(repo => repo.VoorId(planningId), Times.Once);
        this.mockLesRepository.Verify(repo => repo.NieuwsteVoorGroepId(groupId), Times.Once);
        this.mockPlanningRepository.Verify(repo => repo.Update(It.IsAny<Planning>()), Times.Once);
        this.mockMapper.Verify(mapper => mapper.Map<PlanningZonderItemsDto>(mockPlanning), Times.Once);
    }

    [Fact]
    public async Task HaalLessenOpVoorPlanning_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int planningId = 0;
        Guid groupId = Guid.NewGuid();

        var mockPlanning = new Planning
        {
            Id = planningId,
            Name = "Test Planning",
            PlanningItems = new List<PlanningItem>
        {
            new PlanningItem
            {
                Les = new Les
                {
                    Naam = "Les 1",
                    Beschrijving = "Omschrijving van Les 1"
                }
            }
        }
        };

        var expectedLessenDto = new List<LesMetLeeruitkomstenDto>
        {
            new LesMetLeeruitkomstenDto
            {
                Naam = "Les 1",
                Beschrijving = "Omschrijving van Les 1"
            }
        };

        this.mockPlanningRepository
            .Setup(repo => repo.VoorId(planningId))
            .ReturnsAsync(mockPlanning);

        this.mockMapper
            .Setup(mapper => mapper.Map<List<LesMetLeeruitkomstenDto>>(It.IsAny<List<Les>>()))
            .Returns(expectedLessenDto);

        // Act
        var result = await service.HaalLessenOpVoorPlanning(planningId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedLessenDto.Count, result.Count);
        Assert.Equal(expectedLessenDto.First().Naam, result.First().Naam);
        Assert.Equal(expectedLessenDto.First().Beschrijving, result.First().Beschrijving);

        this.mockPlanningRepository.Verify(repo => repo.VoorId(planningId), Times.Once);
        this.mockMapper.Verify(mapper => mapper.Map<List<LesMetLeeruitkomstenDto>>(It.IsAny<List<Les>>()), Times.Once);
    }

    [Fact]
    public async Task Update_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();

        var request = new UpdatePlanningDto
        {
            Id = 1,
            Name = "Updated Description"
        };
        var existingPlanning = new Planning
        {
            Id = request.Id,
            Name = "Original Name"
        };

        this.mockPlanningRepository
           .Setup(repo => repo.VoorId(request.Id))
           .ReturnsAsync(existingPlanning);
        this.mockPlanningRepository
            .Setup(repo => repo.Update(It.IsAny<Planning>()))
            .ReturnsAsync(true);

        // Act
        var result = await service.Update(
            request);

        // Assert
        Assert.True(result);
        this.mockPlanningRepository.Verify(repo => repo.VoorId(request.Id), Times.Exactly(1));
        this.mockPlanningRepository.Verify(repo => repo.Update(It.IsAny<Planning>()), Times.Exactly(1));
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task AlleUnieke_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();

        var mockPlannings = new List<Planning>
        {
            new Planning { Id = 1, Name = "Planning 1" },
            new Planning { Id = 2, Name = "Planning 2" }
        };

        // Expected mapped result
        var expectedMappedPlannings = new List<PlanningZonderItemsDto>
        {
            new PlanningZonderItemsDto { Id = 1, Name = "Planning 1" },
            new PlanningZonderItemsDto { Id = 2, Name = "Planning 2" }
        };

        this.mockPlanningRepository
            .Setup(repo => repo.Lijst())
            .ReturnsAsync(mockPlannings);

        this.mockMapper
            .Setup(mapper => mapper.Map<List<PlanningZonderItemsDto>>(mockPlannings))
            .Returns(expectedMappedPlannings);

        // Act
        var result = await service.AlleUnieke();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMappedPlannings.Count, result.Count);
        Assert.Contains(result, p => p.Name == "Planning 1");
        Assert.Contains(result, p => p.Name == "Planning 2");

        this.mockPlanningRepository.Verify(repo => repo.Lijst(), Times.Once);
        this.mockMapper.Verify(mapper => mapper.Map<List<PlanningZonderItemsDto>>(mockPlannings), Times.Once);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task VoorId_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int planningId = 0;

        var mockPlanning = new Planning
        {
            Id = planningId,
            Name = "Test Planning",
            PlanningItems = new List<PlanningItem>()
        };

        var mockPlanningDto = new PlanningDto
        {
            Id = planningId,
            Name = "Test Planning"
        };

        this.mockPlanningRepository
            .Setup(repo => repo.VoorId(planningId))
            .ReturnsAsync(mockPlanning);

        this.mockMapper
            .Setup(mapper => mapper.Map<PlanningDto>(mockPlanning))
            .Returns(mockPlanningDto);

        // Act
        var result = await service.VoorId(planningId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockPlanningDto.Id, result.Id);
        Assert.Equal(mockPlanningDto.Name, result.Name);

        this.mockPlanningRepository.Verify(repo => repo.VoorId(planningId), Times.Once);
        this.mockMapper.Verify(mapper => mapper.Map<PlanningDto>(mockPlanning), Times.Once);
        this.mockRepository.VerifyAll();
    }
}
