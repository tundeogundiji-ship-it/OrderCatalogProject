

namespace ProductCatalog.Application.Dtos.Authentication
{
    public class GetUsersDto
    {
        public Guid Id { get; set; }
        public string? FirstName {  get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime DateCreated {  get; set; }
    }
}
