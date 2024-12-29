using AutoMapper;
using FluentValidation;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ICDE.UnitTests.Services;

public class BeoordelingCritereaServiceTests
{
    private MockRepository mockRepository;

    private Mock<IBeoordelingCritereaRepository> mockBeoordelingCritereaRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IValidator<MaakBeoordelingCritereaDto>> mockValidatorMaakBeoordelingCritereaDto;
    private Mock<IValidator<UpdateBeoordelingCritereaDto>> mockValidatorUpdateBeoordelingCritereaDto;

    public BeoordelingCritereaServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockBeoordelingCritereaRepository = this.mockRepository.Create<IBeoordelingCritereaRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockValidatorMaakBeoordelingCritereaDto = this.mockRepository.Create<IValidator<MaakBeoordelingCritereaDto>>();
        this.mockValidatorUpdateBeoordelingCritereaDto = this.mockRepository.Create<IValidator<UpdateBeoordelingCritereaDto>>();
    }

    private BeoordelingCritereaService CreateService()
    {
        return new BeoordelingCritereaService(
            this.mockBeoordelingCritereaRepository.Object,
            this.mockMapper.Object,
            this.mockValidatorMaakBeoordelingCritereaDto.Object,
            this.mockValidatorUpdateBeoordelingCritereaDto.Object);
    }

    [Fact]
    public async Task HaalOpMetEerdereVersies_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid critereaGroupId = default(global::System.Guid);

        // Act
        var result = await service.HaalOpMetEerdereVersies(
            critereaGroupId);

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
        UpdateBeoordelingCritereaDto request = null;

        // Act
        var result = await service.Update(
            request);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
}
