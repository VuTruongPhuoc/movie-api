using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Features.Comments;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Comments
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response>
    {
        private readonly ICommentRepository _CommentRepository;
        private readonly MovieDbContext _dbContext;
        public UpdateCommentCommandHandler(ICommentRepository CommentRepository, MovieDbContext dbContext)
        {
            _CommentRepository = CommentRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _dbContext.Comments.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);
            if (comment is null)
            {
                return await Task.FromResult(new UpdateCommentResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy ",
                });
            }
            CustomMapper.Mapper.Map<UpdateCommentCommand, Comment>(request, comment);
            comment.LastModifiedDate = DateTime.UtcNow;
            _CommentRepository.UpdateAsync(comment);
            await _CommentRepository.SaveAsync();
            return await Task.FromResult(new UpdateCommentResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Cập nhật bình luận thành công",
                Comment = CustomMapper.Mapper.Map<CommentDTO>(comment)
            });
        }
    }
}
