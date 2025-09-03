namespace ProductCatalog.Application.Contracts.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
