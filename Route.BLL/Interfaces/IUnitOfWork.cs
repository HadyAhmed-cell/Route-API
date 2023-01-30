using Route.DAL.Entities;

namespace Route.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepositary<TEntity> Repositary<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
