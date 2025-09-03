using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Dormain;

namespace ProductCatalog.Persistence.Repository
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        private readonly ProductCatalogDbContext _dbContext;
        public AuthRepository(ProductCatalogDbContext dbContext):base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUser(Guid userId)
        {
           User? user = await _dbContext.Users.FirstOrDefaultAsync(x=>x.Id == userId);
           return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email!.ToLower() == email.ToLower());
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            IEnumerable<User> users = Enumerable.Empty<User>();
            users = await _dbContext.Users.ToListAsync();

            return users;

        }

        public async Task<bool> IsUserExist(string email)
        {
            bool IsRegisteredUser=false;
            IsRegisteredUser = await _dbContext.Users.AnyAsync(x=>x.Email == email);

            return IsRegisteredUser;
        }
    }
}
