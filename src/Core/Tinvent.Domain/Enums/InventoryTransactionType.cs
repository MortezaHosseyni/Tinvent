namespace Tinvent.Domain.Enums
{
    public enum InventoryTransactionType
    {
        /// <summary>
        /// Stock received from a purchase order.
        /// </summary>
        Purchase,

        /// <summary>
        /// Stock dispatched for a customer order.
        /// </summary>
        Sale,

        /// <summary>
        /// Manual stock count adjustment (e.g., due to damage or loss).
        /// </summary>
        Adjustment,

        /// <summary>
        /// Stock sent out as part of a warehouse transfer.
        /// </summary>
        TransferOut,

        /// <summary>
        /// Stock received as part of a warehouse transfer.
        /// </summary>
        TransferIn,

        /// <summary>
        /// Initial stock quantity when adding a new product.
        /// </summary>
        InitialStock,

        /// <summary>
        /// Stock returned by a customer.
        /// </summary>
        Return
    }
}