using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinvent.Domain.Common;
using Tinvent.Domain.Enums;

namespace Tinvent.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; private set; }

        [MaxLength(250)]
        public string ContactPerson { get; private set; }

        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; private set; }

        [MaxLength(50)]
        [Phone]
        public string PhoneNumber { get; private set; }

        [MaxLength(500)]
        public string Address { get; private set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public SupplierStatus Status { get; private set; }

        // Foreign Key
        public Guid TenantId { get; private set; }

        // Navigation Properties
        public virtual Tenant Tenant { get; private set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; private set; } = new List<PurchaseOrder>();
        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();

        private Supplier() { }

        public Supplier(Guid tenantId, string name, string contactPerson, string email)
        {
            TenantId = tenantId;
            Name = name;
            ContactPerson = contactPerson;
            Email = email;
            Status = SupplierStatus.Active;
        }

        public void Update(string name, string contactPerson, string email, string phone, string address)
        {
            Name = name;
            ContactPerson = contactPerson;
            Email = email;
            PhoneNumber = phone;
            Address = address;
            SetUpdated();
        }

        public void ChangeStatus(SupplierStatus newStatus)
        {
            Status = newStatus;
            SetUpdated();
        }
    }
}