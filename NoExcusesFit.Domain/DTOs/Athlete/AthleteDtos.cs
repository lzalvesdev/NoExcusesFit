namespace NoExcusesFit.Domain.DTOs.Athlete
{
    public record AthleteSumaryDto(
        Guid Id,
        Guid UserAccountId,
        string FirstName, 
        string Email
    );

    public record AthleteResponseDto(Guid Id, Guid UserAccountId, Guid CoachId);
    public record CreateAthleteRequestDto(Guid UserAccountId, Guid CoachId);
    public record UpdateAthleteRequestDto(Guid CoachId);
}
