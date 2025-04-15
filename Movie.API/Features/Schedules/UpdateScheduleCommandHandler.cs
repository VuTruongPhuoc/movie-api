using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Schedules
{
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, Response>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly MovieDbContext _dbContext;
        public UpdateScheduleCommandHandler(IScheduleRepository scheduleRepository, MovieDbContext dbContext)
        {
            _scheduleRepository = scheduleRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (schedule is null)
            {
                return await Task.FromResult(new UpdateScheduleResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy lịch cần cập nhật",
                });
            }
            var scheduleName = await _dbContext.Schedules.AsNoTracking().SingleOrDefaultAsync(x => x.Name == request.Name);
            if (scheduleName?.Name != schedule?.Name && scheduleName != null)
            {
                return await Task.FromResult(new UpdateScheduleResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Lịch đã tồn tại",
                });
            }
            CustomMapper.Mapper.Map<UpdateScheduleCommand, Schedule>(request, schedule!);
            schedule!.LastModifiedDate = DateTime.UtcNow;
            await _scheduleRepository.SaveAsync();
            return await Task.FromResult(new UpdateScheduleResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Cập nhật lịch thành công",
                Schedule = CustomMapper.Mapper.Map<ScheduleDTO>(schedule)
            });
        }
    }
}
