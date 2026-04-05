using NoExcusesFit.Domain.DTOs.Athlete;
using NoExcusesFit.Domain.Entities;

namespace NoExcusesFit.Domain.Interfaces.Business
{
    public interface IAthleteBusiness
    {  
        Task<AthleteResponseDto> AddAsync(CreateAthleteRequestDto request);
        Task<AthleteResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<AthleteResponseDto>> GetAllAsync(int page, int pageSize);
        Task UpdateAsync(Guid id, UpdateAthleteRequestDto request);
        Task DeleteAsync(Guid id);
    }
}
