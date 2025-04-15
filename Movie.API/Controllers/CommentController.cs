using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.API.AutoMapper;
using Movie.API.Features.Categories;
using Movie.API.Features.Comments;
using Movie.API.Requests;
using Movie.API.Requests.Pagination;
using Movie.API.Responses;
using System.Net;
using System.Security.Claims;

namespace Movie.API.Controllers
{
    [Route("api/comment")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("all")]
        public async Task<DataRespone> GetComments(int filmid)
        {

            var query = new GetCommentsQuery()
            {
                FilmId = filmid,
            };
            var commentsResponse = await _mediator.Send(query);
            foreach (var data in commentsResponse.Data)
            {
                data.User.AvatarUrl = $"{Request.Scheme}://{Request.Host}/Content/Images/{data.User.Avatar}";
                foreach (var item in data.Feedbacks)
                {
                    item.User.AvatarUrl = $"{Request.Scheme}://{Request.Host}/Content/Images/{item.User.Avatar}";
                }
            }
            return new DataRespone
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = commentsResponse.Data
            };

        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("add")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentRequest model)
        {
            string userid = HttpContext.User.FindFirstValue("UserId")!;
            var command = new AddCommentCommand() { UserId = userid };
            CustomMapper.Mapper.Map<AddCommentRequest, AddCommentCommand>(model, command);

            var response = await _mediator.Send(command);
            response.Comment.User.AvatarUrl = $"{Request.Scheme}://{Request.Host}/Content/Images/{response.Comment.User.Avatar}";
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentRequest model)
        {
            var command = new UpdateCommentCommand() { Id = id };
            CustomMapper.Mapper.Map<UpdateCommentRequest, UpdateCommentCommand>(model, command);
            var response = await _mediator.Send(command);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var command = new DeleteCommentCommand() { Id = id };
            var response = await _mediator.Send(command);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
