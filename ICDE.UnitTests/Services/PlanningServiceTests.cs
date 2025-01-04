using AutoMapper;
using FluentValidation;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ICDE.UnitTests.Services;

public class PlanningServiceTests
{
    private MockRepository mockRepository;

    private Mock<IPlanningRepository> mockPlanningRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IOpdrachtRepository> mockOpdrachtRepository;
    private Mock<ILesRepository> mockLesRepository;
    private Mock<IValidator<MaakPlanningDto>> mockValidatorMaakPlanningDto;
    private Mock<IValidator<UpdatePlanningDto>> mockValidatorUpdatePlanningDto;

    public PlanningServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockPlanningRepository = this.mockRepository.Create<IPlanningRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockOpdrachtRepository = this.mockRepository.Create<IOpdrachtRepository>();
        this.mockLesRepository = this.mockRepository.Create<ILesRepository>();
        this.mockValidatorMaakPlanningDto = this.mockRepository.Create<IValidator<MaakPlanningDto>>();
        this.mockValidatorUpdatePlanningDto = this.mockRepository.Create<IValidator<UpdatePlanningDto>>();
    }

    private PlanningService CreateService()
    {
        return new PlanningService(
            this.mockPlanningRepository.Object,
            this.mockMapper.Object,
            null,
            this.mockOpdrachtRepository.Object,
            this.mockLesRepository.Object,
            this.mockValidatorMaakPlanningDto.Object,
            this.mockValidatorUpdatePlanningDto.Object
            );
    }

    [Fact]
    public async Task VoegOpdrachtToe_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int planningId = 0;
        Guid groupId = default(global::System.Guid);

        // Act
        var result = await service.VoegOpdrachtToe(
            planningId,
            groupId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task VoegLesToe_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int planningId = 0;
        Guid groupId = default(global::System.Guid);

        // Act
        var result = await service.VoegLesToe(
            planningId,
            groupId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task HaalLessenOpVoorPlanning_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int planningId = 0;

        // Act
        var result = await service.HaalLessenOpVoorPlanning(
            planningId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task Update_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        UpdatePlanningDto request = null;

        // Act
        var result = await service.Update(
            request);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task AlleUnieke_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();

        // Act
        var result = await service.AlleUnieke();

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task VoorId_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int planningId = 0;

        // Act
        var result = await service.VoorId(
            planningId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
}
