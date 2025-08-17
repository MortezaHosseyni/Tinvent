using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinvent.Domain.Common;
using Tinvent.Domain.Enums;

namespace Tinvent.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Sku { get; private set; } // Stock Keeping Unit

        [MaxLength(100)]
        public string Barcode { get; private set; }

        [MaxLength(4000)]
        public string Description { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CostPrice { get; private set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SalePrice { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Weight { get; private set; }

        [MaxLength(100)]
        public string Dimensions { get; private set; }

        [Required]
        public ProductStatus Status { get; private set; }

        // Foreign Keys
        public Guid TenantId { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid? DefaultSupplierId { get; private set; }

        // Navigation Properties
        public virtual Tenant Tenant { get; private set; }
        public virtual ProductCategory Category { get; private set; }
        public virtual Supplier DefaultSupplier { get; private set; }
        public virtual ICollection<ProductImage> Images { get; private set; } = new List<ProductImage>();
        public virtual ICollection<InventorySnapshot> InventorySnapshots { get; private set; } = new List<InventorySnapshot>();

        private Product() { }

        public Product(Guid tenantId, string name, string sku, decimal salePrice, Guid categoryId)
        {
            TenantId = tenantId;
            Name = name;
            Sku = sku;
            SalePrice = salePrice;
            CategoryId = categoryId;
            Status = ProductStatus.Active;
        }

        public void UpdateDetails(string name, string description, decimal salePrice, Guid categoryId)
        {
            Name = name;
            Description = description;
            SalePrice = salePrice;
            CategoryId = categoryId;
            SetUpdated();
        }

        public void UpdatePricing(decimal costPrice, decimal salePrice)
        {
            CostPrice = costPrice;
            SalePrice = salePrice;
            SetUpdated();
        }

        public void ChangeStatus(ProductStatus newStatus)
        {
            Status = newStatus;
            SetUpdated();
        }
    }
}