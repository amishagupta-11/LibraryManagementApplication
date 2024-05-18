namespace LibraryManagementApplication.Models
{
    /// <summary>
    /// Represents the settings used for JWT token generation and validation.
    /// </summary>
    public class AuthorizationSettings
    {
        public required string  SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
