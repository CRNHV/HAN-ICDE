using AutoMapper;
using FluentValidation;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ICDE.UnitTests.Services;

public class OpdrachtServiceTests
{
    private MockRepository mockRepository;

    private Mock<IOpdrachtRepository> mockOpdrachtRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IBeoordelingCritereaRepository> mockBeoordelingCritereaRepository;
    private Mock<IValidator<MaakOpdrachtDto>> mockValidatorMaakOpdrachtDto;
    private Mock<IValidator<UpdateOpdrachtDto>> mockValidatorUpdateOpdrachtDto;

    public OpdrachtServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockOpdrachtRepository = this.mockRepository.Create<IOpdrachtRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockBeoordelingCritereaRepository = this.mockRepository.Create<IBeoordelingCritereaRepository>();
        this.mockValidatorMaakOpdrachtDto = this.mockRepository.Create<IValidator<MaakOpdrachtDto>>();
        this.mockValidatorUpdateOpdrachtDto = this.mockRepository.Create<IValidator<UpdateOpdrachtDto>>();
    }

    private OpdrachtService CreateService()
    {
        return new OpdrachtService(
            this.mockOpdrachtRepository.Object,
            this.mockMapper.Object,
            this.mockBeoordelingCritereaRepository.Object,
            this.mockValidatorMaakOpdrachtDto.Object,
            this.mockValidatorUpdateOpdrachtDto.Object);
    }

    [Fact]
    public async Task HaalAlleDataOp_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid opdrachtGroupId = default(global::System.Guid);

        // Act
        var result = await service.HaalAlleDataOp(
            opdrachtGroupId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task VoegCritereaToe_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid opdrachtGroupId = default(global::System.Guid);
        Guid critereaGroupId = default(global::System.Guid);

        // Act
        var result = await service.VoegCritereaToe(
            opdrachtGroupId,
            critereaGroupId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task HaalStudentOpdrachtDataOp_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid opdrachtGroupId = default(global::System.Guid);

        // Act
        var result = await service.HaalStudentOpdrachtDataOp(
            opdrachtGroupId);

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
        UpdateOpdrachtDto request = null;

        // Act
        var result = await service.Update(
            request);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
}
