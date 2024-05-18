using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApplication.Models
{
    /// <summary>
    /// represents user details
    /// </summary>
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
