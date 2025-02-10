using FirstApi.Common;
using FirstApi.Context;
using FirstApi.Entities;
using FirstApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstApi.Services.Implementation
{
    public class Authentication : IAuthentication
    {

        private readonly IConfiguration _configuration;
        //baraye dastresi be appsettings
        private readonly CityContext _context;

        public Authentication(IConfiguration configuration ,CityContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public CityUser? Validation(string username, string password)
        {
            string passToHash = PasswordHelper.EncodePasswordMd5(password);
            var user = _context.CityUser.FirstOrDefault(x => x.UserName == username && x.Password == passToHash);
            return user;
        }
        public string GenerateToken(CityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes
                (_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256
                );
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("userId", user.Id.ToString()));
            claimsForToken.Add(new Claim("NameKarbari", user.Name.ToString()));

            var jwtSecurityToke = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                //modat zaman motabar boodan token
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToke);
            return tokenToReturn;
        }
    }
}
