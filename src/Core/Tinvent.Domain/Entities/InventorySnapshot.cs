using System.ComponentModel.DataAnnotations;
using Tinvent.Domain.Common;

namespace Tinvent.Domain.Entities
{
    public class InventorySnapshot : BaseEntity
    {
        [Required]
        public int QuantityOnHand { get; private set; }

        public int ReorderLevel { get; private set; }

        public int MaxStockLevel { get; private set; }

        [MaxLength(50)]
        public string BinLocation { get; private set; }

        // Foreign Keys
        public Guid ProductId { get; private set; }
        public Guid WarehouseId { get; private set; }

        // Navigation Properties
        public virtual Product Product { get; private set; }
        public virtual Warehouse Warehouse { get; private set; }

        private InventorySnapshot() { }

        public InventorySnapshot(Guid productId, Guid warehouseId, int initialQuantity)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
            QuantityOnHand = initialQuantity;
        }

        public void UpdateQuantity(int newQuantity)
        {
            QuantityOnHand = newQuantity;
            SetUpdated();
        }

        public void AdjustQuantity(int change)
        {
            QuantityOnHand += change;
            SetUpdated();
        }

        public void UpdateLevels(int reorderLevel, int maxStockLevel)
        {
            ReorderLevel = reorderLevel;
            MaxStockLevel = maxStockLevel;
            SetUpdated();
        }
    }
}