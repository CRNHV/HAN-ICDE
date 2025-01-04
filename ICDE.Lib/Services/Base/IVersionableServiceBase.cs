namespace ICDE.Lib.Services.Base;
public interface IVersionableServiceBase<TReadDto, TCreateDto, TUpdateDto> : ICrudServiceBase<TReadDto, TCreateDto, TUpdateDto>
{
    Task<List<TReadDto>> AlleUnieke();
    Task<TReadDto?> BekijkVersie(Guid groupId, int versieNummer);
    Task<List<TReadDto>> EerdereVersies(Guid groupId, int versieNummer);
    Task<Guid> MaakKopie(Guid groupId, int versieNummer);
    Task<bool> VerwijderVersie(Guid groupId, int versieNummer);
    Task<TReadDto?> NieuwsteVoorGroepId(Guid groupId);
}