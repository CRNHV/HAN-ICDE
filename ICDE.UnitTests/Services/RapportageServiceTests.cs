using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.IO;
using ICDE.Lib.Reports;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Leeruitkomsten;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ICDE.UnitTests.Services;

public class RapportageServiceTests
{
    private MockRepository mockRepository;

    private Mock<IReportExporter> mockReportExporter;
    private Mock<IFileManager> mockFileManager;

    private IReportGenerator cursusReportGenerator;
    private IReportGenerator vakReportGenerator;
    private IReportGenerator opleidingReportGenerator;

    private Mock<ICursusRepository> mockCursusRepository;
    private Mock<IVakRepository> mockVakRepository;
    private Mock<IOpleidingRepository> mockOpleidingRepository;

    private Mock<IValidationManager> mockValidationManager;

    public RapportageServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);
        this.mockReportExporter = this.mockRepository.Create<IReportExporter>();
        this.mockFileManager = this.mockRepository.Create<IFileManager>();

        this.mockCursusRepository = this.mockRepository.Create<ICursusRepository>();
        this.mockOpleidingRepository = this.mockRepository.Create<IOpleidingRepository>();
        this.mockVakRepository = this.mockRepository.Create<IVakRepository>();

        cursusReportGenerator = new CursusReportGenerator(new ValidationManager(), mockCursusRepository.Object);
        vakReportGenerator = new VakReportGenerator(new ValidationManager(), mockVakRepository.Object);
        opleidingReportGenerator = new OpleidingReportGenerator(new ValidationManager(), mockOpleidingRepository.Object);
    }

    private RapportageService CreateService()
    {
        return new RapportageService(
            cursusReportGenerator,
            vakReportGenerator,
            opleidingReportGenerator,
            this.mockReportExporter.Object,
            this.mockFileManager.Object);
    }

    [Fact]
    public async Task GenereerRapportage_VoorCursus_ShouldSucceed()
    {
        // Arrange
        var service = this.CreateService();
        string type = "cursus";
        Guid groupId = default(global::System.Guid);

        var dbCursus = new Cursus();

        mockCursusRepository
            .Setup(x => x.GetFullObjectTreeByGroupId(It.IsAny<Guid>()))
            .ReturnsAsync(dbCursus);

        // Act
        var result = await service.GenereerRapportage(
            type,
            groupId);

        // Assert
        Assert.True(result.All(x => x.Success));
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task GenereerRapportage_VoorOpleiding_ShouldSucceed()
    {
        // Arrange
        var service = this.CreateService();
        string type = "opleiding";
        Guid groupId = default(global::System.Guid);

        var dbOpleiding = new Opleiding();

        mockOpleidingRepository
            .Setup(x => x.GetFullObjectTreeByGroupId(It.IsAny<Guid>()))
            .ReturnsAsync(dbOpleiding);

        // Act
        var result = await service.GenereerRapportage(
            type,
            groupId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task GenereerRapportage_VoorVak_ShouldSucceed()
    {
        // Arrange
        var service = this.CreateService();
        string type = "vak";
        Guid groupId = default(global::System.Guid);

        var dbVak = new Vak();

        mockVakRepository
            .Setup(x => x.GetFullObjectTreeByGroupId(It.IsAny<Guid>()))
            .ReturnsAsync(dbVak);

        // Act
        var result = await service.GenereerRapportage(
            type,
            groupId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    //[Fact]
    //public async Task ExporteerRapportage_StateUnderTest_ExpectedBehavior()
    //{
    //    // Arrange
    //    var service = this.CreateService();
    //    string type = null;
    //    Guid groupId = default(global::System.Guid);

    //    // Act
    //    var result = await service.ExporteerRapportage(
    //        type,
    //        groupId);

    //    // Assert
    //    Assert.True(false);
    //    this.mockRepository.VerifyAll();
    //}
}
