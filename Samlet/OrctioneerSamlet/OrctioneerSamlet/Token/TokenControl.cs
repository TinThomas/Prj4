using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace OrctioneerSamlet.Token
{
    public class TokenControl
    {
        private IConfiguration _configuration;
            public TokenControl(IConfiguration configuration)
            {
                _configuration = configuration;
            }
    
            public async Task<string> GenerateToken(string userId)
            {
                Console.WriteLine("making token");
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId),
                    new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp,
                        new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
                };

                var token = new JwtSecurityToken(
                    issuer: "this app",
                    audience: "User of the app",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),SecurityAlgorithms.HmacSha256));
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

        }
    }
