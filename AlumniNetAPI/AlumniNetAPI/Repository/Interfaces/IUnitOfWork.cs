namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ISpecializationRepository SpecializationRepository { get; }
        IExperienceRepository ExperienceRepository { get; }
        IFacultyRepository FacultyRepository { get; }
        IFinishedStudyRepository FinishedStudyRepository { get; }
        ILearningScheduleRepository LearningScheduleRepository { get; }
        IPostRepository PostRepository { get; }
        IProfileRepository ProfileRepository { get; }
        IStudyProgramRepository StudyProgramRepository { get; }

        Task CompleteAsync();

    }
}
