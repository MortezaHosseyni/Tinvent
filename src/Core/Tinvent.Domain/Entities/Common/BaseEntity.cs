using System.ComponentModel.DataAnnotations;

namespace Tinvent.Domain.Common
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Unique identifier for the entity.
        /// </summary>
        [Key]
        public Guid Id { get; protected set; }

        /// <summary>
        /// The date and time when the entity was created.
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// The date and time when the entity was last updated.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Sets the UpdatedAt property to the current UTC time.
        /// Should be called whenever an entity is modified.
        /// </summary>
        public virtual void SetUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}