namespace ProductCatalog.Application.Contracts.Authentication
{
    public interface IUserContext
    {
        Guid GetUserId();
        string GetUserName();
    }
}
