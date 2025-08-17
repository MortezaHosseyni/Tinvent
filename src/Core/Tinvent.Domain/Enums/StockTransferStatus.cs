namespace Tinvent.Domain.Enums
{
    public enum StockTransferStatus
    {
        /// <summary>
        /// The transfer has been initiated but not yet shipped.
        /// </summary>
        Pending,

        /// <summary>
        /// The stock is currently being moved between warehouses.
        /// </summary>
        InTransit,

        /// <summary>
        /// The stock has been received at the destination warehouse.
        /// </summary>
        Completed,

        /// <summary>
        /// The transfer was cancelled.
        /// </summary>
        Cancelled
    }
}