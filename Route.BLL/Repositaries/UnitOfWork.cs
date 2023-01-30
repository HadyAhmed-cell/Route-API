using Route.BLL.Interfaces;
using Route.DAL.Data;
using Route.DAL.Entities;
using System.Collections;

namespace Route.BLL.Repositaries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositaries;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();

        public IGenericRepositary<TEntity> Repositary<TEntity>() where TEntity : BaseEntity
        {
            if (_repositaries is null)
            {
                _repositaries = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositaries.ContainsKey(type))
            {
                var repository = new GenericRepositary<TEntity>(_context);
                _repositaries.Add(type, repository);
            }

            return (IGenericRepositary<TEntity>)_repositaries[type];
        }
    }
}
