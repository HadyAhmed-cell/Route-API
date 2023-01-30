using Microsoft.EntityFrameworkCore;
using Route.BLL.Interfaces;
using Route.BLL.Specifications;
using Route.DAL.Data;
using Route.DAL.Entities;

namespace Route.BLL.Repositaries
{
    public class GenericRepositary<T> : IGenericRepositary<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepositary(StoreContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        => _context.Set<T>().Add(entity);

        public void Delete(T entity)
                => _context.Set<T>().Remove(entity);


        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
           => await ApplySpecifications(spec).ToListAsync();

        public async Task<T> GetByIdAsync(int id)
        => await _context.Set<T>().FindAsync(id);

        public async Task<int> GetCountAsync(ISpecification<T> spec)
         => await ApplySpecifications(spec).CountAsync();

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
         => await ApplySpecifications(spec).FirstOrDefaultAsync();

        public void Update(T entity)
                => _context.Set<T>().Update(entity);


        private IQueryable<T> ApplySpecifications(ISpecification<T> specification)
            => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specification);
    }
}
