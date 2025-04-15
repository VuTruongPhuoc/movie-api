using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Models.Domain.Entities;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Countries
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Response>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly MovieDbContext _dbContext;
        public UpdateCountryCommandHandler(ICountryRepository countryRepository, MovieDbContext dbContext)
        {
            _countryRepository = countryRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _dbContext.Countries.FindAsync(request.Id);
            if (country is null)
            {
                return await Task.FromResult(new UpdateCountryResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy quốc gia cần cập nhật",
                });
            }
            var countryName = await _dbContext.Countries.AsNoTracking().SingleOrDefaultAsync(x => x.Name == request.Name);
            if (countryName?.Name != country?.Name && countryName != null)
            {
                return await Task.FromResult(new UpdateCountryResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Quốc gia đã tồn tại",
                });
            }
            CustomMapper.Mapper.Map<UpdateCountryCommand, Country>(request, country!);
            country!.LastModifiedDate = DateTime.UtcNow;
            await _countryRepository.SaveAsync();
            return await Task.FromResult(new UpdateCountryResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Cập nhật quốc gia thành công",
                Country = CustomMapper.Mapper.Map<CountryDTO>(country)
            });
        }
    }
}
