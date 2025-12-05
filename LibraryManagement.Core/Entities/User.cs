using Library_Management_System.LibraryManagement.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.LibraryManagement.Core.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        public Role Role { get; set; } = Role.Member;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<BorrowRecord> BorrowRecords { get; set; } = new();
    }
}
