using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinvent.Domain.Common;
using Tinvent.Domain.Enums;

namespace Tinvent.Domain.Entities
{
    public class StockTransfer : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string TransferNumber { get; private set; }

        [Required]
        public StockTransferStatus Status { get; private set; }

        [MaxLength(1000)]
        public string Notes { get; private set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime InitiatedDate { get; private set; }

        [DataType(DataType.DateTime)]
        public DateTime? CompletedDate { get; private set; }

        // Foreign Keys
        public Guid TenantId { get; private set; }
        public Guid FromWarehouseId { get; private set; }
        public Guid ToWarehouseId { get; private set; }
        public Guid InitiatedByUserId { get; private set; }
        public Guid? CompletedByUserId { get; private set; }

        // Navigation Properties
        public virtual Tenant Tenant { get; private set; }
        public virtual Warehouse FromWarehouse { get; private set; }
        public virtual Warehouse ToWarehouse { get; private set; }
        public virtual User InitiatedByUser { get; private set; }
        public virtual User CompletedByUser { get; private set; }
        public virtual ICollection<StockTransferItem> StockTransferItems { get; private set; } = new List<StockTransferItem>();

        private StockTransfer() { }

        public StockTransfer(Guid tenantId, Guid fromWarehouseId, Guid toWarehouseId, Guid initiatedByUserId, string transferNumber)
        {
            TenantId = tenantId;
            FromWarehouseId = fromWarehouseId;
            ToWarehouseId = toWarehouseId;
            InitiatedByUserId = initiatedByUserId;
            TransferNumber = transferNumber;
            Status = StockTransferStatus.Pending;
            InitiatedDate = DateTime.UtcNow;
        }

        public void UpdateStatus(StockTransferStatus newStatus)
        {
            Status = newStatus;
            SetUpdated();
        }

        public void CompleteTransfer(Guid completedByUserId)
        {
            Status = StockTransferStatus.Completed;
            CompletedByUserId = completedByUserId;
            CompletedDate = DateTime.UtcNow;
            SetUpdated();
        }
    }
}