
using NoExcusesFit.Domain.DTOs.Athlete;
using NoExcusesFit.Domain.Entities;
using NoExcusesFit.Domain.Enums;
using NoExcusesFit.Domain.Exceptions;
using NoExcusesFit.Domain.Interfaces.Business;
using NoExcusesFit.Domain.Interfaces.Repositories;

namespace NoExcusesFit.Business
{
    public class AthleteBusiness : IAthleteBusiness
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public AthleteBusiness(IAthleteRepository athleteRepository, IUserRoleRepository userRoleRepository)
        {
            _athleteRepository = athleteRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IEnumerable<AthleteResponseDto>> GetAllAsync(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;

            var athletes = await _athleteRepository.GetAllAsync(skip, pageSize);

            return athletes.Select(a => new AthleteResponseDto
            (
                Id: a.Id,
                UserAccountId: a.UserAccountId,
                CoachId: a.CoachId
            ));
        }

        public async Task<AthleteResponseDto?> GetByIdAsync(Guid id)
        {
            var athlete = await _athleteRepository.GetByIdAsync(id);
            if (athlete is null) return null;

            return new AthleteResponseDto
            (
                Id: athlete.Id,
                UserAccountId: athlete.UserAccountId,
                CoachId: athlete.CoachId
            );
        }

        public async Task<AthleteResponseDto> AddAsync(CreateAthleteRequestDto request)
        {
            var athlete = new Athlete(request.UserAccountId, request.CoachId);

            await _athleteRepository.AddAsync(athlete);

            var athleteRole = new UserRole(athlete.UserAccountId, (int)UserRoleType.Athlete);
            await _userRoleRepository.AssignUserRoleAsync(athleteRole);

            return new AthleteResponseDto
            (
                Id: athlete.Id,
                UserAccountId: athlete.UserAccountId,
                CoachId: athlete.CoachId
            );
        }

        public async Task UpdateAsync(Guid id, UpdateAthleteRequestDto request)
        {
            var athlete = await _athleteRepository.GetByIdAsync(id);

            if (athlete is null)
                throw new NotFoundException("Atleta não encontrado.");

            athlete.UpdateCoach(request.CoachId);

            await _athleteRepository.UpdateAsync(athlete);
        }

        public async Task DeleteAsync(Guid id)
        {
            var athlete = await _athleteRepository.GetByIdAsync(id);

            if (athlete is null)
                throw new NotFoundException("Atleta não encontrado.");

            await _athleteRepository.DeleteAsync(id);
            await _userRoleRepository.DeleteRoleAsync(athlete.UserAccountId, (int)UserRoleType.Athlete);
        }
    }
}
