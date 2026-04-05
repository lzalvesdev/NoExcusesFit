namespace NoExcusesFit.Domain.Entities
{
    public class CoachSpeciality
    {
        public Guid CoachId { get; private set; }
        public int SpecialityId { get; private set; }

        private CoachSpeciality() { }

        public CoachSpeciality(Guid coachId, int specialityId)
        {
            if (coachId == Guid.Empty)
                throw new ArgumentException("CoachId é obrigatório.", nameof(coachId));

            if(specialityId == 0)
                throw new ArgumentException("SpecialityId é obrigatório.", nameof(specialityId));

            CoachId = coachId;
            SpecialityId = specialityId;
        }
    }
}
