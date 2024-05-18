namespace LibraryManagementApplication.Models
{
    /// <summary>
    /// represents register details of the user
    /// </summary>
    public class RegisterDetails
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? AdminKey { get; set; }
    }
}
