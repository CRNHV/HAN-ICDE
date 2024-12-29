using AutoMapper;
using FluentValidation;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ICDE.UnitTests.Services;

public class OpleidingServiceTests
{
    private MockRepository mockRepository;

    private Mock<IOpleidingRepository> mockOpleidingRepository;
    private Mock<IVakRepository> mockVakRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IValidator<MaakOpleidingDto>> mockValidatorMaakOpleidingDto;
    private Mock<IValidator<UpdateOpleidingDto>> mockValidatorUpdateOpleidingDto;

    public OpleidingServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockOpleidingRepository = this.mockRepository.Create<IOpleidingRepository>();
        this.mockVakRepository = this.mockRepository.Create<IVakRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockValidatorMaakOpleidingDto = this.mockRepository.Create<IValidator<MaakOpleidingDto>>();
        this.mockValidatorUpdateOpleidingDto = this.mockRepository.Create<IValidator<UpdateOpleidingDto>>();
    }

    private OpleidingService CreateService()
    {
        return new OpleidingService(
            this.mockOpleidingRepository.Object,
            this.mockVakRepository.Object,
            this.mockMapper.Object,
            this.mockValidatorMaakOpleidingDto.Object,
            this.mockValidatorUpdateOpleidingDto.Object);
    }

    [Fact]
    public async Task KoppelVakAanOpleiding_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid opleidingGroupId = default(global::System.Guid);
        Guid vakGroupId = default(global::System.Guid);

        // Act
        var result = await service.KoppelVakAanOpleiding(
            opleidingGroupId,
            vakGroupId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task ZoekOpleidingMetEerdereVersies_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid groupId = default(global::System.Guid);

        // Act
        var result = await service.ZoekOpleidingMetEerdereVersies(
            groupId);

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
        UpdateOpleidingDto request = null;

        // Act
        var result = await service.Update(
            request);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
}
