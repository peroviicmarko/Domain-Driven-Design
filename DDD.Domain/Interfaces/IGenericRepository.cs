namespace DDD.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task DeleteAsync(int id);
        Task<int> SaveChangesAsync();
        bool Update(T entity);
        int SaveChanges();
    }

}
