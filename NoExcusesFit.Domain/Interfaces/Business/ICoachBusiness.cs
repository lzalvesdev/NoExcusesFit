using NoExcusesFit.Domain.DTOs.Coach;

namespace NoExcusesFit.Domain.Interfaces.Business;

public interface ICoachBusiness
{
    Task<CoachCreatedResponseDto> AddAsync(CreateCoachRequestDto coach);
    Task AddSpecialityAsync(Guid id, int specialityId);
    Task<CoachResponseDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<CoachResponseDto>> GetAllAsync(int page, int pageSize);
    Task DeleteAsync(Guid id);
}