using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Categories
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly MovieDbContext _dbContext;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, MovieDbContext dbContext)
        {
            _categoryRepository = categoryRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return await Task.FromResult(new DeleteCategoryResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy category cần xóa"
                });
            }
            var category = await _dbContext.Categories.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);
            await _categoryRepository.DeleteAsync(request.Id);
            await _categoryRepository.SaveAsync();
            return await Task.FromResult(new DeleteCategoryResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Xóa category thành công",
                Category = CustomMapper.Mapper.Map<CategoryDTO>(category)
            });

        }
    }
}
