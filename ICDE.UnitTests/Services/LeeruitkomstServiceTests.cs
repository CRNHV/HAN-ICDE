using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Dto.Leeruitkomst;
using Moq;

namespace ICDE.UnitTests.Services;

public class LeeruitkomstServiceTests
{
    private MockRepository mockRepository;

    private Mock<ILeeruitkomstRepository> mockLeeruitkomstRepository;
    private Mock<IMapper> mockMapper;
    private IValidator<UpdateLeeruitkomstDto> mockValidatorUpdateLeeruitkomstDto;
    private Mock<IValidator<MaakLeeruitkomstDto>> mockValidatorMaakLeeruitkomstDto;

    public LeeruitkomstServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockLeeruitkomstRepository = this.mockRepository.Create<ILeeruitkomstRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockValidatorUpdateLeeruitkomstDto = new UpdateLeeruitkomstValidation();
        this.mockValidatorMaakLeeruitkomstDto = this.mockRepository.Create<IValidator<MaakLeeruitkomstDto>>();
    }

    private LeeruitkomstService CreateService()
    {
        return new LeeruitkomstService(
            this.mockLeeruitkomstRepository.Object,
            this.mockMapper.Object,
            this.mockValidatorUpdateLeeruitkomstDto,
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
            .ReturnsAsync(mockLeeruitkomst);

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

    [Fact]
    public async Task Update_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var service = this.CreateService();

        var validRequest = new UpdateLeeruitkomstDto
        {
            GroupId = Guid.NewGuid(),
            Naam = "Updated Name",
            Beschrijving = "Updated Description"
        };

        var dbLuk = new Leeruitkomst
        {
            Id = 1,
            Naam = "Old Name",
            Beschrijving = "Old Description",
            GroupId = validRequest.GroupId
        };

        this.mockLeeruitkomstRepository
            .Setup(repo => repo.NieuwsteVoorGroepId(validRequest.GroupId))
            .ReturnsAsync(dbLuk)
            .Verifiable();

        this.mockLeeruitkomstRepository
            .Setup(repo => repo.Update(dbLuk))
            .ReturnsAsync(true)
            .Verifiable();

        // Act
        var result = await service.Update(validRequest);

        // Assert
        Assert.True(result);

        this.mockLeeruitkomstRepository.Verify();
        this.mockLeeruitkomstRepository.Verify(repo => repo.Update(It.Is<Leeruitkomst>(
            luk => luk.Naam == validRequest.Naam &&
                   luk.Beschrijving == validRequest.Beschrijving &&
                   luk.GroupId == validRequest.GroupId
        )), Times.Once);
    }
}
