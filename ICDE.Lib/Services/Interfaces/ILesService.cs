using ICDE.Lib.Dto.Lessen;

namespace ICDE.Lib.Services.Interfaces;
public interface ILesService
{
    Task<LesDto?> Maak(string naam, string beschrijving);
    Task<bool> VerwijderVersie(Guid groupId, int versionId);
    Task<List<LesDto>> Allemaal();
    Task<LesMetEerdereVersies?> HaalLessenOpMetEerdereVersies(Guid groupId);
    Task<bool> KoppelLukAanLes(Guid lesGroupId, Guid lukGroupId);
    Task<bool> OntkoppelLukAanLes(Guid lesGroupId, Guid lukGroupId);
    Task<bool> Update(LesUpdateDto request);
}
