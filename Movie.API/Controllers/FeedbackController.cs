using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.API.AutoMapper;
using Movie.API.Features.Feedbacks;
using Movie.API.Requests.Pagination;
using Movie.API.Requests;
using System.Net;
using System.Security.Claims;
using Movie.API.Responses;

namespace Movie.API.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FeedbackController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FeedbackController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("all")]
        public async Task<Response> GetCommnets(int commentId)
        {
            var query = new GetFeedbacksQuery()
            {
                CommentId = commentId,
            };
            return await _mediator.Send(query);

        }
        [HttpPost("add")]
        public async Task<IActionResult> AddFeedback([FromBody] AddFeedbackRequest model)
        {
            string userid = HttpContext.User.FindFirstValue("UserId")!;
            var command = new AddFeedbackCommand() { UserId = userid };
            CustomMapper.Mapper.Map<AddFeedbackRequest, AddFeedbackCommand>(model, command);

            var response = await _mediator.Send(command);
            response.Feedback.User.AvatarUrl = $"{Request.Scheme}://{Request.Host}/Content/Images/{response.Feedback.User.Avatar}";
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, [FromBody] UpdateFeedbackRequest model)
        {
            var command = new UpdateFeedbackCommand() { Id = id };
            CustomMapper.Mapper.Map<UpdateFeedbackRequest, UpdateFeedbackCommand>(model, command);
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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var command = new DeleteFeedbackCommand() { Id = id };
            var response = await _mediator.Send(command);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
