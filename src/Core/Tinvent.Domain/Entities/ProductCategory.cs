using System.ComponentModel.DataAnnotations;
using Tinvent.Domain.Common;

namespace Tinvent.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; private set; }

        [MaxLength(500)]
        public string Description { get; private set; }

        [Required]
        public bool IsActive { get; private set; }

        // Foreign Keys
        public Guid TenantId { get; private set; }
        public Guid? ParentCategoryId { get; private set; }

        // Navigation Properties
        public virtual Tenant Tenant { get; private set; }
        public virtual ProductCategory ParentCategory { get; private set; }
        public virtual ICollection<ProductCategory> ChildCategories { get; private set; } = new List<ProductCategory>();
        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();

        private ProductCategory() { }

        public ProductCategory(Guid tenantId, string name, string description, Guid? parentCategoryId = null)
        {
            TenantId = tenantId;
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
            IsActive = true;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
            SetUpdated();
        }
    }
}