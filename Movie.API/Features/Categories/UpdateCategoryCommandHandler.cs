using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Categories
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly MovieDbContext _dbContext;
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, MovieDbContext dbContext)
        {
            _categoryRepository = categoryRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);
            if (category is null)
            {
                return await Task.FromResult(new UpdateCategoryResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy thể loại cần cập nhật",
                });
            }
            var categoryName = await _dbContext.Categories.AsNoTracking().SingleOrDefaultAsync(x => x.Name == request.Name);
            if (categoryName?.Name != category?.Name && categoryName != null)
            {
                return await Task.FromResult(new UpdateCategoryResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Thể loại đã tồn tại",
                });
            }
            CustomMapper.Mapper.Map<UpdateCategoryCommand, Category>(request, category!);
            category!.LastModifiedDate = DateTime.UtcNow;
            _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveAsync();
            return await Task.FromResult(new UpdateCategoryResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Cập nhật thể loại thành công",
                Category = CustomMapper.Mapper.Map<CategoryDTO>(category)
            });
        }
    }
}
