using Demo.Api.ViewModel;
using Demo.Services.Interfaces;
using Demo.Services.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Demo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthenticationService _authenticationService;
        private readonly JwtSettings _jwtSettings;
        public AuthController(ILogger<AuthController> logger,
            IAuthenticationService authenticationService, IOptions<JwtSettings> jwtSettings)
        {
            _logger = logger;
            _authenticationService = authenticationService;
            _jwtSettings = jwtSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            try
            {
                // Dummy task
                await Task.Delay(0);

                if (user == null)
                {
                    return BadRequest("Invalid login request");
                }

                var jwtTokenClaims = new JwtTokenClaims();

                if (user.UserName == _jwtSettings.SharedUserName && user.Password == _jwtSettings.SharedPassword)
                {
                    jwtTokenClaims.UserName = _jwtSettings.SharedUserName;
                    jwtTokenClaims.isShared = "1";
                    jwtTokenClaims.UserId = "00000";

                    return Ok(new
                    {
                        Token = _authenticationService.GetToken(jwtTokenClaims)
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, "Error login: {0}", user.UserName);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
