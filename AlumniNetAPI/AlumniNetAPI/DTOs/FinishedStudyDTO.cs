using AlumniNetAPI.Models;

namespace AlumniNetAPI.DTOs
{
    public class FinishedStudyDTO
    {
        public int FinishedStudyId { get; set; }

        public int SpecializationId { get; set; }

        public int LearningScheduleId { get; set; }

        public int StudyProgramId { get; set; }

        public int Year { get; set; }

        public int ProfileId { get; set; }

        public LearningScheduleDTO LearningSchedule { get; set; } = null!;

        public SpecializationDTO? Specialization { get; set; }

        public StudyProgramDTO StudyProgram { get; set; } = null!;


    }
}
