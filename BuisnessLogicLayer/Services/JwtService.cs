

using BuisnessLogicLayer.IServiceContracts;
using DataAccessLayer.DTO;
using DataAccessLayer.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;

namespace BuisnessLogicLayer.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthonticationResponse CreateJwtToken(ApplicationUser applicationUser)
        {
            DateTime expiration = DateTime.UtcNow.
                AddMinutes(Convert.ToDouble(_configuration["Jwt:Expiration_Minutes"]));

            Claim[] claims = new Claim[]
            {
                //subject user id
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString()),

                //jwt unique id
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                //Date and Time of Token Genration
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                //Unique Name Identifier of the user (email)
                new Claim(ClaimTypes.NameIdentifier, applicationUser.Email!),
                //Name  of the user (PersonName)
                new Claim(ClaimTypes.Name, applicationUser.PersonName!),
            };

            SymmetricSecurityKey securityKey = new
                SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            SigningCredentials signingCredentials = 
                new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator =
                new JwtSecurityToken
                (
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    null,
                    expiration,
                    signingCredentials
                );
            JwtSecurityTokenHandler tokenHandler =
                new JwtSecurityTokenHandler();

            string token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthonticationResponse
            {
                Token = token,
                PersonName = applicationUser.PersonName!,
                Email = applicationUser.Email!,
                Expiration = expiration,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpirationDateTime = DateTime.UtcNow
                .AddMinutes(Convert.ToInt32(_configuration["RefreshToken:Expiration_Minutes"]))
                
            };
        }


        //Create Refresh token (base 64 string of random numbers)
        public string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];
            RandomNumberGenerator randomNumber =  RandomNumberGenerator.Create();
            randomNumber.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
