using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Dormain.Common
{
    public abstract class BaseDormainEntity
    {
        [Key, Required]
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime DateCreated { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy {  get; set; }
    }
}
