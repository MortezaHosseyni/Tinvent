using System.ComponentModel.DataAnnotations;
using Tinvent.Domain.Common;

namespace Tinvent.Domain.Entities
{
    public class Warehouse : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; private set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; private set; }

        [MaxLength(500)]
        public string Address { get; private set; }

        [Required]
        public bool IsActive { get; private set; }

        // Foreign Keys
        public Guid TenantId { get; private set; }
        public Guid? ManagerUserId { get; private set; }

        // Navigation Properties
        public virtual Tenant Tenant { get; private set; }
        public virtual User Manager { get; private set; }
        public virtual ICollection<InventorySnapshot> InventorySnapshots { get; private set; } = new List<InventorySnapshot>();

        private Warehouse() { }

        public Warehouse(Guid tenantId, string name, string code, string address)
        {
            TenantId = tenantId;
            Name = name;
            Code = code;
            Address = address;
            IsActive = true;
        }

        public void Update(string name, string address)
        {
            Name = name;
            Address = address;
            SetUpdated();
        }

        public void AssignManager(Guid managerUserId)
        {
            ManagerUserId = managerUserId;
            SetUpdated();
        }
    }
}