using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Schedules
{
    public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, Response>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly MovieDbContext _dbContext;
        public DeleteScheduleCommandHandler(IScheduleRepository scheduleRepository, MovieDbContext dbContext)
        {
            _scheduleRepository = scheduleRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return await Task.FromResult(new DeleteScheduleResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy schedule cần xóa"
                });
            }
            var schedule = await _dbContext.Categories.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);
            await _scheduleRepository.DeleteAsync(request.Id);
            await _scheduleRepository.SaveAsync();
            return await Task.FromResult(new DeleteScheduleResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Xóa schedule thành công",
                Schedule = CustomMapper.Mapper.Map<ScheduleDTO>(schedule)
            });

        }
    }
}
