using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.API.AutoMapper;
using Movie.API.Infrastructure.Data;
using Movie.API.Infrastructure.Repositories;
using Movie.API.Responses;
using Movie.API.Responses.DTOs;

namespace Movie.API.Features.Countries
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Response>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly MovieDbContext _dbContext;
        public DeleteCountryCommandHandler(ICountryRepository countryRepository, MovieDbContext dbContext)
        {
            _countryRepository = countryRepository;
            _dbContext = dbContext;
        }
        public async Task<Response> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return await Task.FromResult(new DeleteCountryResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Không tìm thấy country cần xóa"
                });
            }
            var country = await _dbContext.Countries.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);
            await _countryRepository.DeleteAsync(request.Id);
            await _countryRepository.SaveAsync();
            return await Task.FromResult(new DeleteCountryResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Xóa country thành công",
                Country = CustomMapper.Mapper.Map<CountryDTO>(country)
            });

        }
    }
}
