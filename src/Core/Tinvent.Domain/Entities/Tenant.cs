using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinvent.Domain.Common;
using Tinvent.Domain.Enums;

namespace Tinvent.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Subdomain { get; private set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public TenantStatus Status { get; private set; }

        // Foreign Key
        public Guid SubscriptionPlanId { get; private set; }

        // Navigation Properties
        public virtual SubscriptionPlan SubscriptionPlan { get; private set; }
        public virtual ICollection<User> Users { get; private set; } = new List<User>();
        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();
        public virtual ICollection<Warehouse> Warehouses { get; private set; } = new List<Warehouse>();
        public virtual ICollection<Customer> Customers { get; private set; } = new List<Customer>();
        public virtual ICollection<Supplier> Suppliers { get; private set; } = new List<Supplier>();
        public virtual ICollection<CustomerOrder> CustomerOrders { get; private set; } = new List<CustomerOrder>();
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; private set; } = new List<PurchaseOrder>();

        // Private constructor for EF Core
        private Tenant() { }

        public Tenant(string name, string subdomain, Guid subscriptionPlanId)
        {
            Name = name;
            Subdomain = subdomain;
            SubscriptionPlanId = subscriptionPlanId;
            Status = TenantStatus.Active;
        }

        public void Update(string name, string subdomain)
        {
            Name = name;
            Subdomain = subdomain;
            SetUpdated();
        }

        public void ChangeStatus(TenantStatus newStatus)
        {
            Status = newStatus;
            SetUpdated();
        }

        public void ChangeSubscriptionPlan(Guid newPlanId)
        {
            SubscriptionPlanId = newPlanId;
            SetUpdated();
        }
    }
}