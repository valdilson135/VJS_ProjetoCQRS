namespace Vjs.NegocioCore.DataRepository
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}