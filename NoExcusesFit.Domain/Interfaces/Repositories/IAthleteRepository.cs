using NoExcusesFit.Domain.Entities;

namespace NoExcusesFit.Domain.Interfaces.Repositories
{
    public interface IAthleteRepository
    {
        Task AddAsync(Athlete athlete);
        Task<Athlete?> GetByIdAsync(Guid id);
        Task<List<Athlete>> GetAllAsync(int skip, int take);
        Task UpdateAsync(Athlete athlete);
        Task DeleteAsync(Guid id);
    }
}
