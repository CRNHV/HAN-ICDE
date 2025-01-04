using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Dto.Opleiding;
using Moq;
using System;
using System.Linq.Expressions;
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
    private IValidator<UpdateOpleidingDto> validatorUpdateOpleiding = new UpdateOpleidingValidation();

    public OpleidingServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockOpleidingRepository = this.mockRepository.Create<IOpleidingRepository>();
        this.mockVakRepository = this.mockRepository.Create<IVakRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockValidatorMaakOpleidingDto = this.mockRepository.Create<IValidator<MaakOpleidingDto>>();
    }

    private OpleidingService CreateService()
    {
        return new OpleidingService(
            this.mockOpleidingRepository.Object,
            this.mockVakRepository.Object,
            this.mockMapper.Object,
            this.mockValidatorMaakOpleidingDto.Object,
            validatorUpdateOpleiding);
    }

    [Fact]
    public async Task KoppelVakAanOpleiding_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid opleidingGroupId = Guid.NewGuid();
        Guid vakGroupId = Guid.NewGuid();

        var dbOpleiding = new Opleiding()
        {
            GroupId = opleidingGroupId,
        };

        var dbVak = new Vak()
        {
            GroupId = vakGroupId,
        };

        mockOpleidingRepository
            .Setup(x => x.NieuwsteVoorGroepId(It.Is<Guid>(x => x == dbOpleiding.GroupId)))
            .ReturnsAsync(dbOpleiding);

        mockVakRepository
            .Setup(x => x.NieuwsteVoorGroepId(It.Is<Guid>(x => x == dbVak.GroupId)))
            .ReturnsAsync(dbVak);

        mockOpleidingRepository
            .Setup(x => x.Update(It.Is<Opleiding>(x => x.Vakken.Contains(dbVak))))
            .ReturnsAsync(true);

        // Act
        var result = await service.KoppelVakAanOpleiding(
            opleidingGroupId,
            vakGroupId);

        // Assert
        Assert.True(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task ZoekOpleidingMetEerdereVersies_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid opleidingGroupId = Guid.NewGuid();
        var dbOpleiding = new Opleiding()
        {
            GroupId = opleidingGroupId,
        };
        mockOpleidingRepository
            .Setup(x => x.NieuwsteVoorGroepId(It.Is<Guid>(x => x == dbOpleiding.GroupId)))
            .ReturnsAsync(dbOpleiding);
        mockOpleidingRepository
            .Setup(x => x.Lijst(It.IsAny<Expression<Func<Opleiding, bool>>>()))
            .ReturnsAsync(new List<Opleiding>()
            {
                new Opleiding(),
            });
        mockMapper.Setup(x => x.Map<OpleidingMetVakkenDto>(It.IsAny<Opleiding>()))
            .Returns(new OpleidingMetVakkenDto());
        mockMapper.Setup(x => x.Map<List<OpleidingDto>>(It.IsAny<List<Opleiding>>()))
            .Returns(new List<OpleidingDto>());

        // Act
        var result = await service.ZoekOpleidingMetEerdereVersies(opleidingGroupId);

        // Assert
        Assert.NotNull(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task MaakKopie_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int versieNummer = 0;

        Guid opleidingGroupId = Guid.NewGuid();
        var dbOpleiding = new Opleiding()
        {
            GroupId = opleidingGroupId,
        };
        mockOpleidingRepository
            .Setup(x => x.NieuwsteVoorGroepId(It.Is<Guid>(x => x == dbOpleiding.GroupId)))
            .ReturnsAsync(dbOpleiding);

        mockOpleidingRepository
            .Setup(x => x.Maak(It.IsAny<Opleiding>()))
            .ReturnsAsync(new Opleiding()
            {
                GroupId = Guid.NewGuid(),
            });

        // Act
        var result = await service.MaakKopie(
            opleidingGroupId,
            versieNummer);

        // Assert
        Assert.True(result != Guid.NewGuid());
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task Update_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        Guid opleidingGroupId = Guid.NewGuid();
        UpdateOpleidingDto request = new UpdateOpleidingDto()
        {
            Naam = "Naam",
            GroupId = opleidingGroupId,
            Beschrijving = "Beschrijving"
        };

        var dbOpleiding = new Opleiding()
        {
            GroupId = opleidingGroupId,
        };
        mockOpleidingRepository
            .Setup(x => x.NieuwsteVoorGroepId(It.Is<Guid>(x => x == dbOpleiding.GroupId)))
            .ReturnsAsync(dbOpleiding);

        mockOpleidingRepository
            .Setup(x => x.Update(It.Is<Opleiding>(x => x == dbOpleiding)))
            .ReturnsAsync(true);

        // Act
        var result = await service.Update(request);

        // Assert
        Assert.True(result);
        this.mockRepository.VerifyAll();
    }
}
