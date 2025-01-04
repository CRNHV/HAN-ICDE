using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Data.Repositories.Luk;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Services;
using ICDE.Lib.Services.Interfaces;
using ICDE.Lib.Validation.Dto.Lessen;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace ICDE.UnitTests.Services;

public class LesServiceTests
{
    private MockRepository mockRepository;

    private Mock<ILesRepository> mockLesRepository;
    private Mock<ILeeruitkomstRepository> mockLeeruitkomstRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IValidator<MaakLesDto>> mockValidatorMaakLesDto;
    private IValidator<UpdateLesDto> mockValidatorUpdateLesDto = new UpdateLesValidation();

    public LesServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockLesRepository = this.mockRepository.Create<ILesRepository>();
        this.mockLeeruitkomstRepository = this.mockRepository.Create<ILeeruitkomstRepository>();
        this.mockMapper = this.mockRepository.Create<IMapper>();
        this.mockValidatorMaakLesDto = this.mockRepository.Create<IValidator<MaakLesDto>>();
    }

    private LesService CreateService()
    {
        return new LesService(
            this.mockLesRepository.Object,
            this.mockLeeruitkomstRepository.Object,
            this.mockMapper.Object,
            this.mockValidatorMaakLesDto.Object,
            this.mockValidatorUpdateLesDto);
    }

    [Fact]
    public async Task HaalLessenOpMetEerdereVersies_Success()
    {
        // Arrange
        var service = this.CreateService();
        var groupId = Guid.NewGuid();
        var currentVersion = new Les();
        var otherVersions = new List<Les>();

        mockLesRepository.Setup(x => x.NieuwsteVoorGroepId(groupId))
            .Returns(Task.FromResult(currentVersion));
        mockLesRepository.Setup(x => x.Lijst(It.IsAny<Expression<Func<Les, bool>>>()))
            .Returns(Task.FromResult(otherVersions));
        mockMapper.Setup(x => x.Map<LesDto>(It.IsAny<Les>()))
            .Returns(new LesDto());
        mockMapper.Setup(x => x.Map<List<LesDto>>(It.IsAny<List<Les>>()))
            .Returns(new List<LesDto>());
        mockMapper.Setup(x => x.Map<List<LeeruitkomstDto>>(It.IsAny<List<Leeruitkomst>>()))
            .Returns(new List<LeeruitkomstDto>());

        // Act
        var result = await service.HaalLessenOpMetEerdereVersies(groupId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(currentVersion.Id, result.Les.Id);
        // Assert other properties of LesDto and LesLeeruitkomstenDto
        mockLesRepository.VerifyAll();
        mockMapper.VerifyAll();
    }

    [Fact]
    public async Task KoppelLukAanLes_Success_ReturnsTrue()
    {
        // Arrange
        Guid lesGroupId = Guid.NewGuid();
        Guid lukGroupId = Guid.NewGuid();
        var les = new Les
        {
            GroupId = Guid.NewGuid(),
            Leeruitkomsten = new List<Leeruitkomst>()
        };
        var luk = new Leeruitkomst();
        mockLesRepository.Setup(x => x.GetLessonsWithLearningGoals(lesGroupId)).ReturnsAsync(new List<Les> { les });
        mockLeeruitkomstRepository.Setup(x => x.NieuwsteVoorGroepId(lukGroupId)).ReturnsAsync(luk);
        mockLesRepository.Setup(x => x.Update(It.IsAny<Les>())).ReturnsAsync(true);

        // Act
        var result = await CreateService().KoppelLukAanLes(lesGroupId, lukGroupId);

        // Assert
        Assert.True(result);
        mockLesRepository.Verify(x => x.GetLessonsWithLearningGoals(lesGroupId), Times.Once);
        mockLeeruitkomstRepository.Verify(x => x.NieuwsteVoorGroepId(lukGroupId), Times.Once);
        mockLesRepository.Verify(x => x.Update(les), Times.Once);
    }

    [Fact]
    public async Task OntkoppelLukAanLes_Success_ReturnsTrue()
    {
        // Arrange
        Guid lesGroupId = Guid.NewGuid();
        Guid lukGroupId = Guid.NewGuid();
        var les = new Les
        {
            GroupId = Guid.NewGuid(),
            Leeruitkomsten = new List<Leeruitkomst>
            {
                new Leeruitkomst { GroupId = lukGroupId },
                new Leeruitkomst { GroupId = Guid.NewGuid() }
            }
        };
        mockLesRepository.Setup(x => x.GetLessonsWithLearningGoals(lesGroupId))
            .ReturnsAsync(new List<Les> { les });
        mockLesRepository.Setup(x => x.Update(It.IsAny<Les>()))
            .ReturnsAsync(true);

        var service = this.CreateService();

        // Act
        var result = await service.OntkoppelLukAanLes(lesGroupId, lukGroupId);

        // Assert
        Assert.True(result);
        mockLesRepository.Verify(x => x.GetLessonsWithLearningGoals(lesGroupId), Times.Once);
        mockLesRepository.Verify(x => x.Update(It.Is<Les>(l =>
            l.Id == les.Id &&
            l.Leeruitkomsten.Count == 1 &&
            l.Leeruitkomsten.All(lu => lu.GroupId != lukGroupId))), Times.Once);
    }

    [Fact]
    public async Task MaakKopie_ExistingLes_ReturnsNewGroupId()
    {
        // Arrange
        Guid originalGroupId = Guid.NewGuid();
        int versieNummer = 1;
        var originalLes = new Les { GroupId = originalGroupId, VersieNummer = versieNummer };
        mockLesRepository.Setup(x => x.Lijst(It.IsAny<Expression<Func<Les, bool>>>()))
            .ReturnsAsync(new List<Les> { originalLes });
        mockLesRepository.Setup(x => x.Maak(It.IsAny<Les>())).ReturnsAsync(new Les());

        var service = this.CreateService();

        // Act
        var newGroupId = await service.MaakKopie(originalGroupId, versieNummer);

        // Assert
        Assert.NotEqual(originalGroupId, newGroupId);
        mockLesRepository.Verify(x => x.Lijst(It.Is<Expression<Func<Les, bool>>>(expr => expr.Compile()(originalLes))), Times.Once);
        mockLesRepository.Verify(x => x.Maak(It.Is<Les>(l => l.GroupId == newGroupId)), Times.Once);
    }

    [Fact]
    public async Task Update_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var service = this.CreateService();
        var request = new UpdateLesDto
        {
            GroupId = Guid.NewGuid(),
            Naam = "New Lesson Name",
            Beschrijving = "New Lesson Description"
        };
        var existingLes = new Les
        {
            Id = 1,
            GroupId = Guid.NewGuid(),
            Naam = "Old Lesson Name",
            Beschrijving = "Old Lesson Description"
        };
        mockLesRepository.Setup(x => x.NieuwsteVoorGroepId(request.GroupId)).ReturnsAsync(existingLes);
        mockLesRepository.Setup(x => x.Update(It.IsAny<Les>())).ReturnsAsync(true);

        // Act
        var result = await service.Update(request);

        // Assert
        Assert.True(result);
        mockRepository.VerifyAll();
    }
}
