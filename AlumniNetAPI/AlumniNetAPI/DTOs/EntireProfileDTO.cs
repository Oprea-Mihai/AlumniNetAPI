namespace AlumniNetAPI.DTOs
{
    public class EntireProfileDTO
    {
        //profile
        public string? ProfilePicture { get; set; }

        public string? Description { get; set; }

        //user
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool IsValid { get; set; }
        public bool IsAdmin { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? LinkedIn { get; set; }

        //experiences
        public List<ExperienceDTO>Experiences { get; set; }

        //finished studies
        public List<FinishedStudyDetailedDTO> FinishedStudiesDetailed { get; set;}
    }
}
