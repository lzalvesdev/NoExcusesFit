using NoExcusesFit.Domain.Entities;

namespace NoExcusesFit.Domain.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task CreateAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task UpdateAsync(RefreshToken refreshToken);
    }
}
