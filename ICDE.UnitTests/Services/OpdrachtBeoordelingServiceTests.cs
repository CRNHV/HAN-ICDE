﻿using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Dto.OpdrachtBeoordeling;
using Moq;

namespace ICDE.UnitTests.Services;

public class OpdrachtBeoordelingServiceTests
{
    private MockRepository mockRepository;

    private Mock<IOpdrachtBeoordelingRepository> mockOpdrachtBeoordelingRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IIngeleverdeOpdrachtRepository> mockIngeleverdeOpdrachtRepository;
    private IValidator<OpdrachtBeoordelingDto> opdrachtBeoordelingValidation = new OpdrachtBeoordelingValidation();

    public OpdrachtBeoordelingServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockOpdrachtBeoordelingRepository = this.mockRepository.Create<IOpdrachtBeoordelingRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockIngeleverdeOpdrachtRepository = this.mockRepository.Create<IIngeleverdeOpdrachtRepository>();
    }

    private OpdrachtBeoordelingService CreateService()
    {
        return new OpdrachtBeoordelingService(
            this.mockOpdrachtBeoordelingRepository.Object,
            this.mockMapper.Object,
            opdrachtBeoordelingValidation,
            mockIngeleverdeOpdrachtRepository.Object
            );
    }

    [Fact]
    public async Task HaalBeoordelingenOpVoorUser_UserIdProvided_ReturnsMappedBeoordelingen()
    {
        // Arrange
        var userId = 1;
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

        mockOpdrachtBeoordelingRepository
            .Setup(repo => repo.HaalBeoordelingenOpVoorStudent(userId))
            .ReturnsAsync(mockBeoordelingen);

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
