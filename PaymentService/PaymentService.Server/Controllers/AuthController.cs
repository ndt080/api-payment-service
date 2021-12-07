using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Domain.AuthUtils;
using PaymentService.Domain.Models;
using PaymentService.Domain.Services;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PaymentService.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISubscribeRegisterService _registerService;

        public AuthController(IUserService userService, ISubscribeRegisterService registerService)
        {
            _userService = userService;
            _registerService = registerService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequest user)
        {
            _userService.Register(new User
            {
                Email = user.Email,
                PasswordHash = BCryptNet.HashPassword(user.Password),
                RefreshTokens = new List<RefreshToken>()
            },IpAddress());
            
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(UserRequest model)
        {
            var response = _userService.Login(model, IpAddress());
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _userService.RefreshToken(refreshToken, IpAddress());
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(RevokeTokenRequest model)
        {
            // accept refresh token in request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            _userService.RevokeToken(token, IpAddress());
            return Ok(new { message = "Token revoked" });
        }

        // helper methods

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
    }
}