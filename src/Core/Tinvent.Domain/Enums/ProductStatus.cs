namespace Tinvent.Domain.Enums
{
    public enum ProductStatus
    {
        /// <summary>
        /// The product is active and available for sale.
        /// </summary>
        Active,

        /// <summary>
        /// The product is inactive and not shown in listings.
        /// </summary>
        Archived,

        /// <summary>
        /// The product is no longer sold.
        /// </summary>
        Discontinued
    }
}