using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinvent.Domain.Common;
using Tinvent.Domain.Enums;

namespace Tinvent.Domain.Entities
{
    public class InventoryTransaction : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public InventoryTransactionType TransactionType { get; private set; }

        [Required]
        public int QuantityChange { get; private set; }

        [Required]
        public int QuantityBefore { get; private set; }

        [Required]
        public int QuantityAfter { get; private set; }

        [MaxLength(1000)]
        public string Notes { get; private set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; private set; }

        // Foreign Keys
        public Guid TenantId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid WarehouseId { get; private set; }
        public Guid UserId { get; private set; } // User who performed the transaction
        public Guid? SourceOrderId { get; private set; }
        public Guid? SourcePurchaseId { get; private set; }

        // Navigation Properties
        public virtual Tenant Tenant { get; private set; }
        public virtual Product Product { get; private set; }
        public virtual Warehouse Warehouse { get; private set; }
        public virtual User User { get; private set; }
        public virtual CustomerOrder SourceOrder { get; private set; }
        public virtual PurchaseOrder SourcePurchase { get; private set; }

        private InventoryTransaction() { }

        public InventoryTransaction(Guid tenantId, Guid productId, Guid warehouseId, Guid userId, InventoryTransactionType type, int qtyChange, int qtyBefore, string notes)
        {
            TenantId = tenantId;
            ProductId = productId;
            WarehouseId = warehouseId;
            UserId = userId;
            TransactionType = type;
            QuantityChange = qtyChange;
            QuantityBefore = qtyBefore;
            QuantityAfter = qtyBefore + qtyChange;
            Notes = notes;
            TransactionDate = DateTime.UtcNow;
        }
    }
}