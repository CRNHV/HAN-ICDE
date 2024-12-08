using ICDE.Lib.Dto.Lessen;

namespace ICDE.Lib.Services.Interfaces;
public interface ILesService
{
    Task<LesDto?> CreateLesson(string naam, string beschrijving);
    Task<List<LesDto>> GetAll();
    Task<LesMetEerdereVersies?> GetLessonWithPreviousVersions(Guid groupId);
    Task<bool> KoppelLukAanLes(Guid lesGroupId, Guid lukGroupId);
    Task<bool> OntkoppelLukAanLes(Guid lesGroupId, Guid lukGroupId);
}
