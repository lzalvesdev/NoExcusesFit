using NoExcusesFit.Domain.Entities.Base;

namespace NoExcusesFit.Domain.Entities
{
    public class Athlete : EntityBase
    {
        public Guid UserAccountId { get; private set; }
        public Guid CoachId { get; private set; }

        private Athlete() { }
        public Athlete(Guid userAccountId, Guid coachId)
        {
            if (userAccountId == Guid.Empty)
                throw new ArgumentException("UserAccountId inválido.", nameof(userAccountId));

            if (coachId == Guid.Empty)
                throw new ArgumentException("CoachId inválido.", nameof(coachId));

            UserAccountId = userAccountId;
            CoachId = coachId;
        }

        public void UpdateCoach(Guid coachId)
        {
            if (coachId == Guid.Empty)
                throw new ArgumentException("CoachId inválido.", nameof(coachId));

            CoachId = coachId;
        }
    }
}
