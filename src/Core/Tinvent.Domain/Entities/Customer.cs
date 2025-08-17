using System.ComponentModel.DataAnnotations;
using Tinvent.Domain.Common;

namespace Tinvent.Domain.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        [MaxLength(250)]
        public string FullName { get; private set; }

        [MaxLength(250)]
        public string CompanyName { get; private set; }

        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; private set; }

        [MaxLength(50)]
        [Phone]
        public string PhoneNumber { get; private set; }

        [MaxLength(500)]
        public string BillingAddress { get; private set; }

        [MaxLength(500)]
        public string ShippingAddress { get; private set; }

        [Required]
        public bool IsActive { get; private set; }

        // Foreign Key
        public Guid TenantId { get; private set; }

        // Navigation Properties
        public virtual Tenant Tenant { get; private set; }
        public virtual ICollection<CustomerOrder> Orders { get; private set; } = new List<CustomerOrder>();

        private Customer() { }

        public Customer(Guid tenantId, string fullName, string email)
        {
            TenantId = tenantId;
            FullName = fullName;
            Email = email;
            IsActive = true;
        }

        public void Update(string fullName, string companyName, string email, string phone, string billingAddress, string shippingAddress)
        {
            FullName = fullName;
            CompanyName = companyName;
            Email = email;
            PhoneNumber = phone;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            SetUpdated();
        }
    }
}