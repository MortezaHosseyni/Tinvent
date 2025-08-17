using System.ComponentModel.DataAnnotations;
using Tinvent.Domain.Common;

namespace Tinvent.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        [Required]
        [MaxLength(2048)]
        [Url]
        public string ImageUrl { get; private set; }

        [MaxLength(250)]
        public string AltText { get; private set; }

        [Required]
        public bool IsPrimary { get; private set; }

        [Required]
        public int SortOrder { get; private set; }

        // Foreign Key
        public Guid ProductId { get; private set; }

        // Navigation Property
        public virtual Product Product { get; private set; }

        private ProductImage() { }

        public ProductImage(Guid productId, string imageUrl, string altText, bool isPrimary = false, int sortOrder = 0)
        {
            ProductId = productId;
            ImageUrl = imageUrl;
            AltText = altText;
            IsPrimary = isPrimary;
            SortOrder = sortOrder;
        }

        public void Update(string altText, int sortOrder)
        {
            AltText = altText;
            SortOrder = sortOrder;
            SetUpdated();
        }

        public void SetAsPrimary(bool isPrimary)
        {
            IsPrimary = isPrimary;
            SetUpdated();
        }
    }
}