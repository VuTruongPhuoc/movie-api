using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Movie.API.AutoMapper;
using Movie.API.Features.Users;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Requests;
using Movie.API.Requests.Pagination;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Movie.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly IHostEnvironment _environment;
        private readonly UserManager<User> _userManager;

        public UserController(
            ILogger<UserController> logger,
            IMediator mediator,
            IUserRepository userRepository,
            UserManager<User> userManager,
            IWebHostEnvironment hostEnvironment)
            : base(hostEnvironment)
        {
            _logger = logger;
            _mediator = mediator;
            _userRepository = userRepository;
            _environment = hostEnvironment;
            _userManager = userManager;

        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            var query = new GetUserQuery { UserName = username };
            var response = await _mediator.Send(query);
            response.User.AvatarUrl = $"{Request.Scheme}://{Request.Host}/Content/Images/{response.User.Avatar}";
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("all")]
        public async Task<Response> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetUsersQuery()
            {
                Pagination = new Pagination()
                {
                    pageNumber = pageNumber,
                    pageSize = pageSize
                }
            };

            var usersResponse = await _mediator.Send(query);

            foreach (var user in usersResponse.Data.Items)
            {
                user.AvatarUrl = $"{Request.Scheme}://{Request.Host}/Content/Images/{user.Avatar}";
            }

            return new DataRespone
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = usersResponse.Data
            };
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddUserRequest model)
        {
            var command = new AddUserCommand();
            CustomMapper.Mapper.Map<AddUserRequest, AddUserCommand>(model, command);
            var response = await _mediator.Send(command);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("update/{username}")]
        public async Task<IActionResult> Update(string username, [FromBody] UpdateUserRequest model)
        {
            var command = new UpdateUserCommand() { UserName = username };
            CustomMapper.Mapper.Map<UpdateUserRequest, UpdateUserCommand>(model, command);

            var response = await _mediator.Send(command);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("delete/{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            var command = new DeleteUserCommand() { UserName = username };
            var response = await _mediator.Send(command);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("changerole/{username}/{role}")]
        public async Task<IActionResult> ChangeRole(string username, string role)
        {
            var command = new ChangeRoleCommand()
            {
                UserName = username,
                RoleName = role
            };
            var response = await _mediator.Send(command);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("changeavatar/{username}")]
        public async Task<IActionResult> ChangeAvatar(string username, [FromForm] ChangeImageRequest model)
        {
            if (username != model.UserName)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = string.Empty
                });
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
            {
                return NotFound(new Response()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy",
                });
            }
            if (model.AvatarFile != null)
            {
                if (!string.IsNullOrEmpty(user?.Avatar))
                {
                    DeleteImage(user.Avatar);
                }
                user!.Avatar = await SaveImage(model.AvatarFile);
            }
            CustomMapper.Mapper.Map<ChangeImageRequest, User>(model, user);
            user.AvatarUrl = Path.Combine("Content\\Images", user.Avatar ?? string.Empty);
            _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();

            var dto = CustomMapper.Mapper.Map<UserAvatar>(user);
            return Ok(new UserAvatarResponse
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Thành công",
                User = dto
            });
        }
    }
}
