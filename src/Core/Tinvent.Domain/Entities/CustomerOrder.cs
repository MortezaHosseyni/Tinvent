using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinvent.Domain.Common;
using Tinvent.Domain.Enums;

namespace Tinvent.Domain.Entities
{
    public class CustomerOrder : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; private set; }

        [Required]
        public OrderStatus Status { get; private set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SubtotalAmount { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TaxAmount { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ShippingFee { get; private set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; private set; }

        [MaxLength(500)]
        public string ShippingAddress { get; private set; }

        [Required]
        public PaymentStatus PaymentStatus { get; private set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; private set; }

        [DataType(DataType.DateTime)]
        public DateTime? ShippedDate { get; private set; }

        // Foreign Keys
        public Guid TenantId { get; private set; }
        public Guid CustomerId { get; private set; }

        // Navigation Properties
        public virtual Tenant Tenant { get; private set; }
        public virtual Customer Customer { get; private set; }
        public virtual ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        private CustomerOrder() { }

        public CustomerOrder(Guid tenantId, Guid customerId, string orderNumber, string shippingAddress)
        {
            TenantId = tenantId;
            CustomerId = customerId;
            OrderNumber = orderNumber;
            ShippingAddress = shippingAddress;
            Status = OrderStatus.Pending;
            PaymentStatus = PaymentStatus.Pending;
            OrderDate = DateTime.UtcNow;
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            if (newStatus == OrderStatus.Shipped && !ShippedDate.HasValue)
            {
                ShippedDate = DateTime.UtcNow;
            }
            SetUpdated();
        }

        public void UpdatePaymentStatus(PaymentStatus newPaymentStatus)
        {
            PaymentStatus = newPaymentStatus;
            SetUpdated();
        }

        public void CalculateTotals()
        {
            SubtotalAmount = OrderItems.Sum(item => item.TotalPrice);
            // Assume tax and shipping are calculated elsewhere
            TotalAmount = SubtotalAmount + TaxAmount + ShippingFee;
            SetUpdated();
        }
    }
}