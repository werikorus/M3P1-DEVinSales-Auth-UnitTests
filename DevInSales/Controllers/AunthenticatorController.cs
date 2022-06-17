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
    public class AunthenticatorController : ControllerBase
    {
        private readonly SqlContext _context;

        public AunthenticatorController(SqlContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            try
            {
                var user = _context.User.Where(x => x.UserName.ToLower() == dto.Username.ToLower()
                    && x.Password == dto.Password).FirstOrDefault();

                if (user == null) return NotFound();

                var token = TokenService.GenerateBearerToken(user);
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
                var main = TokenService.GetMainFromExpiredToken(token);
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
