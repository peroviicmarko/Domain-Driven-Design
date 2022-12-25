using DDD.Data.Context;
using DDD.Domain;
using DDD.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DDD.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDomainEntity
    {

        public DbContext _context { get; set; }
        public DbSet<T> _dbSet { get; set; }

        public GenericRepository(TestDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _dbSet.FindAsync(id);

            if (model != null)
            {
               _dbSet.Remove(model);
            }
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();  
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Entry(entity).State = EntityState.Modified;

            return true;
        }
    }
}
