namespace AlumniNetAPI.DTOs
{
    public class ExperienceDTO
    {
        public int ExperienceId { get; set; }
        public String JobTitle { get; set; }
        public String CompanyName { get; set; }
        public int StartDate{ get; set; }
        public int? EndDate{ get; set; }
    }
}
