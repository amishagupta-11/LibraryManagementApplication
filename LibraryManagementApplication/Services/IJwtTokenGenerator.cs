namespace LibraryManagementApplication.Services
{
    /// <summary>
    /// Represents a service responsible for generating JWT tokens.
    /// </summary>
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string username, string role);
    }
}
