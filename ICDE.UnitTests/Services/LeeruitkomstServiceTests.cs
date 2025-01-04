using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Luk;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services;
using Moq;
using System;
using System.Linq.Expressions;
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
    public async Task HaalOpMetEerdereVersies_Success_ReturnsLeeruitkomstMetEerdereVersiesDto()
    {
        // Arrange        
        var service = this.CreateService();
        var leeruitkomstId = Guid.NewGuid();

        var mockLeeruitkomst = new Leeruitkomst();
        mockLeeruitkomstRepository.Setup(x => x.NieuwsteVoorGroepId(leeruitkomstId))
            .Returns(Task.FromResult(mockLeeruitkomst));

        var mockEerdereVersies = new List<Leeruitkomst>();
        mockLeeruitkomstRepository.Setup(x => x.Lijst(It.IsAny<Expression<Func<Leeruitkomst, bool>>>()))
            .Returns(Task.FromResult(mockEerdereVersies));

        var leeruitkomstDto = new LeeruitkomstDto();
        mockMapper.Setup(x => x.Map<LeeruitkomstDto>(It.IsAny<Leeruitkomst>()))
            .Returns(leeruitkomstDto);

        // Act
        var result = await service.HaalOpMetEerdereVersies(leeruitkomstId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(leeruitkomstDto, result.Leeruitkomst);
        mockLeeruitkomstRepository.VerifyAll();
        mockMapper.VerifyAll();
    }

    //[Fact]
    //public async Task MaakKopie_StateUnderTest_ExpectedBehavior()
    //{
    //    // Arrange
    //    var service = this.CreateService();
    //    Guid groupId = default(global::System.Guid);
    //    int versieNummer = 0;

    //    // Act
    //    var result = await service.MaakKopie(
    //        groupId,
    //        versieNummer);

    //    // Assert
    //    Assert.True(false);
    //    this.mockRepository.VerifyAll();
    //}

    //[Fact]
    //public async Task Update_StateUnderTest_ExpectedBehavior()
    //{
    //    // Arrange
    //    var service = this.CreateService();
    //    UpdateLeeruitkomstDto request = null;

    //    // Act
    //    var result = await service.Update(
    //        request);

    //    // Assert
    //    Assert.True(false);
    //    this.mockRepository.VerifyAll();
    //}

    [Fact]
    public async Task MaakKopieVanVersie_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        Guid groupId = Guid.NewGuid();
        int versieId = 1;
        var originalLeeruitkomst = new Leeruitkomst { GroupId = groupId, VersieNummer = versieId };
        var clonedLeeruitkomst = (Leeruitkomst)originalLeeruitkomst.Clone();
        clonedLeeruitkomst.GroupId = Guid.NewGuid();

        this.mockLeeruitkomstRepository
            .Setup(x => x.Lijst(It.IsAny<Expression<Func<Leeruitkomst, bool>>>()))
            .ReturnsAsync(new List<Leeruitkomst> { originalLeeruitkomst });

        this.mockLeeruitkomstRepository
            .Setup(x => x.Maak(It.IsAny<Leeruitkomst>()))
            .ReturnsAsync(new Leeruitkomst());

        var service = this.CreateService();

        // Act
        var result = await service.MaakKopieVanVersie(groupId, versieId);

        // Assert
        Assert.NotEqual(clonedLeeruitkomst.GroupId, result);
        this.mockLeeruitkomstRepository.VerifyAll();
    }
}
