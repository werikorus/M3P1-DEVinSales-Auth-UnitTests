using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevInSales.DTOs;
using DevInSales.Repositories;
using DevInSales.Services;
using DevInSales.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace DevInSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatorController : ControllerBase
    {
        private readonly SqlContext _context;

        public AuthenticatorController(SqlContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var user = _context.User.Where(x => x.UserName.ToLower() == dto.Username.ToLower()
                    && x.Password == dto.Password).FirstOrDefault();

                if (user == null) return NotFound() ;

                var token = TokenService.GenerateToken(user);
                var newRefreshToken = TokenService.GenerateRefreshToken();
                TokenService.SaveRefreshToken(user.UserName, newRefreshToken);

                return Ok(new
                {
                    token,
                    newRefreshToken
                });
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpPost]
        [Route("refresh")]
        [AllowAnonymous]
        public ActionResult<dynamic> RefreshToken([FromQuery] string token, [FromQuery] string refreshToken)
        {
            try
            {
                var main = TokenService.GetPrincipalFromExpiredToken(token);
                var username = main.Identity.Name;
                var savedRefreshToken = TokenService.GetRefreshToken(username);

                if (savedRefreshToken != refreshToken)
                    throw new SecurityTokenException("Invalid refresh token");

                var newToken = TokenService.GenerateBearerToken(main.Claims);
                var newRefreshToken = TokenService.GenerateRefreshToken();
                TokenService.DeleteRefreshToken(username, refreshToken);
                TokenService.SaveRefreshToken(username, newRefreshToken);

                return new ObjectResult(new
                {
                    token = newToken,
                    refreshToken = newRefreshToken
                });
            }
            catch
            {
                return StatusCode(500);
            }           
        }
    }
}
