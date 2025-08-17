using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinvent.Domain.Common;

namespace Tinvent.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        [Required]
        public int Quantity { get; private set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; private set; }

        [NotMapped]
        public decimal TotalPrice => Quantity * UnitPrice;

        // Foreign Keys
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid WarehouseId { get; private set; }

        // Navigation Properties
        public virtual CustomerOrder Order { get; private set; }
        public virtual Product Product { get; private set; }
        public virtual Warehouse Warehouse { get; private set; }

        private OrderItem() { }

        public OrderItem(Guid orderId, Guid productId, Guid warehouseId, int quantity, decimal unitPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            WarehouseId = warehouseId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void UpdateQuantity(int newQuantity)
        {
            Quantity = newQuantity;
            SetUpdated();
        }
    }
}