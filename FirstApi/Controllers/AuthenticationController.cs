using FirstApi.Models;
using FirstApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _auten;

        public AuthenticationController(IAuthentication auten)
        {
            _auten = auten;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequest req)
        {
            var user = _auten.Validation(req.UserName, req.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            //download package IdentityModel.Token && Aspnetcore.JWTBearer
            var tokenToReturn = _auten.GenerateToken(user);
            return Ok(tokenToReturn);
        }

    }
}
