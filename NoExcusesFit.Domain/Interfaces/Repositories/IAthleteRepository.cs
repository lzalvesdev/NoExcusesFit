using NoExcusesFit.Domain.Entities;
using System.Data;

namespace NoExcusesFit.Domain.Interfaces.Repositories
{
    public interface IAthleteRepository
    {
        Task AddAsync(Athlete athlete);
        Task AddAsync(Athlete athlete, IDbConnection connection, IDbTransaction transaction);
        Task<Athlete?> GetByIdAsync(Guid id);
        Task<List<Athlete>> GetAllAsync(int skip, int take);
        Task UpdateAsync(Athlete athlete);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(Guid id, IDbConnection connection, IDbTransaction transaction);
    }
}
