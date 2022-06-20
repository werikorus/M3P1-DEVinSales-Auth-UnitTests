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
        private const string PublicAccessMsg = "Acesso público de todos os colaboradores";
        private const string ManagersAccessMsg = "Acesso exclusivo à gerentes";
        private const string AdministratorsAccessMsg = "Acesso exclusivo à administradores";
        private const string EmployeesAccessMsg = "Bem-vindo à página de funcionários!";

        private readonly UserRepository _userRepository;

        [Route("listar")]
        [Authorize]
        [HttpGet]
        public IActionResult List()
            => User.IsInRole(Permitions.Funcionario.GetDisplayName())
            ? Ok(_userRepository.GetAllUsers().Select(x => new { x.UserName, x.Role }))
            : Ok(_userRepository.GetAllUsers());

        [HttpGet]
        [Route("publico")]
        [AllowAnonymous]
        public async Task<ActionResult> AccessPublic()
        {
            return await Task.FromResult(Ok(PublicAccessMsg));
        }

        [HttpGet]
        [Route("funcionario")]
        [Authorize(Roles = "gerente,funcionario")]
        public async Task<ActionResult> AccessEmployee()
        {
            return await Task.FromResult(Ok(EmployeesAccessMsg));
        }

        [HttpGet]
        [Route("gerente")]
        [Authorize(Roles = "gerentes")]
        public async Task<ActionResult> AccessManager()
        {
            return await Task.FromResult(Ok(ManagersAccessMsg));
        }

        [HttpGet]
        [Route("administrador")]
        [Authorize(Roles = "gerente,funcionario,administrador")]
        public async Task<ActionResult> AccessAdministrator()
        {
            return await Task.FromResult(Ok(AdministratorsAccessMsg));
        }
    }
}
