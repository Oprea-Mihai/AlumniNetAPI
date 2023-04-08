using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface ISpecializationRepository:IBaseRepository<Specialization>
    {
        Task<List<Specialization>> GetSpecializationsByFacultyIdAsync(int facultyId);
        Task<Specialization> GetSpecializationByIdAsync(int id);
    }
}
