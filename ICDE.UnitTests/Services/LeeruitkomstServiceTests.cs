using AutoMapper;
using FluentValidation;
using ICDE.Data.Repositories.Luk;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ICDE.UnitTests.Services;

public class LeeruitkomstServiceTests
{
    private MockRepository mockRepository;

    private Mock<ILeeruitkomstRepository> mockLeeruitkomstRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IValidator<UpdateLeeruitkomstDto>> mockValidatorUpdateLeeruitkomstDto;
    private Mock<IValidator<MaakLeeruitkomstDto>> mockValidatorMaakLeeruitkomstDto;

    public LeeruitkomstServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockLeeruitkomstRepository = this.mockRepository.Create<ILeeruitkomstRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockValidatorUpdateLeeruitkomstDto = this.mockRepository.Create<IValidator<UpdateLeeruitkomstDto>>();
        this.mockValidatorMaakLeeruitkomstDto = this.mockRepository.Create<IValidator<MaakLeeruitkomstDto>>();
    }

    private LeeruitkomstService CreateService()
    {
        return new LeeruitkomstService(
            this.mockLeeruitkomstRepository.Object,
            this.mockMapper.Object,
            this.mockValidatorUpdateLeeruitkomstDto.Object,
            this.mockValidatorMaakLeeruitkomstDto.Object);
    }

    [Fact]
    public async Task HaalOpMetEerdereVersies_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid leeruitkomstId = default(global::System.Guid);

        // Act
        var result = await service.HaalOpMetEerdereVersies(
            leeruitkomstId);

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
        UpdateLeeruitkomstDto request = null;

        // Act
        var result = await service.Update(
            request);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task MaakKopieVanVersie_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid groupId = default(global::System.Guid);
        int versieId = 0;

        // Act
        var result = await service.MaakKopieVanVersie(
            groupId,
            versieId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
}
