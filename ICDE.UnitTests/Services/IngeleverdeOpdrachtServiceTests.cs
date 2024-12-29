using AutoMapper;
using FluentValidation;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Dto.OpdrachtInzending;
using ICDE.Lib.IO;
using ICDE.Lib.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ICDE.UnitTests.Services;

public class IngeleverdeOpdrachtServiceTests
{
    private MockRepository mockRepository;

    private Mock<IIngeleverdeOpdrachtRepository> mockIngeleverdeOpdrachtRepository;
    private Mock<IOpdrachtRepository> mockOpdrachtRepository;
    private Mock<IFileManager> mockFileManager;
    private Mock<IOpdrachtBeoordelingRepository> mockOpdrachtBeoordelingRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IStudentRepository> mockStudentRepository;
    private Mock<IValidator<OpdrachtBeoordelingDto>> mockValidatorOpdrachtBeoordelingDto;
    private Mock<IValidator<LeverOpdrachtInDto>> mockValidatorLeverOpdrachtInDto;

    public IngeleverdeOpdrachtServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockIngeleverdeOpdrachtRepository = this.mockRepository.Create<IIngeleverdeOpdrachtRepository>();
        this.mockOpdrachtRepository = this.mockRepository.Create<IOpdrachtRepository>();
        this.mockFileManager = this.mockRepository.Create<IFileManager>();
        this.mockOpdrachtBeoordelingRepository = this.mockRepository.Create<IOpdrachtBeoordelingRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockStudentRepository = this.mockRepository.Create<IStudentRepository>();
        this.mockValidatorOpdrachtBeoordelingDto = this.mockRepository.Create<IValidator<OpdrachtBeoordelingDto>>();
        this.mockValidatorLeverOpdrachtInDto = this.mockRepository.Create<IValidator<LeverOpdrachtInDto>>();
    }

    private IngeleverdeOpdrachtService CreateService()
    {
        return new IngeleverdeOpdrachtService(
            this.mockIngeleverdeOpdrachtRepository.Object,
            this.mockOpdrachtRepository.Object,
            this.mockFileManager.Object,
            this.mockOpdrachtBeoordelingRepository.Object,
            this.mockMapper.Object,
            this.mockStudentRepository.Object,
            this.mockValidatorOpdrachtBeoordelingDto.Object,
            this.mockValidatorLeverOpdrachtInDto.Object);
    }

    [Fact]
    public async Task HaalInzendingDataOp_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int inzendingId = 0;

        // Act
        var result = await service.HaalInzendingDataOp(
            inzendingId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task HaalInzendingenOp_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid opdrachtId = default(global::System.Guid);

        // Act
        var result = await service.HaalInzendingenOp(
            opdrachtId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task LeverOpdrachtIn_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int userId = 0;
        LeverOpdrachtInDto opdracht = null;

        // Act
        var result = await service.LeverOpdrachtIn(
            userId,
            opdracht);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task SlaBeoordelingOp_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        OpdrachtBeoordelingDto request = null;

        // Act
        var result = await service.SlaBeoordelingOp(
            request);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
}
