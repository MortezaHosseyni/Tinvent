namespace Tinvent.Domain.Enums
{
    public enum PaymentStatus
    {
        /// <summary>
        /// Payment has not been received yet.
        /// </summary>
        Pending,

        /// <summary>
        /// Payment has been successfully processed.
        /// </summary>
        Paid,

        /// <summary>
        /// Payment failed to process.
        /// </summary>
        Failed,

        /// <summary>
        /// Payment has been refunded.
        /// </summary>
        Refunded
    }
}