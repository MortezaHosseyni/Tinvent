using System.ComponentModel.DataAnnotations;
using Tinvent.Domain.Common;

namespace Tinvent.Domain.Entities
{
    public class StockTransferItem : BaseEntity
    {
        [Required]
        public int QuantityTransferred { get; private set; }

        [MaxLength(500)]
        public string Notes { get; private set; }

        // Foreign Keys
        public Guid StockTransferId { get; private set; }
        public Guid ProductId { get; private set; }

        // Navigation Properties
        public virtual StockTransfer StockTransfer { get; private set; }
        public virtual Product Product { get; private set; }

        private StockTransferItem() { }

        public StockTransferItem(Guid stockTransferId, Guid productId, int quantity)
        {
            StockTransferId = stockTransferId;
            ProductId = productId;
            QuantityTransferred = quantity;
        }
    }
}