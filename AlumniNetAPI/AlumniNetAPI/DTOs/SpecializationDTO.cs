using AlumniNetAPI.Models;

namespace AlumniNetAPI.DTOs
{
    public class SpecializationDTO
    {
        public int SpecializationId { get; set; }

        public int FacultyId { get; set; }

        public string? SpecializationName { get; set; }
        public virtual FacultyDTO Faculty { get; set; } = null!;

    }
}
