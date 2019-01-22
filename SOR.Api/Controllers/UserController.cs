using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.BLL;
using SOR.Model;
using SOR.Model.ViewModels;

namespace SOR.Api.Controllers
{
    [EnableCors(Constants.Constants.CORS_POLICY)]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Constants.Constants.USER_POLICY)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]AuthViewModel authViewModel)
        {
            var result = await _userService.CreateAsync(authViewModel);
            if (result.result)
            {
                _logger.LogInformation("Stworzono użytkownika");
                return Ok(new { result.message });
            }
            else
            {
                _logger.LogError("Nie udało się stworzyć użytkownika " + result.message);
                return BadRequest(new { result.message });
            }
        }
    }
}