using NoExcusesFit.Domain.Entities;

namespace NoExcusesFit.Domain.Interfaces.Repositories
{
    public interface IUserRoleRepository
    {
        Task AssignUserRoleAsync(UserRole userRole);
        Task<IEnumerable<string>> GetRolesAsync(Guid id);
        Task DeleteRoleAsync(Guid userAccountId, int roleId);
    }
}
