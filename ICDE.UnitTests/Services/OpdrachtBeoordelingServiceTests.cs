using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ICDE.UnitTests.Services;

public class OpdrachtBeoordelingServiceTests
{
    private MockRepository mockRepository;

    private Mock<IOpdrachtBeoordelingRepository> mockOpdrachtBeoordelingRepository;
    private Mock<IMapper> mockMapper;

    public OpdrachtBeoordelingServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockOpdrachtBeoordelingRepository = this.mockRepository.Create<IOpdrachtBeoordelingRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
    }

    private OpdrachtBeoordelingService CreateService()
    {
        return new OpdrachtBeoordelingService(
            this.mockOpdrachtBeoordelingRepository.Object,
            this.mockMapper.Object);
    }

    [Fact]
    public async Task HaalBeoordelingenOpVoorUser_UserIdProvided_ReturnsMappedBeoordelingen()
    {
        // Arrange
        var userId = 1; // Example user ID
        var mockBeoordelingen = new List<OpdrachtBeoordeling>
        {
            new OpdrachtBeoordeling { Cijfer = 8 },
            new OpdrachtBeoordeling { Cijfer = 9 }
        };
        var mappedResult = new List<OpdrachtMetBeoordelingDto>
        {
            new OpdrachtMetBeoordelingDto { Cijfer = 8 },
            new OpdrachtMetBeoordelingDto { Cijfer = 9 }
        };

        // Mock the repository to return mock data
        mockOpdrachtBeoordelingRepository
            .Setup(repo => repo.HaalBeoordelingenOpVoorStudent(userId))
            .ReturnsAsync(mockBeoordelingen);

        // Mock the mapper to map the data
        mockMapper
            .Setup(mapper => mapper.Map<List<OpdrachtMetBeoordelingDto>>(mockBeoordelingen))
            .Returns(mappedResult);

        var service = this.CreateService();

        // Act
        var result = await service.HaalBeoordelingenOpVoorUser(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mappedResult.Count, result.Count);
        Assert.Equal(mappedResult, result);

        mockOpdrachtBeoordelingRepository.Verify(repo => repo.HaalBeoordelingenOpVoorStudent(userId), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<List<OpdrachtMetBeoordelingDto>>(mockBeoordelingen), Times.Once);
    }

    [Fact]
    public async Task HaalBeoordelingenOpVoorUser_UserIdNull_ThrowsArgumentNullException()
    {
        // Arrange
        int? userId = null;
        var service = this.CreateService();

        // Act & Assert
        var result = await service.HaalBeoordelingenOpVoorUser(userId);

        Assert.Empty(result);
        mockOpdrachtBeoordelingRepository.Verify(repo => repo.HaalBeoordelingenOpVoorStudent(It.IsAny<int>()), Times.Never);
        mockMapper.Verify(mapper => mapper.Map<List<OpdrachtMetBeoordelingDto>>(It.IsAny<List<OpdrachtBeoordeling>>()), Times.Never);
    }
}
