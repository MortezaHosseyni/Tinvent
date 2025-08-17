namespace Tinvent.Domain.Enums
{
    public enum PurchaseOrderStatus
    {
        /// <summary>
        /// The order is a draft and not yet sent to the supplier.
        /// </summary>
        Draft,

        /// <summary>
        /// The order has been sent to the supplier.
        /// </summary>
        Ordered,

        /// <summary>
        /// Some items have been received, but not all.
        /// </summary>
        PartiallyReceived,

        /// <summary>
        /// All items in the order have been received.
        /// </summary>
        Received,

        /// <summary>
        /// The purchase order has been cancelled.
        /// </summary>
        Cancelled
    }
}