using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Features.Episodes;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Episodes
{
    public class UpdateEpisodeCommandHandler : IRequestHandler<UpdateEpisodeCommand, Response>
    {
        private readonly IEpisodeRepository _episodeRepository;
        private readonly MovieDbContext _dbContext;
        public UpdateEpisodeCommandHandler(IEpisodeRepository EpisodeRepository, MovieDbContext dbContext)
        {
            _episodeRepository = EpisodeRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(UpdateEpisodeCommand request, CancellationToken cancellationToken)
        {
            var episode = await _dbContext.Episodes.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id && x.FilmId == request.FilmId);
            if (episode is null)
            {
                return await Task.FromResult(new UpdateEpisodeResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy tập cần cập nhật",
                });
            }

            CustomMapper.Mapper.Map<UpdateEpisodeCommand, Episode>(request, episode ?? new Episode());
            episode!.LastModifiedDate = DateTime.UtcNow;
            _episodeRepository.UpdateAsync(episode);
            await _episodeRepository.SaveAsync();
            return await Task.FromResult(new UpdateEpisodeResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Cập nhật tập thành công",
                Episode = CustomMapper.Mapper.Map<EpisodeDTO>(episode)
            });
        }
    }
}
