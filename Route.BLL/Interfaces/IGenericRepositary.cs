using Route.BLL.Specifications;
using Route.DAL.Entities;

namespace Route.BLL.Interfaces
{
    public interface IGenericRepositary<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);



        Task<int> GetCountAsync(ISpecification<T> spec);

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
