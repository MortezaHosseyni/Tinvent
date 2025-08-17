namespace Tinvent.Domain.Enums
{
    public enum TenantStatus
    {
        /// <summary>
        /// The tenant's account is active and in good standing.
        /// </summary>
        Active,

        /// <summary>
        /// The tenant's account is temporarily suspended.
        /// </summary>
        Suspended,

        /// <summary>
        /// The tenant's account has been permanently cancelled.
        /// </summary>
        Cancelled
    }
}