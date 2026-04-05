using NoExcusesFit.Domain.DTOs.Coach;
using NoExcusesFit.Domain.Entities;
using NoExcusesFit.Domain.QueryResults;

namespace NoExcusesFit.Domain.Interfaces.Repositories;

public interface ICoachRepository
{
    Task AddAsync(Coach coach);
    Task<CoachQueryResult?> GetByIdAsync(Guid id);
    Task<List<CoachQueryResult>> GetAllAsync(int skip, int take);
    Task DeleteAsync(Guid id);
}