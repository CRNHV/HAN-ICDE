using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.IO;
using ICDE.Lib.Reports;
using ICDE.Lib.Services;
using ICDE.Lib.Validation.Leeruitkomsten;
using Moq;

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

        var leeruitkomst1 = new Leeruitkomst()
        {
            Id = 1,
            Naam = "Luk1",
        };

        var leeruitkomst2 = new Leeruitkomst()
        {
            Id = 2,
            Naam = "Luk2",
        };


        var dbCursus = CreateCursus(leeruitkomst1, leeruitkomst2);

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

        Leeruitkomst leeruitkomst1 = new Leeruitkomst(), leeruitkomst2 = new Leeruitkomst(), leeruitkomst3 = new Leeruitkomst();
        Leeruitkomst leeruitkomst4 = new Leeruitkomst(), leeruitkomst5 = new Leeruitkomst(), leeruitkomst6 = new Leeruitkomst();

        var dbOpleiding = new Opleiding()
        {
            Vakken = new List<Vak>()
            {
                new Vak()
                {
                    Leeruitkomsten = new List<Leeruitkomst>()
                    {
                        leeruitkomst1, leeruitkomst2, leeruitkomst3,
                        leeruitkomst4, leeruitkomst5, leeruitkomst6
                    },
                    Cursussen = new List<Cursus>()
                    {
                        CreateCursus(leeruitkomst1, leeruitkomst2, leeruitkomst3),
                        CreateCursus(leeruitkomst4, leeruitkomst5, leeruitkomst6)
                    }
                }
            }
        };

        mockOpleidingRepository
            .Setup(x => x.GetFullObjectTreeByGroupId(It.IsAny<Guid>()))
            .ReturnsAsync(dbOpleiding);

        // Act
        var result = await service.GenereerRapportage(
            type,
            groupId);

        // Assert
        Assert.True(result.All(x => x.Success));
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task GenereerRapportage_VoorVak_ShouldSucceed()
    {
        // Arrange
        var service = this.CreateService();
        string type = "vak";
        Guid groupId = default(global::System.Guid);

        Leeruitkomst leeruitkomst1 = new Leeruitkomst(), leeruitkomst2 = new Leeruitkomst(), leeruitkomst3 = new Leeruitkomst();
        Leeruitkomst leeruitkomst4 = new Leeruitkomst(), leeruitkomst5 = new Leeruitkomst(), leeruitkomst6 = new Leeruitkomst();

        var dbVak = new Vak()
        {
            Leeruitkomsten = new List<Leeruitkomst>()
                    {
                        leeruitkomst1, leeruitkomst2, leeruitkomst3,
                        leeruitkomst4, leeruitkomst5, leeruitkomst6
                    },
            Cursussen = new List<Cursus>()
                    {
                        CreateCursus(leeruitkomst1, leeruitkomst2, leeruitkomst3),
                        CreateCursus(leeruitkomst4, leeruitkomst5, leeruitkomst6)
                    }
        };

        mockVakRepository
            .Setup(x => x.GetFullObjectTreeByGroupId(It.IsAny<Guid>()))
            .ReturnsAsync(dbVak);

        // Act
        var result = await service.GenereerRapportage(
            type,
            groupId);

        // Assert
        Assert.True(result.All(x => x.Success));
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task ExporteerRapportage_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        string type = null;
        Guid groupId = default(global::System.Guid);

        // Act
        var result = await service.ExporteerRapportage(
            type,
            groupId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    private Cursus CreateCursus(params Leeruitkomst[] leeruitkomsten)
    {
        return new Cursus()
        {
            Leeruitkomsten = leeruitkomsten.ToList(),
            Planning = new Planning()
            {
                PlanningItems = new List<PlanningItem>()
                {
                    new PlanningItem()
                    {
                        Les = new Les()
                        {
                            Id = 1,
                             Leeruitkomsten = leeruitkomsten.ToList(),
                        }
                    },
                    new PlanningItem()
                    {
                        Opdracht = new Opdracht()
                        {
                            Id = 1,
                            BeoordelingCritereas = new List<BeoordelingCriterea>()
                            {
                               new BeoordelingCriterea()
                               {
                                    Leeruitkomsten = leeruitkomsten.ToList(),
                               }
                            }
                        }
                    }
                }
            }
        };
    }
}
