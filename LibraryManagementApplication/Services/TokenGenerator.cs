using LibraryManagementApplication.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementApplication.Services
{
    public class TokenGenerator : IJwtTokenGenerator
    {
        private readonly AuthorizationSettings _authSettings;

        public TokenGenerator(AuthorizationSettings jwtSettings)
        {
            _authSettings = jwtSettings;
        }

        /// <summary>
        /// Generates a JWT token for the given username and role.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns> The generated JWT token as a string.</returns>
       
        public string GenerateToken(string username, string role)
        {
            // Claims representing the user identity and role.
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username), 
                new Claim(ClaimTypes.Role, role) 
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecretKey));

            // Signing credentials using the key and HMAC-SHA256 algorithm.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Expiration time for the token.
            var expires = DateTime.Now.AddMinutes(_authSettings.ExpiryMinutes);

            // Create a new JWT token with the specified claims, audience, issuer, expiration, and signing credentials.
            var token = new JwtSecurityToken(
                _authSettings.Issuer, 
                _authSettings.Audience, 
                claims, 
                expires: expires, 
                signingCredentials: creds 
            );

            // Serialize the JWT token to a string.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
