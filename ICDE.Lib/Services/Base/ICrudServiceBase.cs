namespace ICDE.Lib.Services.Base;

public interface ICrudServiceBase<TDto, TCreateDto, TUpdateDto>
{
    Task<TDto?> Maak(TCreateDto request);
    Task<bool> Update(TUpdateDto request);
    Task<bool> Verwijder(int id);
}