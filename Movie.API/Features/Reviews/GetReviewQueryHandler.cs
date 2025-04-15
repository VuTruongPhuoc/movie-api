using MediatR;
using Movie.API.AutoMapper;
using Movie.API.Features.Reviews;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Reviews
{
    public class GetReviewQueryHandler : IRequestHandler<GetReviewQuery, Response>
    {
        private IReviewRepository _reviewRepository;

        public GetReviewQueryHandler(IReviewRepository ReviewRepository)
        {
            _reviewRepository = ReviewRepository;
        }
        public async Task<Response> Handle(GetReviewQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return await Task.FromResult(new Response()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Yêu cầu trống",

                });
            }
            var review = await _reviewRepository.GetByFilmAsync(request.FilmId, request.UserId ?? string.Empty);
            if (review is null)
            {
                return await Task.FromResult(new Response()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy đánh giá"
                });
            }
            return await Task.FromResult(new DataRespone()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Thành công",
                Data = CustomMapper.Mapper.Map<ReviewDTO>(review)

            });
        }
    }
}
