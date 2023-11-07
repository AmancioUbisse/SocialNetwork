using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models.Domain;
using SocialNetwork.API.Services.Interfaces;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await authService.AuthenticateAsync(model.Email, model.Password);

            if (user == null )
            {
                return Unauthorized("Tentativa de login inválida.");
            }

            // Se chegou aqui, o login foi bem-sucedido
            return Ok("Login bem-sucedido");
        }
    }
}