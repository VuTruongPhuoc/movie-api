using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.API.AutoMapper;
using Movie.API.Features.Histories;
using Movie.API.Requests;
using Movie.API.Responses;
using Movie.API.Requests.Pagination;
using System.Security.Claims;

namespace Movie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class HistoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("all")]
        public async Task<Response> GetHistories(int pageNumber = 1, int pageSize = 10)
        {
            string userid = HttpContext.User.FindFirstValue("UserId")!;
            var query = new GetHistoriesQuery()
            {
                UserId = userid,
                Pagination = new Pagination()
                {
                    pageNumber = pageNumber,
                    pageSize = pageSize
                }
            };
            query.UserId = userid;
            return await _mediator.Send(query);
        }
        [HttpPost("add")]
        public async Task<Response> AddHistory([FromBody] AddHistoryRequest model)
        {
            string userid = HttpContext.User.FindFirstValue("UserId")!;
            var command = new AddHistoryCommand();
            command.UserId = userid;
            CustomMapper.Mapper.Map<AddHistoryRequest, AddHistoryCommand>(model, command);
            return await _mediator.Send(command);
        }
        [HttpDelete("delete/{id}")]
        public async Task<Response> DeleteHistory(int id)
        {
            var command = new DeleteHistoryCommand();
            command.Id = id;
            return await _mediator.Send(command);
        }
    }
}
