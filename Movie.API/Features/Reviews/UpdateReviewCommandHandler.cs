using MediatR;
using Movie.API.AutoMapper;
using Movie.API.Features.Reviews;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Responses.DTOs;
using Movie.API.Responses;
using Microsoft.EntityFrameworkCore;
using Movie.API.Models.Domain.Entities;

namespace Movie.API.Features.Reviews
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Response>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly MovieDbContext _dbContext;
        public UpdateReviewCommandHandler(IReviewRepository ReviewRepository, MovieDbContext dbContext)
        {
            _reviewRepository = ReviewRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _dbContext.Reviews.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);
            if (review is null)
            {
                return await Task.FromResult(new UpdateReviewResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy đánh giá cần cập nhật",
                });
            }
            CustomMapper.Mapper.Map<UpdateReviewCommand, Review>(request, review);
            review.LastModifiedDate = DateTime.UtcNow;
            _reviewRepository.UpdateAsync(review);
            await _reviewRepository.SaveAsync();

            return await Task.FromResult(new UpdateReviewResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Cập nhật đánh giá thành công",
                Review = CustomMapper.Mapper.Map<ReviewDTO>(review)
            });
        }
    }
}
