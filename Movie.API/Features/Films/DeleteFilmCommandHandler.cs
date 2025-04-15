using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Features.Countries;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Films
{
    public class DeleteFilmCommandHandler : IRequestHandler<DeleteFilmCommand, Response>
    {
        private readonly IFilmRepository _filmRepository;
        private readonly MovieDbContext _dbContext;
        public DeleteFilmCommandHandler(IFilmRepository FilmRepository, MovieDbContext dbContext)
        {
            _filmRepository = FilmRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(DeleteFilmCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return await Task.FromResult(new DeleteFilmResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy phim cần xóa"
                });
            }
            var film = await _dbContext.Films.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (film is null) return new DeleteFilmResponse()
            {
                Success = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = "Xóa phim không thành công",
                Film = new FilmDTO()
            };
            await _filmRepository.DeleteAsync(request.Id);
            await _filmRepository.SaveAsync();

            var filmCategories = _dbContext.FilmCategories.Where(fc => fc.FilmId == film.Id).ToList();
            _dbContext.FilmCategories.RemoveRange(filmCategories);
            _dbContext.SaveChanges();
            return await Task.FromResult(new DeleteFilmResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Xóa phim thành công",
                Film = CustomMapper.Mapper.Map<FilmDTO>(film)
            });

        }
    }
}
