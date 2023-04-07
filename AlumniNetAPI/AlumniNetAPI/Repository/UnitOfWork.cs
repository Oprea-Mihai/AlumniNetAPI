using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;

namespace AlumniNetAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository? _userRepository;
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        private ISpecializationRepository? _specializationRepository;
        public ISpecializationRepository SpecializationRepository => _specializationRepository ??= new SpecializationRepository(_context);

        private IExperienceRepository? _experienceRepository;
        public IExperienceRepository ExperienceRepository => _experienceRepository??=new ExperienceRepository(_context);

        private IFacultyRepository? _facultyRepository;
        public IFacultyRepository FacultyRepository => _facultyRepository??= new FacultyRepository(_context);

        private IFinishedStudyRepository? _finishedStudyRepository;
        public IFinishedStudyRepository FinishedStudyRepository => _finishedStudyRepository??= new FinishedStudyRepository(_context);

        private ILearningScheduleRepository? _learningScheduleRepository;
        public ILearningScheduleRepository LearningScheduleRepository => _learningScheduleRepository??= new LearningScheduleRepository(_context);

        private IPostRepository? _postRepository;
        public IPostRepository PostRepository => _postRepository??= new PostRepository(_context);

        private IProfileRepository? _profileRepository;
        public IProfileRepository ProfileRepository => _profileRepository??= new ProfileRepository(_context);

        private IStudyProgramRepository? _studyProgramRepository;
        public IStudyProgramRepository StudyProgramRepository => _studyProgramRepository??= new StudyProgramRepository(_context);



        private readonly AlumniNetAppContext _context;

        public UnitOfWork(AlumniNetAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }

                _disposed = true;
            }
        }
    }


}
