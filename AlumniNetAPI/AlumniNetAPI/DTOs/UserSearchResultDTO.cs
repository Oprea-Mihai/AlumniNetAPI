namespace AlumniNetAPI.DTOs
{
    public class UserSearchResultDTO
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; }
        public string FacultyName { get; set; }
        public int GraduationYear { get; set; }
    }
}
