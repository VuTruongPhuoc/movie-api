using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Sections
{
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Response>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly MovieDbContext _dbContext;
        public UpdateSectionCommandHandler(ISectionRepository SectionRepository, MovieDbContext dbContext)
        {
            _sectionRepository = SectionRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await _dbContext.Sections.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);
            if (section is null)
            {
                return await Task.FromResult(new UpdateSectionResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy phần cần cập nhật",
                });
            }
            var sectionName = await _dbContext.Sections.AsNoTracking().SingleOrDefaultAsync(x => x.Name == request.Name);
            if (sectionName?.Name != section?.Name && sectionName != null)
            {
                return await Task.FromResult(new UpdateSectionResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Phần đã tồn tại",
                });
            }
            CustomMapper.Mapper.Map<UpdateSectionCommand, Section>(request, section!);
            section!.LastModifiedDate = DateTime.UtcNow;
            _sectionRepository.UpdateAsync(section);
            await _sectionRepository.SaveAsync();
            return await Task.FromResult(new UpdateSectionResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Cập nhật phần thành công",
                Section = CustomMapper.Mapper.Map<SectionDTO>(section)
            });
        }
    }
}
