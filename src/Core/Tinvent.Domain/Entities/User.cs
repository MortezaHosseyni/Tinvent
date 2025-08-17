using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tinvent.Domain.Common;
using Tinvent.Domain.Enums;

namespace Tinvent.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(250)]
        public string FirstName { get; private set; }
        [Required]
        [MaxLength(250)]
        public string LastName { get; private set; }

        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; private set; }

        [MaxLength(50)]
        [Phone]
        public string PhoneNumber { get; private set; }

        [Required]
        public string PasswordHash { get; private set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public UserRole Role { get; private set; }

        [Required]
        public bool IsActive { get; private set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastLoginDate { get; private set; }

        // Foreign Key
        public Guid TenantId { get; private set; }

        // Navigation Property
        public virtual Tenant Tenant { get; private set; }

        private User() { }

        public User(Guid tenantId, string firstName, string lastName, string email, string phone, string passwordHash, UserRole role)
        {
            TenantId = tenantId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phone;
            PasswordHash = passwordHash;
            Role = role;
            IsActive = true;
        }

        public void UpdateProfile(string firstName, string lastName, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName; 
            Email = email;
            PhoneNumber = phone;
            SetUpdated();
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            SetUpdated();
        }

        public void UpdateLoginTimestamp()
        {
            LastLoginDate = DateTime.UtcNow;
            SetUpdated();
        }

        public void Deactivate()
        {
            IsActive = false;
            SetUpdated();
        }
    }
}