namespace Tinvent.Domain.Enums
{
    public enum OrderStatus
    {
        /// <summary>
        /// Order has been placed but not yet processed.
        /// </summary>
        Pending,

        /// <summary>
        /// Order is being prepared for shipment.
        /// </summary>
        Processing,

        /// <summary>
        /// Order has been shipped to the customer.
        /// </summary>
        Shipped,

        /// <summary>
        /// Order has been delivered to the customer.
        /// </summary>
        Delivered,

        /// <summary>
        /// Order has been cancelled.
        /// </summary>
        Cancelled
    }
}