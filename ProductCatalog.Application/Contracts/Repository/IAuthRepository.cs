using ProductCatalog.Dormain;

namespace ProductCatalog.Application.Contracts.Repository
{
    public interface IAuthRepository:IGenericRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
        Task<bool> IsUserExist(string email);
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUser(Guid userId);
    }
}
