using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinvent.Domain.Common;
using Tinvent.Domain.Enums;

namespace Tinvent.Domain.Entities
{
    public class PurchaseOrder : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; private set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public PurchaseOrderStatus Status { get; private set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; private set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; private set; }

        [DataType(DataType.Date)]
        public DateTime? ExpectedDeliveryDate { get; private set; }

        [DataType(DataType.DateTime)]
        public DateTime? ActualDeliveryDate { get; private set; }

        // Foreign Keys
        public Guid TenantId { get; private set; }
        public Guid SupplierId { get; private set; }
        public Guid WarehouseId { get; private set; } // Destination warehouse
        public Guid CreatedByUserId { get; private set; }

        // Navigation Properties
        public virtual Tenant Tenant { get; private set; }
        public virtual Supplier Supplier { get; private set; }
        public virtual Warehouse Warehouse { get; private set; }
        public virtual User CreatedByUser { get; private set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; private set; } = new List<PurchaseOrderItem>();

        private PurchaseOrder() { }

        public PurchaseOrder(Guid tenantId, Guid supplierId, Guid warehouseId, Guid createdByUserId, string orderNumber, DateTime expectedDelivery)
        {
            TenantId = tenantId;
            SupplierId = supplierId;
            WarehouseId = warehouseId;
            CreatedByUserId = createdByUserId;
            OrderNumber = orderNumber;
            ExpectedDeliveryDate = expectedDelivery;
            Status = PurchaseOrderStatus.Draft;
            OrderDate = DateTime.UtcNow;
        }

        public void UpdateStatus(PurchaseOrderStatus newStatus)
        {
            Status = newStatus;
            if (newStatus == PurchaseOrderStatus.Received && !ActualDeliveryDate.HasValue)
            {
                ActualDeliveryDate = DateTime.UtcNow;
            }
            SetUpdated();
        }
    }
}