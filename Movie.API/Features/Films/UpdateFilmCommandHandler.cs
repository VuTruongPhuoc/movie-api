using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Features.Films;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;
using System;

namespace Movie.API.Features.Films
{
    public class UpdateFilmCommandHandler : IRequestHandler<UpdateFilmCommand, Response>
    {
        private readonly IFilmRepository _filmRepository;
        private readonly MovieDbContext _dbContext;
        public UpdateFilmCommandHandler(IFilmRepository FilmRepository, MovieDbContext dbContext)
        {
            _filmRepository = FilmRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(UpdateFilmCommand request, CancellationToken cancellationToken)
        {
            var film = await _dbContext.Films.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);
            if (film is null)
            {
                return await Task.FromResult(new UpdateFilmResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy phim cần cập nhật",
                });
            }
            var filmName = await _dbContext.Films.AsNoTracking().SingleOrDefaultAsync(x => x.Name == request.Name);
            if (filmName?.Name != film?.Name && filmName != null)
            {
                return await Task.FromResult(new UpdateFilmResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Phim đã tồn tại",
                });
            }
            CustomMapper.Mapper.Map<UpdateFilmCommand, Film>(request, film!);
            film!.LastModifiedDate = DateTime.UtcNow;
            _filmRepository.UpdateAsync(film);
            await _filmRepository.SaveAsync();

            var filmCategories = _dbContext.FilmCategories
                                 .Where(fc => fc.FilmId == film.Id)
                                 .ToList();
            _dbContext.FilmCategories.RemoveRange(filmCategories);
            _dbContext.SaveChanges();

            foreach (var categoryId in request.CategoryIds)
            {
                await _dbContext.FilmCategories.AddAsync(new FilmCategory
                {
                    FilmId = film.Id,
                    CategoryId = categoryId,
                });
            }
            await _filmRepository.SaveAsync();
            return await Task.FromResult(new UpdateFilmResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Cập nhật phim thành công",
                Film = CustomMapper.Mapper.Map<FilmDTO>(film)
            });
        }
    }
}
