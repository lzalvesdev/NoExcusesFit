
using NoExcusesFit.Domain.DTOs.Athlete;
using NoExcusesFit.Domain.Entities;
using NoExcusesFit.Domain.Enums;
using NoExcusesFit.Domain.Exceptions;
using NoExcusesFit.Domain.Interfaces;
using NoExcusesFit.Domain.Interfaces.Business;
using NoExcusesFit.Domain.Interfaces.Repositories;

namespace NoExcusesFit.Business
{
    public class AthleteBusiness : IAthleteBusiness
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AthleteBusiness(
            IAthleteRepository athleteRepository, 
            IUserRoleRepository userRoleRepository, 
            IUnitOfWork unitOfWork)
        {
            _athleteRepository = athleteRepository;
            _userRoleRepository = userRoleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AthleteResponseDto>> GetAllAsync(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;

            var athletes = await _athleteRepository.GetAllAsync(skip, pageSize);

            return athletes.Select(athlete => new AthleteResponseDto(athlete.Id, athlete.UserAccountId, athlete.CoachId));
        }

        public async Task<AthleteResponseDto?> GetByIdAsync(Guid id)
        {
            var athlete = await _athleteRepository.GetByIdAsync(id);
            if (athlete is null) return null;

            return new AthleteResponseDto(athlete.Id, athlete.UserAccountId, athlete.CoachId);
        }

        public async Task<AthleteResponseDto> AddAsync(CreateAthleteRequestDto request)
        {
            var athlete = new Athlete(request.UserAccountId, request.CoachId);
            var athleteRole = new UserRole(athlete.UserAccountId, (int)UserRoleType.Athlete);

            using var uow = _unitOfWork;
            uow.Begin();

            await _athleteRepository.AddAsync(athlete, uow.Connection, uow.Transaction);
            await _userRoleRepository.AssignUserRoleAsync(athleteRole, uow.Connection, uow.Transaction);

            uow.Commit();

            return new AthleteResponseDto(athlete.Id, athlete.UserAccountId, athlete.CoachId);
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

            using var uow = _unitOfWork;
            uow.Begin();

            await _athleteRepository.DeleteAsync(id, uow.Connection, uow.Transaction);
            await _userRoleRepository.DeleteRoleAsync(
                athlete.UserAccountId, 
                (int)UserRoleType.Athlete, 
                uow.Connection, 
                uow.Transaction);

            uow.Commit();
        }
    }
}
