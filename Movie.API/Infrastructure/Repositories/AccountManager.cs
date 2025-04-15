using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movie.API.AutoMapper;
using Movie.API.Infrastructure.Data;
using Movie.API.Models.Domain.Entities;
using Movie.API.Requests;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;
using Movie.API.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Movie.API.Infrastructure.Repositories
{
    public class AccountManager
    {
        private readonly MovieDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountManager> _logger;
        private readonly ISendEmail _sendEmail;
        public AccountManager(
            MovieDbContext dbContext,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration,
            ILogger<AccountManager> logger,
            ISendEmail sendEmail)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
            _sendEmail = sendEmail;
        }
        public async Task<Response> LoginAsync(LoginRequest model, string scheme, HostString host)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRole = await _dbContext.UserRoles.SingleOrDefaultAsync(x => x.UserId == user.Id);
                var role = userRole?.RoleId is not null ? await _roleManager.FindByIdAsync(userRole.RoleId) : null;

                var claims = new Claim[]
                {
                    new Claim("UserName", model.Username),
                    new Claim("UserId", user.Id),
                    new Claim(ClaimTypes.Role, role?.Name ?? string.Empty),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim("Avatar", user.Avatar ?? string.Empty),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var token = GenerateToken(claims);
                var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);

                var userdto = CustomMapper.Mapper.Map<UserDTO>(user);
                userdto.RoleName = role?.Name ?? string.Empty;
                userdto.AvatarUrl = $"{scheme}://{host}/{user.AvatarUrl}";
                return new LoginRespone()
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Đăng nhập thành công",
                    AccessToken = jwttoken,
                    Expiration = token.ValidTo,
                    RefreshToken = this.GenerateRefreshToken(),
                    User = userdto,
                };
            }
            else
            {
                return new Response()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Tên tài khoản hoặc mật khẩu không đúng",
                };
            }
        }
        public async Task<Response> RegisterAsync(RegisterRequest model)
        {
            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                return await Task.FromResult(new RegisterResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Tên tài khoản đã được đăng ký"
                });
            }
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return await Task.FromResult(new RegisterResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Email này đã được đăng ký"
                });
            }
            var user = CustomMapper.Mapper.Map<User>(model);
            await _userManager.CreateAsync(user);
            await _userManager.AddPasswordAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "Customer");
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new RegisterResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Đăng ký thành công",
            });

        }
        public async Task<Response> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == default)
            {
                return new Response()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Tài khoản chưa được đăng ký."
                };
            }
            var newPassword = Cryptography.GetRandomString();
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, newPassword);
            await _dbContext.SaveChangesAsync();
            string subject = "Yêu cầu đặt lại mật khẩu.";
            string body = $"Xin chào {user.DisplayName}, yêu cầu đặt lại mật khẩu của bạn đã được thực hiện. " +
                $"Chúng tôi cung cấp cho bạn mật khẩu mới là {newPassword}, bạn có thể đổi mật khẩu tại trang cá nhân. " +
                $"Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi";
            await _sendEmail.SendEmailAsync(email, subject, body);
            return new Response()
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Mật khẩu mới đã được gửi tới email của bạn."
            };
        }
        public async Task<Response> RefreshToken(RefreshTokenRequest model)
        {
            _logger.LogInformation("Refresh Token called");
            var principal = GetPrincipalFromExpriedToken(model.AccessToken);
            if (principal?.Identity?.Name is null)
            {
                return new RefreshTokenRespone()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Không có quyền"
                };
            }
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user is null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                return new RefreshTokenRespone()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Không có quyền"
                };
            }
            var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, principal.Identity.Name),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.MobilePhone, "0233294832"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
            var token = GenerateToken(claims);
            _logger.LogInformation("Refresh Succeeded");
            var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
            return new LoginRespone()
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Tạo mới thành công",
                AccessToken = jwttoken,
                Expiration = token.ValidTo,
                RefreshToken = model.RefreshToken,
            };

        }
        public async Task<Response> Revoke(string username)
        {
            if (username is null)
            {
                return new Response()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Không có quyền"
                };
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return new Response()
                {
                    Success = false,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Không có quyền"
                };
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            _logger.LogInformation("Revoke Succeeded");
            return new Response()
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Thu hồi thành công"
            };
        }
        public JwtSecurityToken GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWT:Secret")!));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return token;
        }
        private ClaimsPrincipal? GetPrincipalFromExpriedToken(string? token)
        {
            var key = _configuration["JWT:Secret"]!;
            var validation = new TokenValidationParameters
            {
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidAudience = _configuration["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                ValidateLifetime = false
            };
            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }

    }
}
