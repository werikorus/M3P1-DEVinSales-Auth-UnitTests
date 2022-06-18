using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevInSales.Enums;
using DevInSales.Repositories;
using DevInSales.Context;

namespace DevInSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccessController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        [Route("listar")]
        [Authorize]
        [HttpGet]
        public IActionResult List()
            => User.IsInRole(Permitions.Funcionario.GetDisplayName())
            ? Ok(_userRepository.GetAllUsers().Select(x => new { x.UserName, x.DescricaoPermissao }))
            : Ok(_userRepository.GetAllUsers());

        [HttpGet]
        [Route("publico")]
        [AllowAnonymous]
        public IActionResult AccessPublic()
        {
            try
            {
                return Ok("Acesso público de todos os colaboradores");
            }
            catch
            {
                return StatusCode(500);
            }
            
        }

        [HttpGet]
        [Route("funcionario")]
        [Authorize(Roles = "gerente,funcionario")]       
        public IActionResult AccessEmployee()
        {
            try
            {
                return Ok($"Bem-vindo {User.Identity.Name}, à página de funcionários");
            }
            catch
            {
                return StatusCode(500);
            }           
        }

        [HttpGet]
        [Route("gerente")]
        [Authorize(Roles = "gerentes")]
        public IActionResult AccessManager()
        {
            try
            {
                return Ok("Acesso exclusivo à gerentes");
            }
            catch
            {
                return StatusCode(500);
            }            
        }

        [HttpGet]
        [Route("administrador")]
        [Authorize(Roles = "gerente,funcionario,administrador")]
        public IActionResult AccessAdministrator()
        {
            try
            {
                return Ok("Acesso exclusivo à administradores");
            }
            catch
            {
                return StatusCode(500);
            }            
        }

    }
}
