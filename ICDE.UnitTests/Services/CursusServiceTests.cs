using AutoMapper;
using FluentValidation;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ICDE.UnitTests.Services;

public class CursusServiceTests
{
    private MockRepository mockRepository;

    private Mock<ICursusRepository> mockCursusRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IPlanningRepository> mockPlanningRepository;
    private Mock<IValidator<UpdateCursusDto>> mockValidatorUpdateCursusDto;
    private Mock<IValidator<MaakCursusDto>> mockValidatorMaakCursusDto;

    public CursusServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockCursusRepository = this.mockRepository.Create<ICursusRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockPlanningRepository = this.mockRepository.Create<IPlanningRepository>();
        this.mockValidatorUpdateCursusDto = this.mockRepository.Create<IValidator<UpdateCursusDto>>();
        this.mockValidatorMaakCursusDto = this.mockRepository.Create<IValidator<MaakCursusDto>>();
    }

    private CursusService CreateService()
    {
        return new CursusService(
            this.mockCursusRepository.Object,
            this.mockMapper.Object,
            this.mockPlanningRepository.Object,
            this.mockValidatorUpdateCursusDto.Object,
            this.mockValidatorMaakCursusDto.Object);
    }

    [Fact]
    public async Task HaalAlleDataOp_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid cursusGroupId = default(global::System.Guid);

        // Act
        var result = await service.HaalAlleDataOp(
            cursusGroupId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task VoegPlanningToeAanCursus_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid cursusGroupId = default(global::System.Guid);
        int planningId = 0;

        // Act
        var result = await service.VoegPlanningToeAanCursus(
            cursusGroupId,
            planningId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task MaakKopie_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid groupId = default(global::System.Guid);
        int versieNummer = 0;

        // Act
        var result = await service.MaakKopie(
            groupId,
            versieNummer);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task Update_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        UpdateCursusDto request = null;

        // Act
        var result = await service.Update(
            request);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
}
