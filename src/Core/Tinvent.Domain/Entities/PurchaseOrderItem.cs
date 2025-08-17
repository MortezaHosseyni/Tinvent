using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinvent.Domain.Common;

namespace Tinvent.Domain.Entities
{
    public class PurchaseOrderItem : BaseEntity
    {
        [Required]
        public int QuantityOrdered { get; private set; }

        [Required]
        public int QuantityReceived { get; private set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitCost { get; private set; }

        [NotMapped]
        public decimal TotalCost => QuantityOrdered * UnitCost;

        // Foreign Keys
        public Guid PurchaseOrderId { get; private set; }
        public Guid ProductId { get; private set; }

        // Navigation Properties
        public virtual PurchaseOrder PurchaseOrder { get; private set; }
        public virtual Product Product { get; private set; }

        private PurchaseOrderItem() { }

        public PurchaseOrderItem(Guid purchaseOrderId, Guid productId, int quantity, decimal unitCost)
        {
            PurchaseOrderId = purchaseOrderId;
            ProductId = productId;
            QuantityOrdered = quantity;
            UnitCost = unitCost;
            QuantityReceived = 0;
        }

        public void UpdateReceivedQuantity(int received)
        {
            QuantityReceived += received;
            SetUpdated();
        }
    }
}