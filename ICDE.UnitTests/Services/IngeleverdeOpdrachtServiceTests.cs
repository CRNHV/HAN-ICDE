using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Dto.OpdrachtInzending;
using ICDE.Lib.IO;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Validation.Dto.OpdrachtInzending;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using System;
using System.Linq.Expressions;
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
    private IValidator<OpdrachtBeoordelingDto> mockValidatorOpdrachtBeoordelingDto = new OpdrachtBeoordelingValidation();
    private IValidator<LeverOpdrachtInDto> mockValidatorLeverOpdrachtInDto = new OpdrachtInzendingValidation();

    public IngeleverdeOpdrachtServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockIngeleverdeOpdrachtRepository = this.mockRepository.Create<IIngeleverdeOpdrachtRepository>();
        this.mockOpdrachtRepository = this.mockRepository.Create<IOpdrachtRepository>();
        this.mockFileManager = this.mockRepository.Create<IFileManager>();
        this.mockOpdrachtBeoordelingRepository = this.mockRepository.Create<IOpdrachtBeoordelingRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockStudentRepository = this.mockRepository.Create<IStudentRepository>();
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
            this.mockValidatorOpdrachtBeoordelingDto,
            this.mockValidatorLeverOpdrachtInDto);
    }

    [Fact]
    public async Task HaalInzendingDataOp_ShouldReturnNull_WhenInzendingIsNotFound()
    {
        // Arrange
        var service = this.CreateService();
        int inzendingId = 1;

        mockIngeleverdeOpdrachtRepository.Setup(x => x.VoorId(It.IsAny<int>()))
            .ReturnsAsync((int id) => null);

        // Act
        var result = await service.HaalInzendingDataOp(inzendingId);

        // Assert
        Assert.Null(result);
        mockIngeleverdeOpdrachtRepository.VerifyAll();
    }

    [Fact]
    public async Task HaalInzendingDataOp_ShouldReturnNull_WhenOpdrachtIsNotFound()
    {
        // Arrange
        var service = this.CreateService();
        int inzendingId = 1;

        mockIngeleverdeOpdrachtRepository.Setup(x => x.VoorId(It.IsAny<int>()))
            .ReturnsAsync(new IngeleverdeOpdracht());

        mockOpdrachtRepository.Setup(x => x.GetByInzendingId(It.IsAny<int>()))
            .ReturnsAsync((int id) => null);

        // Act
        var result = await service.HaalInzendingDataOp(inzendingId);

        // Assert
        Assert.Null(result);
        mockOpdrachtRepository.VerifyAll();
    }

    [Fact]
    public async Task HaalInzendingDataOp_ShouldReturnOpdrachtInzendingDto_WhenInzendingAndOpdrachtAreFound()
    {
        // Arrange
        var service = this.CreateService();
        int inzendingId = 1;

        var mockIngeleverdeOpdracht = new IngeleverdeOpdracht();
        var mockOpdracht = new Opdracht();
        var mockBeoordeling = new OpdrachtBeoordeling();
        var mockBeoordelingCriterea = new BeoordelingCriterea();

        mockIngeleverdeOpdrachtRepository.Setup(x => x.VoorId(It.IsAny<int>()))
            .ReturnsAsync(mockIngeleverdeOpdracht);

        mockOpdrachtRepository.Setup(x => x.GetByInzendingId(It.IsAny<int>()))
            .ReturnsAsync(mockOpdracht);

        mockMapper.Setup(x => x.Map<IngeleverdeOpdrachtDto>(It.IsAny<IngeleverdeOpdracht>()))
            .Returns(new IngeleverdeOpdrachtDto());

        mockMapper.Setup(x => x.Map<List<OpdrachtBeoordelingDto>>(It.IsAny<List<OpdrachtBeoordeling>>()))
            .Returns(new List<OpdrachtBeoordelingDto>() { new OpdrachtBeoordelingDto() });

        mockMapper.Setup(x => x.Map<List<BeoordelingCritereaDto>>(It.IsAny<List<BeoordelingCriterea>>()))
            .Returns(new List<BeoordelingCritereaDto>() { new BeoordelingCritereaDto() });

        // Act
        var result = await service.HaalInzendingDataOp(inzendingId);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Beoordelingen);
        Assert.Single(result.BeoordelingCritereas);
        mockIngeleverdeOpdrachtRepository.VerifyAll();
        mockOpdrachtRepository.VerifyAll();
        mockMapper.VerifyAll();
    }

    [Fact]
    public async Task HaalInzendingenOp_ShouldReturnEmptyList_WhenNoInzendingenFound()
    {
        // Arrange
        var service = this.CreateService();
        Guid opdrachtId = Guid.NewGuid();

        // Mock setup
        mockIngeleverdeOpdrachtRepository.Setup(x => x.Lijst(It.IsAny<Expression<Func<IngeleverdeOpdracht, bool>>>()))
            .Returns(Task.FromResult(new List<IngeleverdeOpdracht>()));

        // Act
        var result = await service.HaalInzendingenOp(opdrachtId);

        // Assert
        Assert.Empty(result);
        mockIngeleverdeOpdrachtRepository.VerifyAll();
    }

    [Fact]
    public async Task HaalInzendingenOp_ShouldReturnListOfInzendingenDto_WhenInzendingenFound()
    {
        // Arrange
        var service = this.CreateService();
        Guid opdrachtId = Guid.NewGuid();

        var mockInzendingen = new List<IngeleverdeOpdracht>()
        {
            new IngeleverdeOpdracht() { Id = 1, Naam = "Test Inzending 1"},
            new IngeleverdeOpdracht() { Id = 2, Naam = "Test Inzending 2"},
        };

        // Mock setup
        mockIngeleverdeOpdrachtRepository.Setup(x => x.Lijst(It.IsAny<Expression<Func<IngeleverdeOpdracht, bool>>>()))
            .Returns(Task.FromResult(mockInzendingen));

        // Act
        var result = await service.HaalInzendingenOp(opdrachtId);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(mockInzendingen[0].Id, result[0].Id);
        Assert.Equal(mockInzendingen[0].Naam, result[0].Naam);
        Assert.Equal(mockInzendingen[1].Id, result[1].Id);
        Assert.Equal(mockInzendingen[1].Naam, result[1].Naam);
        mockIngeleverdeOpdrachtRepository.VerifyAll();
    }

    [Fact]
    public async Task LeverOpdrachtIn_ValidInput_ReturnsTrue()
    {
        // Arrange
        var service = this.CreateService();
        int userId = 1;
        var mockOpdracht = new Opdracht();


        var bestand = new Mock<IFormFile>();
        bestand.Setup(x => x.Length).Returns(2);

        var mockLeverOpdrachtInDto = new LeverOpdrachtInDto()
        {
            Bestand = bestand.Object,
            Naam = "Naam",
            OpdrachtId = 1,
        };

        // Mock setups
        mockOpdrachtRepository.Setup(x => x.VoorId(It.IsAny<int>())).ReturnsAsync(mockOpdracht);
        mockStudentRepository.Setup(x => x.ZoekStudentNummerVoorUserId(It.IsAny<int>())).ReturnsAsync(1);
        mockFileManager.Setup(x => x.SlaOpdrachtOp(It.IsAny<string>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("fileLocation"));
        mockIngeleverdeOpdrachtRepository.Setup(x => x.Maak(It.IsAny<IngeleverdeOpdracht>())).ReturnsAsync(new IngeleverdeOpdracht());

        // Act
        var result = await service.LeverOpdrachtIn(userId, mockLeverOpdrachtInDto);

        // Assert
        Assert.True(result);

        mockOpdrachtRepository.Verify(x => x.VoorId(It.IsAny<int>()), Times.Once);
        mockStudentRepository.Verify(x => x.ZoekStudentNummerVoorUserId(It.IsAny<int>()), Times.Once);
        mockFileManager.Verify(x => x.SlaOpdrachtOp(It.IsAny<string>(), It.IsAny<IFormFile>()), Times.Once);
        mockIngeleverdeOpdrachtRepository.Verify(x => x.Maak(It.IsAny<IngeleverdeOpdracht>()), Times.Once);
    }

    [Fact]
    public async Task SlaBeoordelingOp_ValidInput_ReturnsTrue()
    {
        // Arrange
        var service = CreateService();
        var request = new OpdrachtBeoordelingDto { Cijfer = 10, Feedback = "Well done!" };

        mockIngeleverdeOpdrachtRepository.Setup(r => r.VoorId(It.IsAny<int>())).ReturnsAsync(new IngeleverdeOpdracht());

        mockOpdrachtBeoordelingRepository.Setup(x => x.Maak(It.IsAny<OpdrachtBeoordeling>())).ReturnsAsync(new OpdrachtBeoordeling());

        // Act
        var result = await service.SlaBeoordelingOp(request);

        // Assert
        Assert.True(result);
        mockOpdrachtBeoordelingRepository.Verify(r => r.Maak(It.IsAny<OpdrachtBeoordeling>()), Times.Once);
    }

    [Fact]
    public async Task SlaBeoordelingOp_InvalidInput_ReturnsFalse()
    {
        // Arrange
        var service = CreateService();
        var request = new OpdrachtBeoordelingDto { Cijfer = 11, Feedback = "Well done!" };

        // Act
        await Assert.ThrowsAsync<ValidationException>(() => service.SlaBeoordelingOp(request));

        // Assert
        mockOpdrachtBeoordelingRepository.Verify(r => r.Maak(It.IsAny<OpdrachtBeoordeling>()), Times.Never);
    }
}
