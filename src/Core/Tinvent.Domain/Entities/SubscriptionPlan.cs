using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinvent.Domain.Common;

namespace Tinvent.Domain.Entities
{
    public class SubscriptionPlan : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; private set; } // e.g., "Basic", "Pro", "Enterprise"

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; private set; }

        [Required]
        public int UserLimit { get; private set; }

        [Required]
        public int ProductLimit { get; private set; }

        [Required]
        public int WarehouseLimit { get; private set; }

        [Required]
        public bool HasAdvancedReports { get; private set; }

        [Required]
        public bool IsActive { get; private set; }

        // Navigation Property
        public virtual ICollection<Tenant> Tenants { get; private set; } = new List<Tenant>();

        private SubscriptionPlan() { }

        public SubscriptionPlan(string name, decimal price, int userLimit, int productLimit, int warehouseLimit, bool hasAdvancedReports)
        {
            Name = name;
            Price = price;
            UserLimit = userLimit;
            ProductLimit = productLimit;
            WarehouseLimit = warehouseLimit;
            HasAdvancedReports = hasAdvancedReports;
            IsActive = true;
        }

        public void Update(string name, decimal price, int userLimit, int productLimit, int warehouseLimit, bool hasAdvancedReports)
        {
            Name = name;
            Price = price;
            UserLimit = userLimit;
            ProductLimit = productLimit;
            WarehouseLimit = warehouseLimit;
            HasAdvancedReports = hasAdvancedReports;
            SetUpdated();
        }

        public void Deactivate()
        {
            IsActive = false;
            SetUpdated();
        }

        public void Activate()
        {
            IsActive = true;
            SetUpdated();
        }
    }
}