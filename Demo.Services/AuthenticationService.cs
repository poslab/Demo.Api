using Demo.Services.Interfaces;
using Demo.Services.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;
        public AuthenticationService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Generate JWT Token 
        /// </summary>
        /// <param name="jwtTokenClaims"></param>
        /// <returns></returns>
        public string GetToken(JwtTokenClaims jwtTokenClaims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim("IsShared", jwtTokenClaims.isShared),
                new Claim(ClaimTypes.Name, jwtTokenClaims.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, jwtTokenClaims.UserId),
            };

            var tokeOptions = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.Expires),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
    }
}
