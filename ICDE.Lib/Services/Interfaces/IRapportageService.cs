namespace ICDE.Lib.Services.Interfaces;
public interface IRapportageService
{
    Task<bool> ValidateOpleiding(Guid opleidingGroupId);
}
