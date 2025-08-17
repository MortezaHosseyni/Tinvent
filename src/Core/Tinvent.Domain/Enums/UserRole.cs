namespace Tinvent.Domain.Enums
{
    public enum UserRole
    {
        /// <summary>
        /// Has full access to all features within the tenant.
        /// </summary>
        Admin,

        /// <summary>
        /// Has access to management features like orders and inventory.
        /// </summary>
        Manager,

        /// <summary>
        /// Has limited access for day-to-day operations.
        /// </summary>
        Staff
    }
}