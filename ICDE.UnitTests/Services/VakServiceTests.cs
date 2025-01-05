using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Data.Repositories.Luk;
using ICDE.Lib.Dto.Vak;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Dto.Vak;
using Moq;

namespace ICDE.UnitTests.Services;

public class VakServiceTests
{
    private MockRepository mockRepository;

    private Mock<IVakRepository> mockVakRepository;
    private Mock<ICursusRepository> mockCursusRepository;
    private Mock<ILeeruitkomstRepository> mockLeeruitkomstRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IValidator<MaakVakDto>> mockValidatorMaakVakDto;
    private IValidator<UpdateVakDto> mockValidatorUpdateVakDto;

    public VakServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockVakRepository = this.mockRepository.Create<IVakRepository>();
        this.mockCursusRepository = this.mockRepository.Create<ICursusRepository>();
        this.mockLeeruitkomstRepository = this.mockRepository.Create<ILeeruitkomstRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockValidatorMaakVakDto = this.mockRepository.Create<IValidator<MaakVakDto>>();
        this.mockValidatorUpdateVakDto = new UpdateVakValidation();
    }

    private VakService CreateService()
    {
        return new VakService(
            this.mockVakRepository.Object,
            this.mockCursusRepository.Object,
            this.mockLeeruitkomstRepository.Object,
            this.mockMapper.Object,
            this.mockValidatorMaakVakDto.Object,
            this.mockValidatorUpdateVakDto);
    }

    [Fact]
    public async Task HaalVolledigeVakDataOp_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid vakGroupId = default(global::System.Guid);

        var dbVak = new Vak();

        mockVakRepository
            .Setup(x => x.NieuwsteVoorGroepId(It.IsAny<Guid>()))
            .ReturnsAsync(dbVak);

        mockMapper
            .Setup(x => x.Map<VakMetOnderwijsOnderdelenDto>(It.IsAny<object>()))
            .Returns(new VakMetOnderwijsOnderdelenDto());

        mockVakRepository
           .Setup(x => x.Lijst(It.IsAny<Expression<Func<Vak, bool>>>()))
           .ReturnsAsync(new List<Vak>()
           {
                dbVak
           }
        );

        mockMapper
           .Setup(x => x.Map<List<VakDto>>(It.IsAny<object>()))
           .Returns(new List<VakDto>());

        // Act
        var result = await service.HaalVolledigeVakDataOp(
            vakGroupId);

        // Assert
        Assert.NotNull(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task KoppelCursus_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid vakGroupId = default(global::System.Guid);
        Guid cursusGroupId = default(global::System.Guid);

        var dbVak = new Vak();
        var dbCursus = new Cursus();

        mockVakRepository
           .Setup(x => x.NieuwsteVoorGroepId(It.IsAny<Guid>()))
           .ReturnsAsync(dbVak);

        mockCursusRepository
            .Setup(x => x.NieuwsteVoorGroepId(It.IsAny<Guid>()))
            .ReturnsAsync(dbCursus);

        mockVakRepository
            .Setup(x => x.Update(It.IsAny<Vak>()))
            .ReturnsAsync(true);

        // Act
        var result = await service.KoppelCursus(
            vakGroupId,
            cursusGroupId);

        // Assert
        Assert.True(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task KoppelLeeruitkomst_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid vakGroupId = default(global::System.Guid);
        Guid lukGroupId = default(global::System.Guid);

        var dbVak = new Vak();
        var dbLeeruitkomst = new Leeruitkomst();

        mockVakRepository
           .Setup(x => x.NieuwsteVoorGroepId(It.IsAny<Guid>()))
           .ReturnsAsync(dbVak);

        mockLeeruitkomstRepository
            .Setup(x => x.NieuwsteVoorGroepId(It.IsAny<Guid>()))
            .ReturnsAsync(dbLeeruitkomst);

        mockVakRepository
            .Setup(x => x.Update(It.IsAny<Vak>()))
            .ReturnsAsync(true);

        // Act
        var result = await service.KoppelLeeruitkomst(
            vakGroupId,
            lukGroupId);

        // Assert
        Assert.True(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task BekijkVersie_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid vakGroupId = default(global::System.Guid);
        int vakVersie = 0;

        var dbVak = new Vak()
        {
            GroupId = vakGroupId,
        };

        mockMapper
            .Setup(x => x.Map<VakDto>(It.IsAny<object>()))
            .Returns(new VakDto());

        mockVakRepository
            .Setup(x => x.Versie(It.IsAny<Guid>(), It.IsAny<int>()))
            .ReturnsAsync(dbVak);

        // Act
        var result = await service.BekijkVersie(
            vakGroupId,
            vakVersie);

        // Assert
        Assert.NotNull(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task Update_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        UpdateVakDto request = new()
        {
            Naam = "UpdatedName",
            Beschrijving = "UpdatedBeschrijving",
        };
        Vak dbVak = new Vak();

        mockVakRepository
            .Setup(x => x.NieuwsteVoorGroepId(It.IsAny<Guid>()))
            .ReturnsAsync(dbVak);

        mockVakRepository
            .Setup(x => x.Update(It.Is<Vak>(x => x == dbVak)))
            .ReturnsAsync(true);

        // Act
        var result = await service.Update(
            request);

        // Assert
        Assert.True(result);
        this.mockRepository.VerifyAll();
    }
}
