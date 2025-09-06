using Microsoft.AspNetCore.Http;
using ProductCatalog.Application.Contracts.Authentication;



namespace ProductCatalog.Persistence.Authentication
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var claim = httpContextAccessor.HttpContext!.User.Claims.ToArray();
            return Guid.Parse(claim[0].Value);

        }

        public string GetUserName()
        {
            var claim = httpContextAccessor.HttpContext!.User.Claims.ToArray();
            return claim[2].Value!;
        }
    }
}
