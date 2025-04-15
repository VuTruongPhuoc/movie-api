using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.API.AutoMapper;
using Movie.API.Features.Tracks;
using Movie.API.Requests;
using Movie.API.Responses;
using Movie.API.Requests.Pagination;
using System.Security.Claims;

namespace Movie.API.Controllers
{
    [Route("api/track")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TrackController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TrackController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("all")]
        public async Task<Response> GetTracks(int pageNumber = 1, int pageSize = 10)
        {
            string userid = HttpContext.User.FindFirstValue("UserId")!;
            var query = new GetTracksQuery()
            {
                UserId = userid,
                Pagination = new Pagination()
                {
                    pageSize = pageSize,
                    pageNumber = pageNumber
                }
            };
            return await _mediator.Send(query);
        }
        [HttpPost("add")]
        public async Task<Response> AddTrack([FromBody] AddTrackRequest model)
        {
            string userid = HttpContext.User.FindFirstValue("UserId")!;
            var command = new AddTrackCommand();
            command.UserId = userid;
            CustomMapper.Mapper.Map<AddTrackRequest, AddTrackCommand>(model, command);
            return await _mediator.Send(command);
        }
        [HttpDelete("delete/{id}")]
        public async Task<Response> DeleteTrack(int id)
        {
            var command = new DeleteTrackCommand();
            command.Id = id;
            return await _mediator.Send(command);
        }
    }
}
