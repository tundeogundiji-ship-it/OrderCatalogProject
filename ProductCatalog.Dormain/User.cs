using ProductCatalog.Dormain.Common;


namespace ProductCatalog.Dormain
{
    public class User: BaseDormainEntity
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PasswordHash { get; set; }
    }
}
