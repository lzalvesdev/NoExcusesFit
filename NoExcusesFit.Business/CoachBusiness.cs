using NoExcusesFit.Domain.DTOs.Athlete;
using NoExcusesFit.Domain.DTOs.Coach;
using NoExcusesFit.Domain.DTOs.Speciality;
using NoExcusesFit.Domain.Entities;
using NoExcusesFit.Domain.Enums;
using NoExcusesFit.Domain.Exceptions;
using NoExcusesFit.Domain.Interfaces.Business;
using NoExcusesFit.Domain.Interfaces.Repositories;

namespace NoExcusesFit.Business;

public class CoachBusiness : ICoachBusiness
{
    private readonly ICoachRepository _coachRepository;
    private readonly ICoachSpecialityRepository _coachSpecialityRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public CoachBusiness(
        ICoachRepository coachRepository, 
        ICoachSpecialityRepository coachSpecialityRepository,
        IUserRoleRepository userRoleRepository)
    {
        _coachRepository = coachRepository;
        _coachSpecialityRepository = coachSpecialityRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<IEnumerable<CoachResponseDto>> GetAllAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;

        var coaches = await _coachRepository.GetAllAsync(skip, pageSize);

        return coaches.Select(c => new CoachResponseDto(
             c.Id,
             c.UserAccountId,
             c.FirstName,
             c.Email,
             c.Specialities.Select(s => new SpecialitySummaryDto(s.Id, s.Description)).ToList(),
             c.Athletes.Select(a => new AthleteSumaryDto(a.Id, a.UserAccountId, a.FirstName, a.Email)).ToList()
        ));
    }

    public async Task<CoachResponseDto?> GetByIdAsync(Guid id)
    {
        var coach = await _coachRepository.GetByIdAsync(id);
        if (coach is null) return null;

        return new CoachResponseDto(
             coach.Id,
             coach.UserAccountId,
             coach.FirstName,
             coach.Email,
             coach.Specialities.Select(s => new SpecialitySummaryDto(s.Id, s.Description)).ToList(),
             coach.Athletes.Select(a => new AthleteSumaryDto(a.Id, a.UserAccountId, a.FirstName, a.Email)).ToList()
        );
    }

    public async Task<CoachCreatedResponseDto> AddAsync(CreateCoachRequestDto request)
    {
        var coach = new Coach(
           request.UserAccountId
        );

        await _coachRepository.AddAsync(coach);

        var coachRole = new UserRole(coach.UserAccountId, (int)UserRoleType.Coach);
        await _userRoleRepository.AssignUserRoleAsync(coachRole);

        return new CoachCreatedResponseDto(coach.Id);
    }

    public async Task AddSpecialityAsync(Guid coachId, int specialityId)
    {
        var alreadyExists = await _coachSpecialityRepository.ExistsAsync(coachId, specialityId);
        if (alreadyExists)
            throw new InvalidOperationException("Especialidade já vinculada a este treinador.");

        var speciality = new CoachSpeciality(coachId, specialityId);

        await _coachSpecialityRepository.AddAsync(speciality);
    }

    public async Task DeleteAsync(Guid id)
    {
        var coach = await _coachRepository.GetByIdAsync(id);
        if (coach is null)
            throw new NotFoundException("Treinador não encontrado.");

        await _coachRepository.DeleteAsync(id);
        await _userRoleRepository.DeleteRoleAsync(coach.UserAccountId, (int)UserRoleType.Coach);
    }
}