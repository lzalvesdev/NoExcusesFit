using NoExcusesFit.Domain.Entities;

namespace NoExcusesFit.Domain.Interfaces.Repositories
{
    public interface IUserAccountRepository
    {
        Task CreateAsync(UserAccount userAccount);
        Task<UserAccount?> GetByEmailAsync(string email);
        Task<UserAccount?> GetByIdAsync(Guid id);
        Task<List<UserAccount>> GetAllAsync(int skip, int take);
        Task UpdateAsync(UserAccount userAccount);
        Task DeleteAsync(Guid id);
    }
}
