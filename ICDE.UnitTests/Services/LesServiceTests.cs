using AutoMapper;
using FluentValidation;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Data.Repositories.Luk;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ICDE.UnitTests.Services;

public class LesServiceTests
{
    private MockRepository mockRepository;

    private Mock<ILesRepository> mockLesRepository;
    private Mock<ILeeruitkomstRepository> mockLeeruitkomstRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IValidator<MaakLesDto>> mockValidatorMaakLesDto;
    private Mock<IValidator<UpdateLesDto>> mockValidatorUpdateLesDto;

    public LesServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockLesRepository = this.mockRepository.Create<ILesRepository>();
        this.mockLeeruitkomstRepository = this.mockRepository.Create<ILeeruitkomstRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockValidatorMaakLesDto = this.mockRepository.Create<IValidator<MaakLesDto>>();
        this.mockValidatorUpdateLesDto = this.mockRepository.Create<IValidator<UpdateLesDto>>();
    }

    private LesService CreateService()
    {
        return new LesService(
            this.mockLesRepository.Object,
            this.mockLeeruitkomstRepository.Object,
            this.mockMapper.Object,
            this.mockValidatorMaakLesDto.Object,
            this.mockValidatorUpdateLesDto.Object);
    }

    [Fact]
    public async Task HaalLessenOpMetEerdereVersies_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid groupId = default(global::System.Guid);

        // Act
        var result = await service.HaalLessenOpMetEerdereVersies(
            groupId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task KoppelLukAanLes_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid lesGroupId = default(global::System.Guid);
        Guid lukGroupId = default(global::System.Guid);

        // Act
        var result = await service.KoppelLukAanLes(
            lesGroupId,
            lukGroupId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task OntkoppelLukAanLes_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid lesGroupId = default(global::System.Guid);
        Guid lukGroupId = default(global::System.Guid);

        // Act
        var result = await service.OntkoppelLukAanLes(
            lesGroupId,
            lukGroupId);

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
        UpdateLesDto request = null;

        // Act
        var result = await service.Update(
            request);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
}
