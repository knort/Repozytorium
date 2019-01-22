using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SOR.BLL;
using SOR.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Api.Controllers
{
    [EnableCors(Constants.Constants.CORS_POLICY)]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody]AuthViewModel authViewModel)
        {
            var result = await _userService.LoginAsync(authViewModel);
            if (result.result)
            {
                return Ok(result.jwtViewModel);
            }
            return BadRequest(new { error = result.message });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody]JwtViewModel jwtViewModel)
        {
            var claims = Request.HttpContext.User.Identity.Name;
            try
            {
                var result = await _userService.RefreshTokenAsync(jwtViewModel);
                if (result != null)
                    return Ok(result);
                return NotFound(new { error = "Nie znaleziono takiego użytkownika" });
            }
            catch (SecurityTokenException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]AuthViewModel authViewModel)
        {
            try
            {
                var result = await _userService.CreateAsync(authViewModel);
                if (result.result)
                {
                    return Ok();
                }
                return BadRequest(new { error = result.message });
            }
            catch (Exception e)
            {
                return BadRequest(new { Password = "Hasło musi byc wypełnione" });
            }
        }
    }
}
