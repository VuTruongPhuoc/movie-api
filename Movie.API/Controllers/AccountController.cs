using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Requests;
using Movie.API.Responses;

namespace Movie.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly UserManager<User> _userManager;
        public readonly IConfiguration _configuration;
        private readonly AccountManager _accountManager;
        public AccountController(UserManager<User> userManager, IConfiguration configuration, AccountManager accountManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _accountManager = accountManager;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var loginResponse = await _accountManager.LoginAsync(model, Request.Scheme, Request.Host);

            if (!loginResponse.Success)
            {
                return Unauthorized(loginResponse);
            }
            return Ok(loginResponse);
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var registerResponse = await _accountManager.RegisterAsync(model);
            if (!registerResponse.Success)
            {
                return BadRequest(registerResponse);
            }
            return Ok(registerResponse);
        }
        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var response = await _accountManager.ForgotPassword(email);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("refreshtoken")]
        public async Task<Response> Refresh([FromBody] RefreshTokenRequest model)
        {
            return await _accountManager.RefreshToken(model);
        }
        [HttpDelete("revoketoken")]
        public async Task<Response> Revoke()
        {
            string username = HttpContext.User.Identity?.Name!;
            return await _accountManager.Revoke(username);
        }


    }
}
