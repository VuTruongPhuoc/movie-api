using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Features.Feedbacks;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Feedbacks
{
    public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand, Response>
    {
        private readonly IFeedbackRepository _FeedbackRepository;
        private readonly MovieDbContext _dbContext;
        public UpdateFeedbackCommandHandler(IFeedbackRepository FeedbackRepository, MovieDbContext dbContext)
        {
            _FeedbackRepository = FeedbackRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _dbContext.Feedbacks.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);
            if (feedback is null)
            {
                return await Task.FromResult(new UpdateFeedbackResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy ",
                });
            }
            CustomMapper.Mapper.Map<UpdateFeedbackCommand, Feedback>(request, feedback);
            feedback.LastModifiedDate = DateTime.UtcNow;
            _FeedbackRepository.UpdateAsync(feedback);
            await _FeedbackRepository.SaveAsync();
            return await Task.FromResult(new UpdateFeedbackResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Cập nhật bình luận thành công",
                Feedback = CustomMapper.Mapper.Map<FeedbackDTO>(feedback)
            });
        }
    }
}
