using AutoMapper;
using ICDE.Data.Repositories.Interfaces;
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
    public async Task HaalBeoordelingenOpVoorUser_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        int? userId = null;

        // Act
        var result = await service.HaalBeoordelingenOpVoorUser(
            userId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
}
