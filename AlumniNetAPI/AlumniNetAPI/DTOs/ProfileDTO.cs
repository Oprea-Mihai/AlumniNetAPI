using AlumniNetAPI.Models;

namespace AlumniNetAPI.DTOs
{
    public class ProfileDTO
    {
        public int ProfileId { get; set; }

        public string? ProfilePicture { get; set; }

        public string? Description { get; set; }       
    }
}
