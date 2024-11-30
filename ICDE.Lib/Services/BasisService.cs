namespace ICDE.Lib.Services;
internal abstract class BasisService<T> where T : class
{
    public abstract Task<T> Maak();
    public abstract Task<T> Verwijder();
    public abstract Task<T> Bekijk();
    public abstract Task<T> Update();
}
